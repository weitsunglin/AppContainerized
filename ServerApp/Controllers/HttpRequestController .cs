using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MyApp.Services;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HttpRequestController : ControllerBase
    {
        private readonly HttpService _httpService;
        private readonly DbService _dbService;
        private readonly ILogger<HttpRequestController> _logger;

        public HttpRequestController(HttpService httpService, DbService dbService, ILogger<HttpRequestController> logger)
        {
            _httpService = httpService;
            _dbService = dbService;
            _logger = logger;
        }

        //處理client http request 業務邏輯
        [HttpPost("execute-query")]
        public async Task<IActionResult> ExecuteQuery([FromBody] QueryRequest request)
        {
            try
            {
                _logger.LogInformation("ExecuteQuery called with query: {Query}", request.Query);

                // Determine if the query is a SELECT or non-SELECT query
                if (request.Query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                {
                    var result = await _dbService.ExecuteQueryAsync(request.Query);
                    return Ok(result);
                }
                else
                {
                    var rowsAffected = await _dbService.ExecuteNonQueryAsync(request.Query);
                    _logger.LogInformation("Query executed successfully. Rows affected: {RowsAffected}", rowsAffected);
                    return Ok($"{rowsAffected} row(s) affected");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing query");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class QueryRequest
    {
        public string Query { get; set; }
    }
}