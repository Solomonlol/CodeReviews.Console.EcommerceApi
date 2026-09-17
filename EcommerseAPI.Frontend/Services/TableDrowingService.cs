using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System.Reflection;

namespace EcommerseAPI.Frontend.Services
{
    internal class TableDrowingService : ITableDrawingService
    {
        public Task DrowSimpleTable<T>(PagedResult<T>? pagedResult = null, string? title = null, CancellationToken ct = default, IEnumerable<T>? enumerableValues = null, bool isNestedDrawing = false)
        {
            Console.Clear();
            
            var itemsList = new List<T>();
            if (pagedResult != null)
                itemsList = pagedResult.Items.ToList();
            else if (enumerableValues != null)
                itemsList = enumerableValues.ToList();
            else return Task.CompletedTask;

            IEnumerable<PropertyInfo> properties;


            properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => !t.PropertyType.IsInterface && !t.PropertyType.IsClass
                        || t.PropertyType == typeof(string));
            Draw<T>(properties, itemsList, title);


            if (isNestedDrawing)
            {
                properties = typeof(T)
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(t => IsCollection(t.PropertyType) && t.PropertyType!=typeof(string));
                                
                foreach (var property in properties)
                {
                    var elementType = GetCollectionElementType(property.PropertyType);
                        if (elementType == null) continue;

                    var allNestedItems = new List<object>();
                    foreach (var item in itemsList)
                    {
                        var collection = property.GetValue(item) as System.Collections.IEnumerable;
                        if (collection == null) continue;

                        foreach(var nested in collection)
                        {
                            if(nested!=null)
                                allNestedItems.Add(nested);
                        }    
                    }
                    if (allNestedItems.Count == 0) continue;
                    DrawNested(elementType, allNestedItems, $"{property.Name}");
                }
            }

            if (pagedResult != null)
            {
                var footerTable = new Table();
                footerTable.AddColumns($"Page: {pagedResult.Page}", $"Total pages: {pagedResult.TotalPages}", $"Page size: {pagedResult.PageSize}", $" Total count:{pagedResult.TotalCount}");
                AnsiConsole.Write(footerTable);
            }
            return Task.CompletedTask;
        }


        private void Draw<T>(IEnumerable<PropertyInfo>? properties, List<T> itemsList, string? title=null)
        {
            var table = new Table()
                            .Border(TableBorder.Double)
                            .ShowRowSeparators();

            if (!string.IsNullOrEmpty(title))
                table.Title($"[green]{title}[/]");


            foreach (var property in properties)
            {
                table.AddColumn(property.Name);
            }

            foreach (var item in itemsList)
            {
                var values = properties.Select(p => Markup.Escape(p.GetValue(item)?.ToString() ?? string.Empty)).ToArray();
                table.AddRow(values);
            }

            AnsiConsole.Write(table);
        }

        private void DrawNested(Type elementType, List<object> items, string title)
        {
            var properties = elementType
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => IsSimpleType(p.PropertyType))
        .ToList();

            if (properties.Count == 0) return;

            var table = new Table()
                .Border(TableBorder.Rounded)
                .ShowRowSeparators()
                .Title($"[yellow]{title}[/]");

            foreach (var property in properties)
                table.AddColumn(property.Name);

            foreach (var item in items)
            {
                var values = properties
                    .Select(p => Markup.Escape(p.GetValue(item)?.ToString() ?? string.Empty))
                    .ToArray();
                table.AddRow(values);
            }

            AnsiConsole.Write(table);
        }

        private static bool IsSimpleType(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            return type.IsPrimitive
                || type.IsEnum
                || type == typeof(string)
                || type == typeof(decimal)
                || type == typeof(DateTime)
                || type == typeof(DateTimeOffset)
                || type == typeof(Guid)
                || type == typeof(TimeSpan);
        }

        private static bool IsCollection(Type type)
        {
            return type != typeof(string)
                && typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }

        private static Type? GetCollectionElementType(Type collectionType)
        {
            if (collectionType.IsArray)
                return collectionType.GetElementType();

            if(collectionType.IsGenericType)
            {
                var args = collectionType.GetGenericArguments();
                if (args.Length == 1)
                    return args[0];
            }

            return typeof(object);
        }
    }
}
