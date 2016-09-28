using System.Collections.Generic;
using System.Linq;
using Alb.Models;
using Alb.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Alb.Controllers
{
    [Route("api/[controller]")]
    public class PhotosController : Controller
    {
        private IPhotoRepository _photosRepo;
        private IAlbumsPhotosRepository _albumsPhotosRepo;
        private IHostingEnvironment _hostingEnv;
        public PhotosController(
            IPhotoRepository photosRepo, 
            IHostingEnvironment hostingEnv,
            IAlbumsPhotosRepository albumsPhotosRepo)
        {
            _photosRepo = photosRepo;
            _hostingEnv = hostingEnv;
            _albumsPhotosRepo = albumsPhotosRepo;
        }
        
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<Album>))]
        public IEnumerable<Photo> Get()
        {
            return _photosRepo.FindAll();
        }

        [HttpGet("{id:int}")]
        [Produces("application/json", Type = typeof(Photo))]
        public IActionResult Get(int id)
        {
            var photo = _photosRepo.Find(id);

            if (photo == null) {
                return NotFound();
            }

            return Json(photo);
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(Photo))]
        [Consumes("application/json")]
        public IActionResult Post([FromBody]Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = _photosRepo.Create(photo);

            if (photo.Albums.Any())
            {
                foreach(int album in photo.Albums)
                {
                    _albumsPhotosRepo.Create(new AlbumsPhotos() {
                        AlbumId = album,
                        PhotoId = created
                    });
                }
            }
            
            return CreatedAtAction("Get", new { id = created }, _photosRepo.Find(created));
        }

        [HttpPut("{id:int}")]
        [Produces("application/json", Type = typeof(Photo))]
        public IActionResult Put(int id, [FromBody]Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(_photosRepo.Update(id, photo));
        }

        [HttpDelete("{id:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int id)
        {
            _photosRepo.Delete(id);
            var deleted = _photosRepo.Find(id);

            if (deleted != null)
            {
                return BadRequest();
            }

            return Json(new { Status = "OK" });
        }
    }
}
