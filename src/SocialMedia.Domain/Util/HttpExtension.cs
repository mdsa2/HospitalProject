using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Invoice.Domain.Util
{
    public static  class HttpExtension
    {
        public static void AddPagiinationHeader ( this HttpResponse response,PaginationHeader header)
        {
            var jsonOPtions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOPtions));
            response.Headers.Add("Access-Control-Expose-Headers", "pagination");
        }
    }
}
