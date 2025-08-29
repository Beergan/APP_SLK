using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RuomRaCoffe.Shared.Dtos;

namespace RuomRaCoffe.Admin.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

   

}