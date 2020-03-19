using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class _Player
{
	[JsonProperty("playeName")]
	public string playerName {get;set;}
	
	[JsonProperty("character")]
	public string character {get;set;}
	
	[JsonProperty("equipment")]
	public Dictionary<string,string> equipment {get;set;}

	[JsonProperty("runeProficiency")]
	public Dictionary<string,float> runeProficiency {get;set;}
}
