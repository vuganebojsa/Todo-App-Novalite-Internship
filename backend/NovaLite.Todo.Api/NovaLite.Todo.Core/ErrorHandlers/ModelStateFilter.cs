using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace NovaLite.Todo.Core.ErrorHandlers
{
    public class ModelStateFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            var response = new
            {
                errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(error => error.ErrorMessage))
            };
            var jsonErrorResponse = JsonConvert.SerializeObject(response);
            context.Result = new ContentResult
            {
                Content = jsonErrorResponse,
                ContentType = "application/json",
                StatusCode = 400
            };
        }
    }
}