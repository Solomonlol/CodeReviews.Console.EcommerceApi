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
        public async Task DrowTable<T>(IEnumerable<T> itemsList, CancellationToken ct = default)
        {
            var table = new Table();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                table.AddColumn(property.Name);
            }

            foreach(var item in itemsList)
            {
                var values = properties.Select(p => p.GetValue(item)?.ToString() ?? string.Empty).ToArray();
                
            }

            AnsiConsole.Write(table);
        }
    }
}
