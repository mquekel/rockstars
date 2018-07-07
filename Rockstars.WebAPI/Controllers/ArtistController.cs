﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public IActionResult Add([FromBody] Artist artist)
        {
            this._artistRepository.Create(artist);
            return Accepted();
        }
    }
}