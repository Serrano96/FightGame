using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FightGame;
using System;

namespace FightGameApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET api/players
        [HttpGet]
        public IActionResult Get()
        {     
            var result = _playerService.GetPlayers();
            return new ObjectResult(result);   
        }

        // GET api/players/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var player = _playerService.GetPlayerById(id);
                return new ObjectResult(player);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(new { Error = $"El jugador con id {id} no existe" });
            }
        }

        // POST api/players
        [HttpPost]
        public IActionResult Post([FromBody]Player player)
        {
            _playerService.AddPlayer(player);
            return new CreatedResult($"api/players/{player.Id}",player);
        }

        // PUT api/players/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Player player)
        {
            try
            {
                if (id != player.Id)
                {
                    return BadRequest();
                }
                _playerService.UpdatePlayer(player);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

        // DELETE api/players/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _playerService.Delete(id);
                return new OkResult();
            }
            catch (Exception e)
            {
                return NotFound();
            }
          
        }
    }
}
