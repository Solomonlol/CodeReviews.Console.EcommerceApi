using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace EcommerseAPI.Frontend.Services
{
    internal class TableDrowingService : ITableDrawingService
    {
        public Task DrowSimpleTable<T>(PagedResult<T>? pagedResult = null,  string? title = null, CancellationToken ct = default, IEnumerable<T>? enumerableValues = null)
        {
            Console.Clear();
            var itemsList = new List<T>();
            if (pagedResult != null)
                itemsList = pagedResult.Items.ToList();
            else if (enumerableValues != null)
                itemsList = enumerableValues.ToList();
            else return Task.CompletedTask;
            
            var table = new Table()
                            .Border(TableBorder.Double)
                            .ShowRowSeparators();

            if (!string.IsNullOrEmpty(title))
                table.Title($"[green]{title}[/]");

            var properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t=>!t.PropertyType.IsInterface && !t.PropertyType.IsClass 
                        || t.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                table.AddColumn(property.Name);
            }

            foreach (var item in itemsList)
            {
                var values = properties.Select(p => p.GetValue(item)?.ToString() ?? string.Empty).ToArray();
                table.AddRow(values);

            }
                        
            AnsiConsole.Write(table);

            if (pagedResult != null)
            {
                var footerTable = new Table();
                footerTable.AddColumns($"Page: {pagedResult.Page}", $"Total pages: {pagedResult.TotalPages}", $"Page size: {pagedResult.PageSize}", $" Total count:{pagedResult.TotalCount}");
                AnsiConsole.Write(footerTable);
            }
            return Task.CompletedTask;
        }
    }
}
