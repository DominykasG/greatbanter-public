using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.DTO;
using api.Models;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/banters")]
    [ApiController]
    public class BantersController : ControllerBase
    {
        private readonly IBantersService _banterService;
        public BantersController(IBantersService banterService) {
            _banterService = banterService;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost("AddBanter")]
        public async Task<ActionResult> AddBanter([FromBody]AddBanterDTO banter) 
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            await _banterService.AddBanter(banter, id);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost("AddScore")]
        public async Task<ActionResult> AddScore([FromBody] SetBanterScoreDTO setBanterScore)
        {
            await _banterService.SetScore(1, setBanterScore.Id);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost("RemoveScore")]
        public async Task<ActionResult> RemoveScore([FromBody] SetBanterScoreDTO setBanterScore)
        {
            await _banterService.SetScore(-1, setBanterScore.Id);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<BanterDTO>>> Get()
        {
            return Ok(await _banterService.GetAllBanters());
        }
        [HttpGet("GetRandom")]
        public async Task<ActionResult<BanterDTO>> GetRandomBanter()
        {
            return Ok(await _banterService.GetRandomBanter());
        }
        [HttpGet("GetBest")]
        public async Task<ActionResult<BanterDTO>> GetBestBanter() 
        {
            return Ok(await _banterService.GetBestBanter());
        }

    }
}