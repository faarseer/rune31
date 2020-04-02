using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class _Runes
{
	public List<Rune> Runes;
}

[Serializable]
public class Rune
{
	[JsonProperty("rclass")]
	public string rclass {get;set;}
	
	[JsonProperty("name")]
	public string name {get;set;}

	[JsonProperty("collection")]
	public string collection {get;set;}

	[JsonProperty("description")]
	public string description {get;set;}
	
	[JsonProperty("tier")]
	public int tier {get;set;}
	
	[JsonProperty("rewardProb")]
	public int rewardProb {get;set;}
	
	[JsonProperty("sprite")]
	public string sprite {get;set;}
	
	[JsonProperty("prsd_sprite")]
	public string prsd_sprite {get;set;}

	[JsonProperty("buff")]
	public Dictionary<string,object> buff {get;set;}
}
