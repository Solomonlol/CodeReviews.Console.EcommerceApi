using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Services
{
    public class SaleService : ISaleService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;
        public SaleService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<Result<Sale>> Create(Sale sale, CancellationToken ct = default)
        {
            if (!sale.SaleItems.Any())
                return Result<Sale>.Failure("Sale is empty. Add items before trying create new sale.");

            foreach (var item in sale.SaleItems)
                sale.TotalPrice += item.Quantity * item.UnitPrice;

            await _db.Sales.AddAsync(sale, ct);
            await _db.SaleItems.AddRangeAsync(sale.SaleItems, ct);
            return await _db.SaveChangesAsync(ct) > 0 ? Result<Sale>.Success(sale) : Result<Sale>.Failure("Cannot save in database");
        }

        public async Task<Result> Delete(int saleId, CancellationToken ct = default)
        {
            var sale = await _db.Sales.FindAsync(saleId);
            if (sale != null)
            {
                sale.IsEnded = true;
                sale.EndedAt = DateTime.UtcNow;
                _db.Sales.Update(sale);
                return await _db.SaveChangesAsync(ct) > 0 ? Result.Success(sale) : Result.Failure("Cannot save in database");
            }
            else return Result.Failure($"Sale with Id={saleId} was not found.");
        }

        public async Task<Result<Sale>> Get(int saleId, CancellationToken ct = default)
        {
            var sale = await _db.Sales.FindAsync(saleId, ct);

            if(sale==null)
                return Result<Sale>.Failure($"Sale with Id={saleId} was not found.");

            return Result<Sale>.Success(sale);
        }

        public async Task<Result<PagedResult<Sale>>> GetAll(int page=1, int pageSize=5, CancellationToken ct = default)
        {
            var totalCount = await _db.Sales.CountAsync(ct);
            var list = await _db.Sales
                .OrderBy(s=>s.CreatedAt)
                .Include(s=>s.SaleItems)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var pagedList = new PagedResult<Sale>()
            {
                Items = list,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Result<PagedResult<Sale>>.Success(pagedList);
        }

        public async Task<Result<PagedResult<Sale>>> GetAllByLogin(string login, int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            var totalCount = await _db.Sales.Where(s=>s.User.Login==login).CountAsync(ct);
            var list = await _db.Sales
                .Where(s => s.User.Login == login)
                .OrderBy(s => s.CreatedAt)
                .Include(s => s.SaleItems)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var pagedList = new PagedResult<Sale>()
            {
                Items = list,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Result<PagedResult<Sale>>.Success(pagedList);
        }

        public async Task<Result> Update(Sale item, CancellationToken ct = default)
        {
            var sale = await _db.Sales.FindAsync(item);
            if(sale!=null)
            {
                _mapper.Map(item, sale);
                _db.Sales.Update(sale);
                return await _db.SaveChangesAsync(ct) > 0 ? Result.Success(sale) : Result.Failure("Cannot save in database.");
            }
            return Result.Failure("Sale was not found.");
        }
    }
}
