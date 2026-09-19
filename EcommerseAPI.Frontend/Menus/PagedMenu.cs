using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Dto.Sales;
using EcommerseAPI.Frontend.Entities.Filters;
using EcommerseAPI.Frontend.Entities.Sort;
using EcommerseAPI.Frontend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Reflection;

namespace EcommerseAPI.Frontend.Menus
{
    internal class PagedMenu<TService, TResultDto, TFilter> : UserInterface where TService : class, IPagedResultService where TFilter : class, IFilter, new()
    {
        private protected PagedResult<TResultDto>? _pagedResult;
        private protected readonly IServiceProvider _serviceProvider;
        private protected readonly ITableDrawingService _drawingService;
        
        private protected readonly string _title;
        private protected SortParams _sortParams = new();
        private protected TFilter _filter = new();
        public PagedMenu(IServiceProvider sp, string title) : base(title)
        {
            _title = title;
            _serviceProvider = sp;
            _drawingService = sp.GetRequiredService<ITableDrawingService>();

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

            var pageNumber = await AnsiConsole.AskAsync<int>("[yellow]Enter page number what you need:[/]");
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

            var pageSize = await AnsiConsole.AskAsync<int>("[yellow]Enter page number what you need:[/]");
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

            var filter = Activator.CreateInstance<TFilter>();

            var properties = typeof(TFilter).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => !t.PropertyType.IsInterface && !t.PropertyType.IsClass
                        || t.PropertyType == typeof(string));

            var filterParams = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<string>()
                .Title("Choose filter parameters to add:")
                .AddChoices(properties.Select(p => p.Name)));

            foreach (var paramName in filterParams)
            {
                var prop = properties.First(p => p.Name == paramName);

                object? value = prop.PropertyType switch
                {
                    Type t when t == typeof(int) || t == typeof(int?) => await AnsiConsole.AskAsync<int>($"[yellow]Set {prop.Name}:[/]", ct),

                    Type t when t == typeof(decimal) || t== typeof(decimal?) => await AnsiConsole.AskAsync<decimal>($"[yellow]Set {prop.Name}:[/]", ct),

                    Type t when t == typeof(string) => await AnsiConsole.AskAsync<string>($"[yellow]Set {prop.Name}:[/]"),

                    _ => null
                };

                if (value != null)
                    prop.SetValue(filter, value);
            }
            _filter = filter;
            _pagedResult.Page = 1;

            await GetAll(page: _pagedResult.Page, pageSize: _pagedResult.PageSize, sort: _sortParams, filter: _filter, ct: ct);
        }

        public async Task AddSort(CancellationToken ct = default)
        {
            if (_pagedResult == null) return;

            var properties = typeof(SortParams).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => !t.PropertyType.IsInterface && !t.PropertyType.IsClass
                        || t.PropertyType == typeof(string)); 

            var sortParams = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<string>()
                .Title("Choose sort parameters to add:")
                .AddChoices(properties.Select(p=>p.Name)));

            foreach (var property in properties)
            {
                var dtoProperties = typeof(TResultDto).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => !t.PropertyType.IsInterface && !t.PropertyType.IsClass
                        || t.PropertyType == typeof(string));
                if (sortParams.Contains(property.Name))
                    switch (property.Name)
                    {
                        case "OrderBy":
                            _sortParams.OrderBy = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                .Title("Order by:")
                                .AddChoices(dtoProperties.Select(p => p.Name)));
                            break;
                        case "Direction":
                            _sortParams.Direction = await AnsiConsole.PromptAsync(new SelectionPrompt<ListSortDirection>()
                                .Title("Direction:")
                                .AddChoices(ListSortDirection.Ascending, ListSortDirection.Descending));
                            break;
                    }
            }
            _pagedResult.Page = 1;
            await GetAll(page: _pagedResult.Page, pageSize: _pagedResult.PageSize, sort: _sortParams, filter: _filter, ct: ct);
        }

        private async Task GetAll(IFilter? filter = null, SortParams? sort = null, int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            try
            {
                var service = _serviceProvider.GetRequiredService<TService>();
                var response = await service.GetAll(filter: filter, sort: sort, page: page, pageSize: pageSize, ct: ct);
                if (response!=null && response.IsSuccessStatusCode)
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
            await GetAll(ct:ct);
        }
    }
}
