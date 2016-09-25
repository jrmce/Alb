using System.Collections.Generic;
using Alb.Models;
using Alb.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Alb.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {
        private IAlbumRepository _albumsRepo;
        public AlbumsController(IAlbumRepository albumsRepo)
        {
            _albumsRepo = albumsRepo;
        }
        
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<Album>))]
        public IEnumerable<Album> Get()
        {
            return _albumsRepo.FindAll();
        }

        [HttpGet("{id:int}")]
        [Produces("application/json", Type = typeof(Album))]
        public IActionResult Get(int id)
        {
            var album = _albumsRepo.Find(id);

            if (album == null) {
                return NotFound();
            }

            return Json(album);
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(Album))]
        [Consumes("application/json")]
        public IActionResult Post([FromBody]Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = _albumsRepo.Create(album);
            
            return CreatedAtAction("Get", new { id = created }, _albumsRepo.Find(created));
        }

        [HttpPut("{id:int}")]
        [Produces("application/json", Type = typeof(Album))]
        public IActionResult Put(int id, [FromBody]Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(_albumsRepo.Update(id, album));
        }

        [HttpDelete("{id:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int id)
        {
            _albumsRepo.Delete(id);
            var deleted = _albumsRepo.Find(id);

            if (deleted != null)
            {
                return BadRequest();
            }

            return Json(new { Status = "OK" });
        }
    }
}
