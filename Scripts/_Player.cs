using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class _Player
{
	[JsonProperty("character")]
	public string character {get;set;}
	
	[JsonProperty("equipment")]
	public Dictionary<string,string> equipment {get;set;}

	[JsonProperty("rune_proficiency")]
	public Dictionary<string,float> rune_proficiency {get;set;}
}
