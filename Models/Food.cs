using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


namespace BackendAPI.Models
{
	public class Food
	{
        [Key]
        [JsonPropertyName("name")]
		public string? Name { get; set; }
		[JsonPropertyName("type")]
		public string? Type { get; set; }
		[JsonPropertyName("effect")]
		public string? Effect { get; set; }
		[JsonPropertyName("description")]
		public string? Description { get; set; }
		[JsonPropertyName("rarity")]
		public int? Rarity { get; set; }
	}

}


