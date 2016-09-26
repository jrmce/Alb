using System;
using System.Collections.Generic;
using System.IO;
using Alb.Models;
using Alb.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public IEnumerable<Photo> GetPhotos()
        {
            return _photosRepo.FindAll();
        }

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

        [HttpPost]
        [Produces("application/json", Type = typeof(Photo))]
        [Consumes("multipart/form-data")]
        public IActionResult UploadPhotos()
        {
            var files = Request.Form.Files;
            string albumId = null;

            if (Request.Form.ContainsKey("albumId"))
            {
                albumId = Request.Form["albumId"];
            }
            
            var createdPhotos = new List<Photo>();

            foreach (var file in files)
            {
                var ext = System.IO.Path.GetExtension(file.FileName);
                var guid = Guid.NewGuid();
                var filename = $"{guid}{ext}";
                var path = $"{_hostingEnv.ContentRootPath}/uploads";
                var newFile = $"{path}/{filename}";
                
                using (FileStream fs = System.IO.File.Create(newFile))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                var photo = new Photo()
                {
                    Filename = filename
                };

                photo.Id = _photosRepo.Create(photo);
                createdPhotos.Add(photo);
            }

            if (!string.IsNullOrEmpty(albumId))
            {
                var albumsPhotos = new List<AlbumsPhotos>();

                createdPhotos.ForEach(photo => {
                    albumsPhotos.Add(new AlbumsPhotos()
                    {
                        AlbumId = Int32.Parse(albumId),
                        PhotoId = photo.Id
                    });
                });

                albumsPhotos.ForEach(ap => {
                    _albumsPhotosRepo.Create(ap);
                });
            }

            return Json(createdPhotos);
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
