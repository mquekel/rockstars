using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rockstars.DataAccess.Repositories;
using Rockstars.DataAccess.Services;
using Rockstars.Domain.Entities;
using Rockstars.WebAPI.ViewModels;

namespace Rockstars.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SongController : Controller
    {
        private readonly IRepository<Song> _songRepository;

        private readonly ISongSearchService _songSearchService;

        public SongController(IRepository<Song> songRepository, ISongSearchService songSearchService)
        {
            _songSearchService = songSearchService;
            _songRepository = songRepository;
        }

        /// <summary>
        /// Get all songs.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Song> GetAll()
        {
            return this._songRepository.GetAll();
        }

        /// <summary>
        /// Search for an song by genre.
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpPost("Search")]
        public ActionResult Search(string genre)
        {
            var songs = this._songSearchService.SearchByGenre(genre);
            if (!songs.Any())
            {
                return NotFound(new Error("song not found."));
            }

            return Json(songs);
        }

        /// <summary>
        /// Gets the song by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var song = this._songRepository.Get(id);
            if (song == null)
            {
                return NotFound();
            }

            return Json(song);
        }

        /// <summary>
        /// Add an song to the database.
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] Song song)
        {
            this._songRepository.Create(song);
            return Accepted();
        }

        /// <summary>
        /// Add an array of songs to the database.
        /// </summary>
        /// <param name="songs"></param>
        /// <returns></returns>
        [HttpPost("AddMultiple")]
        public IActionResult AddMultiple([FromBody] IEnumerable<Song> songs)
        {
            try
            {
                this._songRepository.Create(songs);
            }
            catch (Exception e)
            {
                return BadRequest(new Error(e.Message));
            }

            return Accepted();
        }

        /// <summary>
        /// Updates an song.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="song"></param>
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Song song)
        {
            this._songRepository.Update(song);
        }

        /// <summary>
        /// Deletes the song.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this._songRepository.Delete(id);
        }
    }
}
