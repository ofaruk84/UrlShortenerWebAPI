using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlModalsController : ControllerBase
    {
        private readonly IUrlModalService _urlModalService;

        public UrlModalsController(IUrlModalService urlModalService)
        {
            _urlModalService = urlModalService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UrlRequestModal urlRequestModal)
        {
            var result = await _urlModalService.AddAsync(urlRequestModal);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("addcustom")]
        public async Task<IActionResult> AddCustom(UrlRequestModal urlRequestModal)
        {
            var result = await _urlModalService.AddCustomAsync(urlRequestModal);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("redirect")]
        public async Task<IActionResult> CreateAndRedirect(string shortUrl, [FromQuery] string redirect = "true")
        {
            var shortUrlResult = await _urlModalService.GetByShortUrlAsync(shortUrl);
           
            if (!shortUrlResult.Success) return BadRequest(shortUrlResult);

            if (redirect.Equals("true")) return Redirect(shortUrlResult.Data.UrlModal.LongUrl);

            return Ok(shortUrlResult);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _urlModalService.GetAllAsync();

            if (!result.Success) return BadRequest(result);

            return Ok(result);

        }
  
   


    }
}
