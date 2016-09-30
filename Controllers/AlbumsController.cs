using System.Collections.Generic;
using System.Linq;
using Alb.Models;
using Alb.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Alb.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {
        private IAlbumRepository _albumRepo;
        private IAlbumsPhotosRepository _albumsPhotosRepo;
        public AlbumsController(
            IAlbumRepository albumRepo,
            IAlbumsPhotosRepository albumsPhotoRepo)
        {
            _albumRepo = albumRepo;
            _albumsPhotosRepo = albumsPhotoRepo;
        }
        
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<Album>))]
        public IEnumerable<Album> Get()
        {
            return _albumRepo.FindAll();
        }

        [HttpGet("{id:int}")]
        [Produces("application/json", Type = typeof(Album))]
        public IActionResult Get(int id)
        {
            var album = _albumRepo.Find(id);

            if (album == null) {
                return NotFound();
            }

            return Json(album);
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(CreateAlbum))]
        [Consumes("application/json")]
        public IActionResult Post([FromBody]CreateAlbum album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = _albumRepo.Create(album);

            if (album.Photos.Any())
            {
                foreach(int photo in album.Photos)
                {
                    _albumsPhotosRepo.Create(new AlbumsPhotos() {
                        AlbumId = created,
                        PhotoId = photo
                    });
                }
            }
            
            return CreatedAtAction("Get", new { id = created }, _albumRepo.Find(created));
        }

        [HttpPut("{id:int}")]
        [Produces("application/json", Type = typeof(Album))]
        public IActionResult Put(int id, [FromBody]Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(_albumRepo.Update(id, album));
        }

        [HttpDelete("{id:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int id)
        {
            _albumRepo.Delete(id);
            var deleted = _albumRepo.Find(id);

            if (deleted != null)
            {
                return BadRequest();
            }

            return Json(new { Status = "OK" });
        }
    }
}
