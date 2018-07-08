using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rockstars.DataAccess.Repositories;
using Rockstars.Domain.Entities;
using Rockstars.WebAPI.ViewModels;

namespace Rockstars.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ArtistController : Controller
    {
        private readonly IRepository<Artist> _artistRepository;

        public ArtistController(IRepository<Artist> artistRepository)
        {
            this._artistRepository = artistRepository;
        }

        /// <summary>
        /// Get all artists.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Artist> GetAll()
        {
            return this._artistRepository.GetAll();
        }

        /// <summary>
        /// Search for an artist by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("Search")]
        public ActionResult Search(string name)
        {
            var artists = this._artistRepository.Search(q => string.Equals(q.Name, name, StringComparison.OrdinalIgnoreCase));
            var artist = artists.FirstOrDefault();
            if (artist == null)
            {
                return NotFound(new Error("artist not found."));
            }

            return Json(artist);
        }

        /// <summary>
        /// Gets the artist by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var artist = this._artistRepository.Get(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Json(artist);
        }

        /// <summary>
        /// Add an artist to the database.
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] Artist artist)
        {
            try
            {
                this._artistRepository.Create(artist);
            }
            catch (Exception e)
            {
                return BadRequest(new Error(e.Message));
            }
            return Accepted();
        }

        /// <summary>
        /// Add an array of artist to the database.
        /// </summary>
        /// <param name="artists"></param>
        /// <returns></returns>
        [HttpPost("AddMultiple")]
        public IActionResult AddMultiple([FromBody] IEnumerable<Artist> artists)
        {
            try
            {
                this._artistRepository.Create(artists);
            }
            catch (Exception e)
            {
                return BadRequest(new Error(e.Message));
            }

            return Accepted();
        }

        /// <summary>
        /// Updates an artist.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="artist"></param>
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Artist artist)
        {
            this._artistRepository.Update(artist);
        }
    }
}
