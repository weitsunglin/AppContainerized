using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MyApp.Services;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HttpRequestHandlerController : ControllerBase
    {
        private readonly HttpRequestHandler _httpHandler;
        private readonly DatabaseHandler _dbHandler;
        private readonly ILogger<HttpRequestHandlerController> _logger;

        public HttpRequestHandlerController(HttpRequestHandler httpHandler, DatabaseHandler dbHandler, ILogger<HttpRequestHandlerController> logger)
        {
            _httpHandler = httpHandler;
            _dbHandler = dbHandler;
            _logger = logger;
        }

        [HttpPost("execute-query")]
        public async Task<IActionResult> ExecuteQuery([FromBody] QueryRequest request)
        {
            try
            {
                _logger.LogInformation("ExecuteQuery called with query: {Query}", request.Query);

                // 判斷查詢是 SELECT 還是非 SELECT 查詢
                if (request.Query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                {
                    var result = await _dbHandler.ExecuteQueryAsync(request.Query);
                    return Ok(result);
                }
                else
                {
                    var rowsAffected = await _dbHandler.ExecuteNonQueryAsync(request.Query);
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
