using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace BackendAPI.Models
{
	public class Character
	{

		[Key]
        [Required]
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
		[JsonPropertyName("rarity")]
		public int? Rarity { get; set; }
	}

}


