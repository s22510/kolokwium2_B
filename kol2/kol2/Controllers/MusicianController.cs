using kol2.Models.DTOS;
using kol2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Controllers
{
    [Route("musicians")]
    [ApiController]
    public class MusicianController : ControllerBase
    {
        private readonly IDbService _dbService;

        public MusicianController(IDbService service)
        {
            _dbService = service;
        }

        [Route("{idMusician}")]
        [HttpGet]
        public Task<IEnumerable<object>> GetMusician(int idMusician)
        {
            return _dbService.GetMusician(idMusician);
        }

        [Route("{idMusician}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMusicianAsync(int idMusician)
        {
            if (await _dbService.checkAlbumArtist(idMusician)) return NotFound("Muzyk jest na albumie, nie mozna usunac");
            _dbService.DeleteMusician(idMusician);
            await _dbService.SaveChanges();
            return Ok();
        }
    }
}
