using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace EcommerseAPI.Frontend.Services
{
    internal class AttributeService : IAttributeService
    {
        //private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        //private readonly ITableDrawingService _drawingService;
        private readonly string _attributeUrl = "api/v1/categories/";
        private readonly string _attributeValueUrl = "api/v1/products/";
        private readonly HttpClient _httpClient;
        public AttributeService(IServiceProvider sp) 
        {
            //_drawingService = sp.GetRequiredService<ITableDrawingService>();
            //_categoryService = sp.GetRequiredService<ICategoryService>();
            _productService = sp.GetRequiredService<IProductService>();
            _httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient");
        }

        public async Task AddAttribute(IEnumerable<ProductDto> attributes, CancellationToken ct = default)
        {
            try
            {
                var productList = attributes.ToList();
                var product = await AnsiConsole.PromptAsync(new SelectionPrompt<ProductDto>().Title("[yellow]Choose product:[/]")
                    .UseConverter(p=>$"{p.Name} | {p.CategoryName}")
                    .AddChoices(productList));
                await _productService.GetOne(product.Name, ct);

                var attributeDto = new CategoryAttributeDtoRequest()
                {
                    Name = await AnsiConsole.AskAsync<string>("[yellow]Enter attribute name:[/]"),
                    Unit = await AnsiConsole.PromptAsync(new TextPrompt<string>("[yellow]Enter unit measurement(optional):[/]").AllowEmpty())
                };

                var url = _attributeUrl + $"{product.CategoryName}/attributes";
                var dto = JsonSerializer.Serialize(attributeDto);
                var content = new StringContent(dto, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content, ct);

                if (!response.IsSuccessStatusCode)
                {
                    AnsiConsole.MarkupLine($"Error: {response.StatusCode}");
                    return;
                }

                if (await AnsiConsole.ConfirmAsync("Add value?"))
                {
                    var value = new ProductAttributeValueDto()
                    {
                        ProductAttributeName = attributeDto.Name,
                        ProductId = product.Id,
                        Value = await AnsiConsole.AskAsync<string>("[yellow]Enter value:[/]")
                    };

                    url = _attributeValueUrl + $"{product.Name}/attributes";
                    dto = JsonSerializer.Serialize(value);
                    content = new StringContent(dto, Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(url, content, ct);

                    if (!response.IsSuccessStatusCode)
                    {
                        AnsiConsole.MarkupLine($"Error: {response.StatusCode}");
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }


        public async Task DeleteAttribute(IEnumerable<ProductDto> products, CancellationToken ct = default)
        {
            try
            {
                var productList = products.ToList();
                var product = await AnsiConsole.PromptAsync(new SelectionPrompt<ProductDto>().Title("[yellow]Choose product:[/]")
                    .UseConverter(p => $"{p.Name} | {p.CategoryName}")
                    .AddChoices(productList));

                var dto = await _productService.GetOne(product.Name, ct);
                if (dto == null)
                {
                    AnsiConsole.MarkupLine($"[red]Product was not found.[/]");
                    return;
                }

                
                var attributeList = dto.Attributes.ToList();
                var productAttributeName = await AnsiConsole.PromptAsync(new SelectionPrompt<string>().Title("[yellow]Choose attribute:[/]")
                    .AddChoices(attributeList.Select(a => a.Name)));

                var url = _attributeUrl + $"{product.CategoryName}/attributes/{productAttributeName}";
                var response = await _httpClient.DeleteAsync(url, ct);

                if (response.IsSuccessStatusCode)
                    AnsiConsole.MarkupLine("[green]Attribute was deleted.[/]");

            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        public async Task UpdateAttribute(IEnumerable<ProductDto> products, CancellationToken ct = default)
        {
            try
            {
                var productList = products.ToList();
                var product = await AnsiConsole.PromptAsync(new SelectionPrompt<ProductDto>().Title("[yellow]Choose product:[/]")
                    .UseConverter(p => $"{p.Name} | {p.CategoryName}")
                    .AddChoices(productList));
                var fullProduct = await _productService.GetOne(product.Name, ct);
                if (fullProduct == null)
                {
                    AnsiConsole.MarkupLine($"[red]Product was not found.[/]");
                    return;
                }

                var attributeList = fullProduct.Attributes.ToList();
                var productAttributeName = await AnsiConsole.PromptAsync(new SelectionPrompt<string>().Title("[yellow]Choose attribute:[/]")
                    .AddChoices(attributeList.Select(a => a.Name)));

                var attributeDto = new CategoryAttributeDtoRequest()
                {
                    Unit = await AnsiConsole.PromptAsync(new TextPrompt<string>("[yellow]Enter unit measurement(optional):[/]").AllowEmpty())
                };

                var url = _attributeUrl + $"{product.CategoryName}/attributes/{productAttributeName}";
                var dto = JsonSerializer.Serialize(attributeDto);
                var content = new StringContent(dto, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(url, content, ct);

                if (!response.IsSuccessStatusCode)
                    return;

                if (await AnsiConsole.ConfirmAsync("Update value?"))
                {
                    var value = new ProductAttributeValueDto()
                    {
                        ProductAttributeName = productAttributeName,
                        ProductId = product.Id,
                        Value = await AnsiConsole.AskAsync<string>("[yellow]Enter value:[/]")
                    };

                    url = _attributeValueUrl + $"{product.Name.Trim().ToLower()}/attributes/{productAttributeName}";
                    dto = JsonSerializer.Serialize(value);
                    content = new StringContent(dto, Encoding.UTF8, "application/json");
                    response = await _httpClient.PutAsync(url, content, ct);

                    if (!response.IsSuccessStatusCode)
                    {
                        AnsiConsole.MarkupLine($"Error: {response.StatusCode}");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }
        //private async Task AddAttributeValue(CancellationToken ct = default)
        //{
        //    var value = 
        //}

        //private Task UpdateAttributeValue(CancellationToken ct = default)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
