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
        private IPhotoRepository _photoRepo;
        private IAlbumsPhotosRepository _albumsPhotosRepo;
        private IHostingEnvironment _hostingEnv;
        public PhotosController(
            IPhotoRepository photoRepo, 
            IHostingEnvironment hostingEnv,
            IAlbumsPhotosRepository albumsPhotosRepo)
        {
            _photoRepo = photoRepo;
            _hostingEnv = hostingEnv;
            _albumsPhotosRepo = albumsPhotosRepo;
        }
        
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<Album>))]
        public IEnumerable<Photo> Get()
        {
            return _photoRepo.FindAll();
        }

        [HttpGet("{id:int}")]
        [Produces("application/json", Type = typeof(Photo))]
        public IActionResult Get(int id)
        {
            var photo = _photoRepo.Find(id);

            if (photo == null) {
                return NotFound();
            }

            return Json(photo);
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(CreatePhoto))]
        [Consumes("application/json")]
        public IActionResult Post([FromBody]CreatePhoto photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = _photoRepo.Create(photo);

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
            
            return CreatedAtAction("Get", new { id = created }, _photoRepo.Find(created));
        }

        [HttpPut("{id:int}")]
        [Produces("application/json", Type = typeof(Photo))]
        public IActionResult Put(int id, [FromBody]Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(_photoRepo.Update(id, photo));
        }

        [HttpDelete("{id:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int id)
        {
            _photoRepo.Delete(id);
            var deleted = _photoRepo.Find(id);

            if (deleted != null)
            {
                return BadRequest();
            }

            return Json(new { Status = "OK" });
        }
    }
}
