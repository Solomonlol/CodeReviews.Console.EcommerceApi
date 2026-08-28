using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Result> Create(CategoryDto item, CancellationToken ct = default)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c=>c.Name==item.Name, ct);
            if (category == null)
            {
                category = _mapper.Map<Category>(item);
                await _db.Categories.AddAsync(category, ct);

                return await _db.SaveChangesAsync(ct) > 0
                    ? Result.Success(item)
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure($"Category with name '{item.Name}' already exist.");
        }

        public async Task<Result> Delete(string name, CancellationToken ct = default)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Name == name, ct);
            if (category != null)
            {
                _db.Remove(category);
                return await _db.SaveChangesAsync(ct) > 0 
                    ? Result.Success(category) 
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure($"Category with name '{name}' was not found.");
        }

        public async Task<Result<CategoryDto>> Get(string name, CancellationToken ct = default)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c=>c.Name==name, ct);
            return category != null 
                ? Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(category)) 
                : Result<CategoryDto>.Failure($"Category with name '{name}' was not found.");
        }

        public async Task<Result<PagedResult<CategoryDto>>> GetAll(int page=1, int pageSize =5, CancellationToken ct = default)
        {
            var totalCount = await _db.Categories.CountAsync(ct);
            var list = await _db.Categories
                .OrderBy(c=>c.Name)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
            var dtoList = _mapper.Map<IEnumerable<CategoryDto>>(list);
            var pagedResult = new PagedResult<CategoryDto>()
            {
                Items = dtoList,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Totalpages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            return Result<PagedResult<CategoryDto>>.Success(pagedResult);
        }

        public async Task<Result> Update(string name, CategoryDto item, CancellationToken ct = default)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Name == name, ct);
            if (category != null)
            {
                _mapper.Map(item, category);
                _db.Categories.Update(category);
                return await _db.SaveChangesAsync(ct) > 0
                    ? Result.Success(category)
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure($"Category with name '{name}' was not found.");
        }
    }
}
