﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Rockstars.DataAccess.Repositories;
using Rockstars.Domain.Entities;

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
            this._artistRepository.Create(artist);
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
            foreach (var artist in artists)
            {
                this._artistRepository.Create(artist);
            }

            return Accepted();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Artist artist)
        {
            this._artistRepository.Update(artist);
        }
    }
}
