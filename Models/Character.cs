using System;
using System.Text.Json.Serialization;

namespace BackendAPI.Models;

public class Character
{

    [JsonPropertyName("rarity")]
	public int? Rarity { get; set; }
	[JsonPropertyName("name")]
	public string? Name { get; set; }
	[JsonPropertyName("vision")]
	public string? Vision { get; set; }
	[JsonPropertyName("weapon")]
	public string? Weapon { get; set; }
	[JsonPropertyName("constellation")]
	public string? Constellation { get; set; }
	[JsonPropertyName("birthday")]
	public string? Birthday { get; set; }
	
}
