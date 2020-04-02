using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

//Serialization of Attributes
public class _Enemies
{
	public Dictionary<string,_Enemy> Enemies;
}

[Serializable]
public class _Enemy
{
	[JsonProperty("name")]
	public string name {get;set;}
	
	[JsonProperty("dmg")]
	public int dmg {get;set;}
	
	[JsonProperty("magicPenetration")]
	public float magicPenetration {get;set;}

	[JsonProperty("magicResistance")]
	public float magicResistance {get;set;}
	
	[JsonProperty("health")]
	public int health {get;set;}
	
	[JsonProperty("attackDmg")]
	public int attackDmg {get;set;}
	
	[JsonProperty("attackRate")]
	public float attackRate {get;set;}
	
	[JsonProperty("property")]
	public string property {get;set;}

	[JsonProperty("magic")]
	public string magic {get;set;}
	
	[JsonProperty("magicCool")]
	public float mgcCool {get;set;}
	
	[JsonProperty("prefab")]
	public string prefab {get;set;}
	
	[JsonProperty("maxDrop")]
	public int maxDrop {get;set;}
	
	[JsonProperty("reward")]
	public Dictionary<string,int> reward {get;set;}
}
