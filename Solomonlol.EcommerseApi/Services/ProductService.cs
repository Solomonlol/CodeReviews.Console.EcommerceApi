using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;
        public ProductService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Result> Create(ProductDto item, CancellationToken ct = default)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == item.Name, ct);
            if (product == null)
            {
                var createdProduct =_mapper.Map<Product>(item);
                await _db.Products.AddAsync(createdProduct, ct);
                return await _db.SaveChangesAsync(ct)>0 
                    ? Result.Success(item) 
                    : Result.Failure("Cannot save changes to database.");
            }
            else return Result.Failure($"Product with name {item.Name} already exist.");
        }

        public async Task<Result> Delete(string name, CancellationToken ct = default)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == name, ct);
            if (product != null)
            {
                product.IsDeleted = true;
                _db.Products.Update(product);
                return await _db.SaveChangesAsync(ct) > 0
                    ? Result.Success(name)
                    : Result.Failure("Cannot save changes to database.");

            }
            else return Result.Failure($"Product with name {name} was not found.");
        }

        public async Task<Result<ProductDto>> Get(string name, CancellationToken ct = default)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == name, ct);
            
            return product != null
                ? Result<ProductDto>.Success(_mapper.Map<ProductDto>(product)) 
                : Result<ProductDto>.Failure($"Product with name {name} was not found.");
        }

        public async Task<Result<PagedResult<ProductDto>>> GetAll(int page=1, int pageSize =5, CancellationToken ct = default)
        {
            var totalCount = await _db.Products.CountAsync(ct);

            var list = await _db.Products
                .OrderBy(c => c.Name)
                .Include(c=>c.Category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
            var dtoList = _mapper.Map<IEnumerable<ProductDto>>(list);
            var pagedResult = new PagedResult<ProductDto>()
            {
                Items = dtoList,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            return Result<PagedResult<ProductDto>>.Success(pagedResult);
        }

        public async Task<Result> Update(string name, ProductDto item, CancellationToken ct = default)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == name, ct);
            if (product != null)
            {
                _mapper.Map(item, product);
                _db.Products.Update(product);
                return await _db.SaveChangesAsync(ct) > 0
                    ? Result.Success(item)
                    : Result.Failure("Cannot save changes to database.");
            }
            else return Result.Failure($"Product with name {name} was not found.");
        }
    }
}
