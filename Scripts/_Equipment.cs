using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class _Equipments
{
	//public Dictionary<string,Dictionary<string,Dictionary<string,_Equipment>>> Equipment;
	public List<_Equipment> Equipment;
}

[Serializable]
public class _Equipment
{
	[JsonProperty("name")]
	public string name {get;set;}
	
	[JsonProperty("dmg")]
	public float dmg {get;set;}
	
	[JsonProperty("mdDmg")]
	public float mdDmg {get;set;}

	[JsonProperty("magicPenetration")]
	public float magicPenetration {get;set;}

	[JsonProperty("magicResistance")]
	public float magicResistance {get;set;}
	
	[JsonProperty("health")]
	public int health {get;set;}
	
	[JsonProperty("Rune")]
	public Dictionary<string, float> rune {get;set;}
	
	[JsonProperty("magicAttribute")]
	public Dictionary<string, Dictionary<string,object>> magicAttribute {get;set;}
	
	[JsonProperty("eleSuperiority")]
	public float eleSup {get;set;}
	
	[JsonProperty("description")]
	public string description {get;set;}
	
	[JsonProperty("img")]
	public string img {get;set;}
	
	[JsonProperty("collection")]
	public string collection {get;set;}
	
	[JsonProperty("tier")]
	public int tier {get;set;}
	
	[JsonProperty("sort")]
	public string sort {get;set;}
	
	[JsonProperty("set")]
	public string sett {get;set;}
}
