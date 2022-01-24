
using BookStore.Filters.ResourceFilters.Caching;
using BookStore.Data.Contexto;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace BookStore.Filters.ResourceFilters
{
    public class CachingResourceFilter : Attribute, IResourceFilter
    {
        private const string Key = "livros";
        private readonly SimpleMemoryCache memoryCache;

        public CachingResourceFilter(SimpleMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (memoryCache.CheckIfExists(Key)) 
            {
                var livros = memoryCache.GetOrCreate<IEnumerable<Livro>>(Key, null);

                context.Result = new OkObjectResult(
                    new
                    {
                        Status = "Success",
                        Data = livros
                    });
            }
        }
    }
}