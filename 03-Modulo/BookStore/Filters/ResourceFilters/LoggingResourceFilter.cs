
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BookStore.Filters.ResourceFilters
{
    public class LoggingResourceFilter : IResourceFilter
    {
        private readonly ILogger<LoggingResourceFilter> logger;

        public LoggingResourceFilter(ILogger<LoggingResourceFilter> logger)
        {
            this.logger = logger;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            logger.LogInformation("Resource executed.");
            Debug.WriteLine("Resource executed.");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            logger.LogInformation("Resource executing.");
            Debug.WriteLine("Resource executing.");
        }
    }
}