using System;
using System.Diagnostics;
using System.Text.Json;
using BackendAPI.Models;
using BackendAPI.Data;
using Microsoft.AspNetCore.Mvc;



namespace BackendAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly ICharacterService _service;
	public CharacterController(IHttpClientFactory clientFactory)
    {
        if (clientFactory is null)
        {
            throw new ArgumentNullException(nameof(clientFactory));
        }
        _httpClient = clientFactory.CreateClient("genshin");
    }

    public CharacterController(ICharacterService service)
    {
        _service = service;
    }

    // GET from httpClient and store into CharacterService
    [HttpGet]
    [Route("raw")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetGenshinCharacters()
    {
        var res = await _httpClient.GetAsync("characters");
        var content = await res.Content.ReadAsStringAsync();
        string[] charList = JsonSerializer.Deserialize<string[]>(content);
        List<Character> characters = new List<Character>(); 

        foreach (string name in charList)
        {
            res = await _httpClient.GetAsync("characters/" + name);
            content = await res.Content.ReadAsStringAsync();
            Character character = JsonSerializer.Deserialize<Character>(content);
            characters.Add(character);
            _service.AddChar(character);
        }
        return Ok(characters);
    }

    // GET actions
    [HttpGet]
    public ActionResult<IEnumerable<Character>> GetAll()
    {
        IEnumerable<Character> characters = _service.GetAllCharacters();
        return Ok(characters);
    }

    [HttpGet("{name}")]
    public ActionResult<Character> Get(string name)
    {
        var character = _service.GetCharByName(name);

        if (character == null)
            return NotFound();

        return character;
    }

    // POST actions
    [HttpPost]
    public IActionResult Create(Character character)
    {
        _service.AddChar(character);
        return CreatedAtAction(nameof(Create), new { name = character.Name }, character);
    }

    // PUT actions
    [HttpPut("{name}")]
    public IActionResult Update(string name, Character character)
    {
        if (name != character.Name)
            return BadRequest();

        var existingCharacter = _service.GetCharByName(name);
        if (existingCharacter is null)
            return NotFound();

        return NoContent();
    }

    // DELETE actions
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        var character = _service.GetCharByName(name);

        if (character is null)
            return NotFound();


        return NoContent();
    }
}
