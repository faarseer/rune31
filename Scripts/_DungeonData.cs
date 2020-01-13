using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

//Serialization of Attributes
public class _Dungeons
{
	public List<_Dungeon> Dungeons;
}

[Serializable]
public class _Dungeon
{
	[JsonProperty("name")]
	public string name {get;set;}
	
	[JsonProperty("map")]
	public string map {get;set;}
	
	[JsonProperty("sound")]
	public string sound {get;set;}

	[JsonProperty("enemy")]
	public Dictionary<string,string> enemy {get;set;}
}
