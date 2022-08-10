using System;
using System.Diagnostics;
using System.Text.Json;
using BackendAPI.Models;
using BackendAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackendAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class FoodController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IFoodService _service;
    public FoodController(IHttpClientFactory clientFactory, IFoodService service)
    {
        if (clientFactory is null)
        {
            throw new ArgumentNullException(nameof(clientFactory));
        }
        if (service is null)
        {
            throw new ArgumentNullException(nameof(service));
        }
        _service = service;
        _httpClient = clientFactory.CreateClient("genshin");
    }

    [NonAction]
    public string ReplaceName(string name)
    {
        string foodName = name.Replace("é", "e").Replace(",", "").Replace("&", "")
                .Replace("'", "").Replace("  ", "-").Replace(" ", "-").ToLower();

        return foodName;
    }
    
    [NonAction]
    public string ReplaceHTML(string html, int temp, string baseURL, string name, Food food)
    {
        html = html.Replace("{{name" + temp + "}}", food.Name).Replace("{{type" + temp + "}}", food.Type).Replace("{{effect" + temp + "}}", food.Effect)
                .Replace("{{description" + temp + "}}", food.Description).Replace("{{rarity" + temp + "}}", food.Rarity.ToString())
                .Replace("{{imgsrc" + temp + "}}", baseURL + name);

        return html;
    }
    
    [NonAction]
    public string ReturnHTML(List<Food> meal)
    {
        var baseURL = "https://api.genshin.dev/consumables/food/";
        var html = System.IO.File.ReadAllText(@"./assets/index.html");

        for (int i = 0; i < meal.Count; i++)
        {
            Food food = _service.GetFoodByName(meal[i].Name);
            int temp = i + 1;
            string foodName = ReplaceName(food.Name);
            html = ReplaceHTML(html, temp, baseURL, foodName, food);
        }
        return html;
    }

    // GET from httpClient and store into database
    [HttpGet]
    [Route("raw")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetGenshinFood()
    {
        List<Food> foodList = new();
        var res = await _httpClient.GetAsync("/consumables/food");
        var content = await res.Content.ReadAsStringAsync();
        JObject foodListResponse = JsonConvert.DeserializeObject<JObject>(content);

        foreach (KeyValuePair<string, JToken> property in foodListResponse)
        {
            Food food = JsonConvert.DeserializeObject<Food>(property.Value.ToString());
            foodList.Add(food);
            _service.AddFood(food);
        }

        return Ok(foodList);
    }

    // GET actions
    [HttpGet]
    [Route("get_meal")]
    [ProducesResponseType(200)]
    public List<Food> Get3CourseMeal()
    {
        List<Food> meal = _service.Get3CourseMeal();
        return meal;
    }

    [HttpGet]
    [Route("get_meal_HTML")]
    [ProducesResponseType(200)]
    public ActionResult GetHTML()
    {
        List<Food> meal = Get3CourseMeal();
        var html = ReturnHTML(meal);
        return base.Content(html, "text/html");
    }


    [HttpGet]
    public IEnumerable<Food> GetAll()
    {
        IEnumerable<Food> food = _service.GetAllFood();
        return food;
    }

    [HttpGet("{name}")]
    public ActionResult<Food> Get(string name)
    {
        Food food = _service.GetFoodByName(name);

        if (food == null)
            return NotFound();

        return food;
    }

    // POST actions
    [HttpPost]
    public IActionResult Create(Food food)
    {
        _service.AddFood(food);
        return CreatedAtAction(nameof(Create), new { name = food.Name }, food);
    }

    // PUT actions
    [HttpPut("{name}")]
    public IActionResult Update(string name, Food food)
    {
        if (name != food.Name)
            return BadRequest();

        var existingFood = _service.GetFoodByName(name);
        if (existingFood is null)
            return NotFound();

        _service.UpdateFood(food);

        return NoContent();
    }

    // DELETE actions
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        Food food = _service.GetFoodByName(name);

        if (food is null)
            return NotFound();
        _service.RemoveFood(food);

        return NoContent();
    }
}
