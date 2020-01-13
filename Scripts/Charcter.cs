using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _Characters
{
	public Dictionary<string,_Character> Characters;
}

[Serializable]
public class _Character
{
	[JsonProperty("name")]
	public string name {get;set;}

	[JsonProperty("dmg")]
	public float dmg {get;set;}
	
	[JsonProperty("magic_penetration")]
	public float magic_penetration {get;set;}

	[JsonProperty("magic_resistance")]
	public float magic_resistance {get;set;}
	
	[JsonProperty("rune_superiority")]
	public float rune_superiority {get;set;}
	
	[JsonProperty("health")]
	public int health {get;set;}
}
