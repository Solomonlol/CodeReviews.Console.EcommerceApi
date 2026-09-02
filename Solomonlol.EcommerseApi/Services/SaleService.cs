using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.Models.Dto.Sale;
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
        public async Task<Result<SaleDtoResponce>> Create(SaleDtoRequest saleDto, CancellationToken ct = default)
        {
            if (!saleDto.SaleItems.Any())
                return Result<SaleDtoResponce>.Failure("Sale is empty. Add items before trying create new sale.");
            decimal totalPrice = 0;
            foreach (var item in saleDto.SaleItems)
            {
                var product = await _db.Products.FindAsync(item.ProductId);
                if(product!=null)
                    totalPrice += item.Quantity * product.Price;
            }

            var sale = _mapper.Map<Sale>(saleDto);
            sale.TotalPrice = totalPrice;
            sale.CreatedAt = DateTime.UtcNow;


            await _db.Sales.AddAsync(sale, ct);
            await _db.SaleItems.AddRangeAsync(sale.SaleItems, ct);
            var saveCount=await _db.SaveChangesAsync(ct);
            var saleCheck = await _db.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.CreatedAt == sale.CreatedAt && s.UserId == sale.UserId);

            

            var saleResponce = _mapper.Map<SaleDtoResponce>(saleCheck);
            return saveCount > 0 
                ? Result<SaleDtoResponce>.Success(saleResponce) 
                : Result<SaleDtoResponce>.Failure("Cannot save in database");
        }


        public async Task<Result<SaleDtoResponce>> Get(int saleId, CancellationToken ct = default)
        {
            var sale = await _db.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.Id == saleId, ct);

            if(sale==null)
                return Result<SaleDtoResponce>.Failure($"Sale with Id={saleId} was not found.");

            var saleDto = _mapper.Map<SaleDtoResponce>(sale);

            return Result<SaleDtoResponce>.Success(saleDto);
        }

        public async Task<Result<PagedResult<SaleDtoResponce>>> GetAll(int page=1, int pageSize=5, CancellationToken ct = default)
        {
            var totalCount = await _db.Sales.CountAsync(ct);
            var list = await _db.Sales
                .OrderBy(s=>s.CreatedAt)
                .Include(s=>s.SaleItems)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var dtoList = _mapper.Map<List<SaleDtoResponce>>(list);

            var pagedList = new PagedResult<SaleDtoResponce>()
            {
                Items = dtoList,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Result<PagedResult<SaleDtoResponce>>.Success(pagedList);
        }

        public async Task<Result<PagedResult<SaleDtoResponce>>> GetAllByLogin(string login, int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            var totalCount = await _db.Sales.Where(s=>s.User.Login==login).CountAsync(ct);
            var list = await _db.Sales
                .Where(s => s.User.Login == login)
                .OrderBy(s => s.CreatedAt)
                .Include(s => s.SaleItems)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var dtoList = _mapper.Map<List<SaleDtoResponce>>(list);

            var pagedList = new PagedResult<SaleDtoResponce>()
            {
                Items = dtoList,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Result<PagedResult<SaleDtoResponce>>.Success(pagedList);
        }

        public async Task<Result> Update(SaleDtoRequest item, CancellationToken ct = default)
        {
            var sale = await _db.Sales.FindAsync(item.UserId);
            if(sale!=null)
            {
                _mapper.Map(item, sale);
                _db.Sales.Update(sale);
                return await _db.SaveChangesAsync(ct) > 0 ? Result.Success(sale) : Result.Failure("Cannot save in database.");
            }
            return Result.Failure("Sale was not found.");
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
    }
}
