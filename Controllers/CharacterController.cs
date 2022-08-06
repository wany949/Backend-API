using System;
using BackendAPI.Models;
using BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
	public CharacterController()
    {

    }

    // GET actions
    [HttpGet]
    public ActionResult<List<Character>> GetAll() =>
        CharacterService.GetAll();

    [HttpGet("{name}")]
    public ActionResult<Character> Get(string name)
    {
        var character = CharacterService.Get(name);

        if (character == null)
            return NotFound();

        return character;
    }

    // POST actions
    [HttpPost]
    public IActionResult Create(Character character)
    {
        CharacterService.Add(character);
        return CreatedAtAction(nameof(Create), new { name = character.Name }, character);
    }

    // PUT actions
    [HttpPut("{name}")]
    public IActionResult Update(string name, Character character)
    {
        if (name != character.Name)
            return BadRequest();

        var existingCharacter = CharacterService.Get(name);
        if (existingCharacter is null)
            return NotFound();

        CharacterService.Update(character);

        return NoContent();
    }

    // DELETE actions
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        var character = CharacterService.Get(name);

        if (character is null)
            return NotFound();

        CharacterService.Delete(name);

        return NoContent();
    }
}
