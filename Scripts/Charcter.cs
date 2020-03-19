using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _Characters
{
	public Dictionary<string,_Character> Character;
}

[Serializable]
public class _Character
{
	[JsonProperty("name")]
	public string name {get;set;}

	[JsonProperty("dmg")]
	public int dmg {get;set;}
	
	[JsonProperty("magicPenetration")]
	public float magicPenetration {get;set;}

	[JsonProperty("magicResistance")]
	public float magicResistance {get;set;}
	
	[JsonProperty("runeSuperiority")]
	public float runeSuperiority {get;set;}
	
	[JsonProperty("health")]
	public int health {get;set;}
	
	[JsonProperty("collection")]
	public string collection {get;set;}
}
