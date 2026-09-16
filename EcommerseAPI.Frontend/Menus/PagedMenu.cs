using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services.Factory.Sort;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.Net.Http.Json;

namespace EcommerseAPI.Frontend.Menus
{
    internal class PagedMenu<TService, TResultDto, TFilter> : UserInterface where TService : class, IPagedResultService where TFilter : class, IFilter, new()
    {
        private PagedResult<TResultDto>? _pagedResult;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITableDrawingService _drawingService;
        private readonly IProductService? _productService;
        private readonly string _title;
        private SortParams? _sortParams;
        private TFilter _filter = new();
        public PagedMenu(IServiceProvider serviceProvider, ITableDrawingService drawingService, string title) : base(title)
        {
            _title = title;
            _drawingService = drawingService;
            _serviceProvider = serviceProvider;
            AddItem("Next page", () => NextPage());
            AddItem("Previous page", () => PreviousPage());
            AddItem("Choose page", () => ChoosePageNumber());
            AddItem("Change page size", () => ChangePageSize());
            AddItem("Add filter", () => AddFiltering());
            AddItem("Add sort", () => AddSort());
            AddExitOption("Back");
        }

        public PagedMenu(IServiceProvider serviceProvider, ITableDrawingService drawingService, IProductService productService, string title) : base(title)
        {
            _title = title;
            _drawingService = drawingService;
            _serviceProvider = serviceProvider;
            _productService = productService;
            AddItem("Next page", () => NextPage());
            AddItem("Previous page", () => PreviousPage());
            AddItem("Choose page", () => ChoosePageNumber());
            AddItem("Change page size", () => ChangePageSize());
            AddItem("Add filter", () => AddFiltering());
            AddItem("Add sort", () => AddSort());
            AddExitOption("Back");
        }

        public async Task NextPage(CancellationToken ct = default)
        {
            if (_pagedResult == null) return;

            if (_pagedResult.Page < _pagedResult.TotalPages)
                _pagedResult.Page++;
            await GetAll(page: _pagedResult.Page, pageSize: _pagedResult.PageSize, sort: _sortParams, filter: _filter, ct: ct);

        }

        public async Task PreviousPage(CancellationToken ct = default)
        {
            if (_pagedResult == null) return;

            if (_pagedResult.Page > 1)
                _pagedResult.Page--;
            await GetAll(page: _pagedResult.Page, pageSize: _pagedResult.PageSize, sort: _sortParams, filter: _filter, ct: ct);
        }

        public async Task ChoosePageNumber(CancellationToken ct=default)
        {
            if (_pagedResult == null) return;

            var pageNumber = await AnsiConsole.AskAsync<int>("Enter page number what you need:");
            if (pageNumber > _pagedResult.TotalPages | pageNumber < 1)
                AnsiConsole.MarkupLine("[red]Incorrect data[/]");
            else
            {
                _pagedResult.Page = pageNumber;
                await GetAll(page: _pagedResult.Page, pageSize: _pagedResult.PageSize, sort: _sortParams, filter: _filter, ct: ct);
            }
        }

        public async Task ChangePageSize(CancellationToken ct = default)
        {
            if (_pagedResult == null) return;

            var pageSize = await AnsiConsole.AskAsync<int>("Enter page number what you need:");
            if (pageSize < 1)
                AnsiConsole.MarkupLine("[red]Incorrect data[/]");
            else
            {
                _pagedResult.Page = 1;
                _pagedResult.PageSize = pageSize;
                await GetAll(page: _pagedResult.Page, pageSize: _pagedResult.PageSize, sort: _sortParams, filter: _filter, ct:ct);
            }
        }

        public async Task AddFiltering(CancellationToken ct = default)
        {
            if (_pagedResult == null) return;

            await GetAll(page: _pagedResult.Page, pageSize: _pagedResult.PageSize, sort: _sortParams, filter: _filter, ct: ct);
        }

        public async Task AddSort(CancellationToken ct = default)
        {
            if (_pagedResult == null) return;

            await GetAll(page: _pagedResult.Page, pageSize: _pagedResult.PageSize, sort: _sortParams, filter: _filter, ct: ct);
        }

        private async Task GetAll(object? filter = null, SortParams? sort = null, int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            try
            {
                var service = _serviceProvider.GetRequiredService<TService>();
                var response = await service.GetAll(filter: filter, sort: sort, page: page, pageSize: pageSize, ct: ct);
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
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }

        protected override async Task OnStartingAsync(CancellationToken ct = default)
        {
            await GetAll(ct);
        }
    }
}
