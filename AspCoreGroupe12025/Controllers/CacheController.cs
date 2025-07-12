using AspCoreGroupe12025.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreGroupe12025.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly RedisCacheService _redis;

        public CacheController()
        {
            _redis = new RedisCacheService();
        }

        [HttpPost("set")]
        public async Task<IActionResult> SetData([FromQuery] string key, [FromQuery] string value)
        {
            await _redis.SetDataAsync(key, value, TimeSpan.FromMinutes(10));
            return Ok("Saved to Redis");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetData([FromQuery] string key)
        {
            var value = await _redis.GetDataAsync<string>(key);
            return Ok(value ?? "Not found");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromQuery] string key)
        {
            var success = await _redis.RemoveDataAsync(key);
            return Ok(success ? "Removed" : "Key not found");
        }
    }
}
