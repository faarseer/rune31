using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class TotalPool : MonoBehaviour
{
	private string filepath;
	private string jsons;
	public _Runes totalpool;

	void Awake()
	{
		filepath = Application.dataPath + "/rune31/Scripts/GameData/" + "Rune.json";
		jsons = File.ReadAllText(filepath);
		totalpool = JsonConvert.DeserializeObject<_Runes>(jsons);
		//Debug.Log(totalpool.Runes[0].name);
		//Debug.Log(totalpool.Runes.Count);
	}
}
