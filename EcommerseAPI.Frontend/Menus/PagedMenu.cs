using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services.Factory.Sort;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class PagedMenu<TService, TResultDto, TFilter> : UserInterface where TService : class, IPagedResultService where TFilter: class, IFilter, new()
    {
        private PagedResult<TResultDto>? _pagedResult;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITableDrawingService _drawingService;
        private readonly string _title;
        private SortParams? _sortParams;
        private TFilter _filter = new();
        public PagedMenu(IServiceProvider serviceProvider, ITableDrawingService drawingService, string title) : base(title) 
        {
            _title = title;
            _drawingService = drawingService;
            _serviceProvider = serviceProvider;
            AddExitOption("Back");
            AddItem("Next page", () => NextPage());
            AddItem("Previous page", () => PreviousPage());
            AddItem("Add filter", () => AddFiltering());
            AddItem("Add sort", () => AddSort());
        }

        public async Task NextPage(CancellationToken ct=default)
        {
            if(_pagedResult.Page<_pagedResult.TotalPages)
                _pagedResult.Page++;
            await GetAll(page: _pagedResult.Page, sort: _sortParams);
            
        }

        public async Task PreviousPage(CancellationToken ct = default)
        {
            if (_pagedResult.Page > 1)
                _pagedResult.Page--;
            await GetAll(page: _pagedResult.Page, sort: _sortParams);
        }

        public async Task AddFiltering(CancellationToken ct=default)
        {
            await GetAll(page: _pagedResult.Page, sort: _sortParams, filter: _filter);
        }
        
        public async Task AddSort(CancellationToken ct=default)
        {

        }

        private async Task GetAll(object? filter = null, SortParams? sort = null, int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            var service = _serviceProvider.GetRequiredService<TService>();
            var response = await service.GetAll(filter: filter, sort:sort, page:page, pageSize:pageSize, ct:ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<PagedResult<TResultDto>>(ct);
                if (content.Items.Any())
                {
                    await _drawingService.DrowSimpleTable(content, $"{_title}", ct);
                    _pagedResult = content;
                }
            }
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        protected override async Task OnStartingAsync(CancellationToken ct = default)
        {
            await GetAll(ct);
        }
    }
}
