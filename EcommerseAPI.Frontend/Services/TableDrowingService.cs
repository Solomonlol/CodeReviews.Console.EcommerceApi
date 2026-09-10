using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace EcommerseAPI.Frontend.Services
{
    internal class TableDrowingService : ITableDrowingService
    {
        public async Task DrowSimpleTable<T>(IEnumerable<T> itemsList, string? title = null, CancellationToken ct = default)
        {
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
        }
    }
}
