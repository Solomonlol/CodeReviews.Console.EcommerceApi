using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Services
{
    public class AttributeService : IAttributeService, IAttributeValueService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;
        public AttributeService(IMapper mapper, ApplicationContext db)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<Result> AddAttribute(string categoryName, ProductAttributeDto item, CancellationToken ct = default)
        {
            var checkCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Name == categoryName, ct);
            if (checkCategory != null)
            {
                var exists = await _db.ProductAttributes.AnyAsync(a => a.Name == item.Name, ct);

                if (exists) return Result.Failure("Attribute with this name already exists");

                var attribute = _mapper.Map<ProductAttribute>(item);
                await _db.ProductAttributes.AddAsync(attribute, ct);
                return await _db.SaveChangesAsync(ct) > 0
                    ? Result.Success(item)
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure("Category was not found");
        }
        public async Task<Result> DeleteAttribute(string categoryName, string attributeName, CancellationToken ct = default)
        {
            var checkCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Name == categoryName, ct);

            if (checkCategory == null) 
                return Result.Failure("Category was not found");

            var checkAttribute = await _db.ProductAttributes.FirstOrDefaultAsync(x => x.Name == attributeName, ct);
            if (checkAttribute != null)
            {
                _db.ProductAttributes.Remove(checkAttribute);
                return await _db.SaveChangesAsync(ct) > 0 
                    ? Result.Success(attributeName) 
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure("Attribute was not found");
        }
        public async Task<Result> UpdateAttribute(string categoryName, ProductAttributeDto item, CancellationToken ct = default)
        {
            var checkCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Name == categoryName, ct);
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

        public async Task<Result> AddAttributeValue(string productName, ProductAttributeValueDto item, CancellationToken ct = default)
        {
            var productCheck = await _db.Products.FirstOrDefaultAsync(p => p.Name == productName, ct);

            if (productCheck == null)
                return Result.Failure($"Product with name '{productName}' was not found.");

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


        public async Task<Result> DeleteAttributeValue(string productName, string productAttributeName, CancellationToken ct = default)
        {
            var productCheck = await _db.Products.FirstOrDefaultAsync(p => p.Name == productName, ct);

            if (productCheck == null)
                return Result.Failure($"Product with name '{productName}' was not found.");

            var productAttributeCheck = await _db.ProductAttributes.FirstOrDefaultAsync(a => a.Name == productAttributeName, ct);

            if (productAttributeCheck == null)
                return Result.Failure($"Attribute with name '{productAttributeName}' in product '{productName}' was not found.");

            var valueCheck = await _db.ProductAttributeValues
                .FirstOrDefaultAsync(a =>
                a.ProductAttribute.Name == productAttributeName &&
                a.Product.Name == productName, ct);
            if (valueCheck != null)
            {
                _db.ProductAttributeValues.Remove(valueCheck);
                return await _db.SaveChangesAsync(ct) > 0 
                    ? Result.Success() 
                    : Result.Failure("Cannot save changes to database");
            }
            else return Result.Failure("Attribute value was not found.");
        }

        public async Task<Result> UpdateAttributeValue(string productName, string productAttributeName, ProductAttributeValueDto item, CancellationToken ct = default)
        {
            var productCheck = await _db.Products.FirstOrDefaultAsync(p => p.Name == productName, ct);

            if (productCheck == null)
                return Result.Failure($"Product with name '{productName}' was not found.");

            var productAttributeCheck = await _db.ProductAttributes.FirstOrDefaultAsync(a => a.Name == productAttributeName, ct);

            if (productAttributeCheck == null)
                return Result.Failure($"Attribute with name '{productAttributeName}' in product '{productName}' was not found.");

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
