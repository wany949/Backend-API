﻿using System;
using BackendAPI.Models;
using BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
    private readonly HttpClient _httpClient;
	public CharacterController(IHttpClientFactory clientFactory)
    {
        if (clientFactory is null)
        {
            throw new ArgumentNullException(nameof(clientFactory));
        }
        _httpClient = clientFactory.CreateClient("genshin");
    }

    // GET actions
    [HttpGet]
    [Route("raw")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetGenshinCharacters()
    {
        var res = await _httpClient.GetAsync("characters");
        var content = await res.Content.ReadAsStringAsync();
        return Ok(content);
    }



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
