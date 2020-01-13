using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

//Serialization of Attributes
public class _Enemies
{
	public Dictionary<string,_Enemy> Enemies = new Dictionary<string,_Enemy>();
}

[Serializable]
public class _Enemy
{
	[JsonProperty("name")]
	public string name {get;set;}
	
	[JsonProperty("dmg")]
	public float dmg {get;set;}
	
	[JsonProperty("magic_penetration")]
	public float magic_penetration {get;set;}

	[JsonProperty("magic_resistance")]
	public float magic_resistance {get;set;}
	
	[JsonProperty("health")]
	public int health {get;set;}
	
	[JsonProperty("attackDmg")]
	public float attackDmg {get;set;}
	
	[JsonProperty("attackRate")]
	public float attackRate {get;set;}
	
	[JsonProperty("property")]
	public string property {get;set;}

	[JsonProperty("magic")]
	public string magic {get;set;}
	
	[JsonProperty("sprite")]
	public string sprite {get;set;}
	
	[JsonProperty("reward")]
	public Dictionary<string,float> reward {get;set;}
}
