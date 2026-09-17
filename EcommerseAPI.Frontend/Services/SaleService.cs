using EcommerseAPI.Frontend.Entities.Dto.Sales;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Entities.Sort;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static EcommerseAPI.Frontend.Entities.EnumHelper;

namespace EcommerseAPI.Frontend.Services
{
    internal class SaleService : ISaleService
    {
        private readonly HttpClient _httpClient;
        private readonly IUrlService _urlService;
        private readonly ITableDrawingService _drawingService;
        private readonly IShoppingCartService _cartService;
        public SaleService(IUrlServiceFactory serviceFactory, 
            ITableDrawingService drawingService, 
            IHttpClientFactory clientFactory,
            IShoppingCartService cartService)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
            _urlService = serviceFactory.Create("api/v1/sales/");
            _drawingService = drawingService;
            _cartService = cartService;
        }
        public async Task Create(CancellationToken ct = default)
        {
            try
            {
                var cartList = _cartService.GetList();
                if (cartList.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]No items in cart.[/]");
                    return;
                }
                if (await AnsiConsole.ConfirmAsync($"Are you sure to create deal with {cartList.Count} items? ", cancellationToken: ct))
                {
                    var sale = new SaleDtoRequest();
                    foreach (var item in cartList)
                    {
                        sale.SaleItems.Add(new SaleItemDtoRequest { ProductId = item.Product.Id, Quantity = item.Quantity });
                    }

                    var Url = await _urlService.GetUrl(ct: ct);
                    var dto = JsonSerializer.Serialize(sale);
                    var content = new StringContent(dto, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(Url, content, ct);
                    if (response.IsSuccessStatusCode)
                        AnsiConsole.MarkupLine("[green]Successfully created[/]");
                    else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
                }
                else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        public async Task Close(int id, CancellationToken ct = default)
        {
            try
            {
                var Url = await _urlService.GetUrl(ct: ct);
                var response = await _httpClient.DeleteAsync(Url + id, ct);
                if (response.IsSuccessStatusCode)
                    AnsiConsole.MarkupLine($"[green]Sale with id={id} was deleted.[/]");
                else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        public async Task<HttpResponseMessage?> GetAll(IFilter? filter = null, SortParams? sort = null, int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            try
            {
                var Url = await _urlService.GetUrl(ct: ct, pageSize: pageSize, page: page, sort: sort, filter:filter);
                var response = await _httpClient.GetAsync(Url, ct);
                return response;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                return null;
            }
        }

        public async Task GetOne(int id, CancellationToken ct = default)
        {
            try
            {
                var url = await _urlService.GetUrl(ct: ct) + $"{id}";
                var response = await _httpClient.GetAsync(url, ct);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<UserDtoResponse>(ct);
                    if (content != null)
                    {
                        var list = new List<UserDtoResponse>();
                        list.Add(content);
                        await _drawingService.DrowSimpleTable(enumerableValues: list, title: $"Sale id: {id}", ct: ct, isNestedDrawing: true);
                    }
                }
                else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }
    }
}
