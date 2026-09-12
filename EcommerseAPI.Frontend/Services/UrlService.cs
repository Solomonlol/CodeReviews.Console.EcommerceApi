using Auth0.ManagementApi.Core;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services.Factory.Sort;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EcommerseAPI.Frontend.Services
{
    internal class UrlService : IUrlService
    {
        private readonly string _baseUrl ="";
        private List<string> _urlStrings = new();
        public UrlService(string baseUrl) 
        { 
            _baseUrl = baseUrl;
        }
        public Task<string> GetUrl(object? filter = null, SortParams? sort = null, int? page=null, int? pageSize=null, CancellationToken ct = default)
        {
            SetFilterUrl(filter);
            SetOrderByUrl(sort);
            SetPage(page, pageSize);
            string finalString = string.Empty;
            
            if (_urlStrings.Any())
                finalString = string.Join("&", _urlStrings);

            if (finalString.Length > 0)
                finalString= '?' + finalString;
                
            return Task.FromResult(_baseUrl+ finalString);
        }

        private void SetFilterUrl(object? filter = null)
        {
            if (filter is null) return;

            var properties = typeof(object).GetProperties().ToArray();

            if (!properties.Any())
                return;
                        
            foreach (var property in properties)
            {
                string filterString = string.Empty;
                var value = property.GetValue(filter);
                if (value is null) continue;

                filterString = $"{property.Name}={value}";
                _urlStrings.Add(filterString);
            }            
        }

        private void SetOrderByUrl(SortParams? sort = null)
        {
            if(sort==null)
                return;

            if(sort.OrderBy!=null)
                _urlStrings.Add($"orderBy={sort.OrderBy}");

            if (sort.Direction != null)
                _urlStrings.Add($"Direction={(int)sort.Direction}");
        }

        private void SetPage(int? page, int? pageSize)
        {
            if(page!=null)
                _urlStrings.Add($"page={page}");
            if(pageSize!=null)
                _urlStrings.Add($"pageSize={pageSize}");
        }
    }
}
