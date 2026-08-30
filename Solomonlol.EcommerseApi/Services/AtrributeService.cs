using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Services
{
    public class AtrributeService : IAttributeService, IAttributeValueService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;
        public AtrributeService(IMapper mapper, ApplicationContext db)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<Result> AddAttribute(ProductAttributeDto item, CancellationToken ct = default)
        {
            var checkCategory = await _db.Categories.FindAsync(item.CategoryId, ct);
            if (checkCategory != null)
            {
                var attribute = _mapper.Map<ProductAttribute>(item);
                await _db.ProductAttributes.AddAsync(attribute, ct);
                return await _db.SaveChangesAsync(ct) > 0
                    ? Result.Success(item)
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure("Category was not found");
        }
        public async Task<Result> DeleteAttribute(string name, CancellationToken ct = default)
        {
            var checkAttribute = await _db.ProductAttributes.FirstOrDefaultAsync(x => x.Name == name, ct);
            if (checkAttribute != null)
            {
                _db.ProductAttributes.Remove(checkAttribute);
                return await _db.SaveChangesAsync(ct) > 0 
                    ? Result.Success(name) 
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure("Attribute was not found");
        }
        public async Task<Result> UpdateAttribute(ProductAttributeDto item, CancellationToken ct = default)
        {
            var checkCategory = await _db.Categories.FindAsync(item.CategoryId, ct);
            if (checkCategory != null)
            {
                var attribute = await _db.ProductAttributes.FindAsync(item.Id, ct);
                if (attribute != null)
                {
                    _db.ProductAttributes.Update(attribute);
                    return await _db.SaveChangesAsync(ct) > 0 
                        ? Result.Success(item) 
                        : Result.Failure("Cannot save changes to database");
                }
                else return Result.Failure("Attribute was not found");
            }
            else return Result.Failure("Category was not found.");
        }

        public async Task<Result> AddAttributeValue(ProductAttributeValueDto item, CancellationToken ct = default)
        {
            var valueCheck = await _db.ProductAttributeValues
                .FirstOrDefaultAsync(a=>
                a.ProductAttributeId==item.ProductAttributeId && 
                a.ProductId==item.ProductId, ct);
            if (valueCheck == null)
            {
                var value = _mapper.Map<ProductAttributeValue>(item);
                await _db.ProductAttributeValues.AddAsync(value, ct);
                return await _db.SaveChangesAsync(ct) > 0 
                    ? Result.Success(item) 
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure("Attribute value already exist.");
        }


        public async Task<Result> DeleteAttributeValue(int productId, int productAttributeId, CancellationToken ct = default)
        {
            var valueCheck = await _db.ProductAttributeValues
                .FirstOrDefaultAsync(a =>
                a.ProductAttributeId == productId &&
                a.ProductId == productAttributeId, ct);
            if (valueCheck != null)
            {
                _db.ProductAttributeValues.Remove(valueCheck);
                return await _db.SaveChangesAsync(ct) > 0 
                    ? Result.Success() 
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure("Attribute value was not found.");
        }


        public async Task<Result> UpdateAttributeValue(ProductAttributeValueDto item, CancellationToken ct = default)
        {
            var valueCheck = await _db.ProductAttributeValues
                .FirstOrDefaultAsync(a =>
                a.ProductAttributeId == item.ProductAttributeId &&
                a.ProductId == item.ProductId, ct);
            if(valueCheck!=null)
            {
                var value = _mapper.Map<ProductAttributeValue>(item);
                _db.ProductAttributeValues.Update(value);
                return await _db.SaveChangesAsync(ct) > 0
                    ? Result.Success()
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure("Attribute value was not found.");
        }
    }
}
