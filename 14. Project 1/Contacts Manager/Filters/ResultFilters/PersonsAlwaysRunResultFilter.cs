using Microsoft.AspNetCore.Mvc.Filters;

namespace Contacts_Manager.Filters.ResultFilters
{
    // This filter triggers always even after any filter short circuits
    public class PersonsAlwaysRunResultFilter : IAlwaysRunResultFilter
    {
        private readonly ILogger<PersonsAlwaysRunResultFilter> _logger;

        public PersonsAlwaysRunResultFilter(ILogger<PersonsAlwaysRunResultFilter> logger)
        {
            _logger = logger;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("Always Run Action Filter After");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("Always Run Action Filter Before");
        }
    }
}
