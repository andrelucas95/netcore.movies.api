using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Movies.API.Application.Models
{
    public class PageDataResponse<T> : IActionResult
    {
        public PageDataResponse(IList<T> data, int count)
        {
            Data = data;
            Count = count;
        }

        public IList<T> Data { get; set; }
        public int Count { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}