using System.Collections.Generic;
using Alb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alb.Controllers
{
    [Route("api/albums/{albumId:int}/photos")]
    public class PhotosController : Controller
    {
        private IResourceRepository<Photo> _photosRepo;
        public PhotosController(IResourceRepository<Photo> photosRepo)
        {
            _photosRepo = photosRepo;
        }
        
        // GET api/albums/1/photos
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<Album>))]
        public IEnumerable<Photo> GetPhotos(int albumId)
        {
            return _photosRepo.FindAll(albumId);
        }

        // GET api/albums/1/photos/2
        [HttpGet("{id:int}")]
        [Produces("application/json", Type = typeof(Photo))]
        public IActionResult GetPhoto(int id)
        {
            var photo = _photosRepo.Find(id);

            if (photo == null) {
                return NotFound();
            }

            return Json(photo);
        }

        // POST api/albums/1/photos
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
            
            return CreatedAtAction("Get", new { id = created }, _photosRepo.Find(created));
        }

        // PUT api/albums/5/photos/1
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

        // DELETE api/albums/5/photos/2
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
