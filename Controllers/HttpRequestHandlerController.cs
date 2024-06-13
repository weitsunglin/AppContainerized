using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MySocketHttpApp.Services;

namespace MySocketHttpApp.Controllers
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

        [HttpPost("add-record")]
        public async Task<IActionResult> AddRecord([FromBody] AddRecordRequest request)
        {
            try
            {
                _logger.LogInformation("AddRecord called with TableName: {TableName}, ColumnNames: {ColumnNames}, Values: {Values}", request.TableName, request.ColumnNames, request.Values);

                var rowsAffected = await _dbHandler.AddRecordAsync(request.TableName, request.ColumnNames, request.Values);

                _logger.LogInformation("Record added successfully. Rows affected: {RowsAffected}", rowsAffected);

                return Ok($"{rowsAffected} record(s) added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding record");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class AddRecordRequest
    {
        public string TableName { get; set; } = string.Empty;
        public string ColumnNames { get; set; } = string.Empty;
        public string Values { get; set; } = string.Empty;
    }
}
