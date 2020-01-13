using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _Equipments
{
	public Dictionary<string,_Equipment> Equipments;
}

[Serializable]
public class _Equipment
{
	[JsonProperty("eclass")]
	public string eclass {get;set;}
	
	[JsonProperty("name")]
	public string name {get;set;}

	[JsonProperty("dmg")]
	public string dmg {get;set;}
	
	[JsonProperty("magic_penetration")]
	public float magic_penetration {get;set;}

	[JsonProperty("magic_resistance")]
	public float magic_resistance {get;set;}
	
	[JsonProperty("health")]
	public int health {get;set;}
	
	[JsonProperty("power")]
	public string power {get;set;}
	
	[JsonProperty("description")]
	public string description {get;set;}
}
