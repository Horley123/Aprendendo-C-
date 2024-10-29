using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // executa antes do action
            _logger.LogInformation("### Executando -> OnActionExecuting");
            _logger.LogInformation("##############################");
            _logger.LogInformation($"### {DateTime.Now.ToLongDateString()}");
            _logger.LogInformation($"### Model State: {context.ModelState.IsValid}");
            _logger.LogInformation("##############################");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // executa apos do action
            _logger.LogInformation("### Executando -> OnActionExecuted");
            _logger.LogInformation("##############################");
            _logger.LogInformation($"### {DateTime.Now.ToLongDateString()}");
            _logger.LogInformation($"### Model State: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("##############################");
        }
    }
}