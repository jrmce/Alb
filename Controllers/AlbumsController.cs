using System.Collections.Generic;
using Alb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alb.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {
        private IResourceRepository<Album> _albumsRepo;
        public AlbumsController(IResourceRepository<Album> albumsRepo)
        {
            _albumsRepo = albumsRepo;
        }
        
        // GET api/albums
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<Album>))]
        public IEnumerable<Album> Get()
        {
            return _albumsRepo.FindAll();
        }

        // GET api/albums/5
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

        // POST api/albums
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

        // PUT api/albums/5
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

        // DELETE api/albums/5
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
