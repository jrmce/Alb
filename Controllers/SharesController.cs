using System;
using System.Collections.Generic;
using Alb.Models;
using Alb.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Alb.Controllers
{
    [Route("api/[controller]")]
    public class SharesController : Controller
    {
        private IHostingEnvironment _hostingEnv;
        private IShareRepository _shareRepo;
        public SharesController(
            IHostingEnvironment hostingEnv,
            IShareRepository shareRepo)
        {
            _hostingEnv = hostingEnv;
            _shareRepo = shareRepo;
        }
        
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<Album>))]
        public IEnumerable<Share> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{token:Guid}")]
        [Produces("application/json", Type = typeof(Share))]
        public IActionResult Get(Guid token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(Share))]
        [Consumes("application/json")]
        public IActionResult Post([FromBody]CreateShare share)
        {
            var id = _shareRepo.Create(share);
            var created = _shareRepo.Find(id);

            return CreatedAtAction("Get", new { token = created.Token }, created);
        }

        [HttpPut("{id:int}")]
        [Produces("application/json", Type = typeof(Share))]
        public IActionResult Put(int id, [FromBody]Share share)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
