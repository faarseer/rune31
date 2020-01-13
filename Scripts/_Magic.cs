using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class _Magics
{
	public Dictionary<string,Dictionary<string,Dictionary<string,_Magic>>> Magics;
	//key: Forme_Rune / key: Element_Rune / key : cast_Rune if not ele_cast, all Rho
}

[Serializable]
public class _Magic
{
	[JsonProperty("mclass")]
	public string rclass {get;set;}
	
	[JsonProperty("name")]
	public string name {get;set;}

	[JsonProperty("collection")]
	public string collection {get;set;}

	[JsonProperty("description")]
	public string description {get;set;}
	
	[JsonProperty("Forme_Rune")]
	public string Forme_Rune {get;set;}

	[JsonProperty("Element_Rune")]
	public string Element_Rune {get;set;}
	
	[JsonProperty("Cast_Rune")]
	public string Cast_Rune {get;set;}
	
	[JsonProperty("dmg")]
	public float dmg {get;set;}
	
	[JsonProperty("duration")]
	public float duration {get;set;}
	
	[JsonProperty("channeling")]
	public bool channeling {get;set;}
	
	[JsonProperty("area")]
	public int area {get;set;}
	
	[JsonProperty("target")]
	public bool target {get;set;}
	
	[JsonProperty("condition")]
	public string condition {get;set;}
	
	[JsonProperty("cc")]
	public string cc {get;set;}
	
	[JsonProperty("prefab")]
	public string prefab {get;set;}
}
