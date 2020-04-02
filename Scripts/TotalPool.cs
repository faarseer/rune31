using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class TotalPool : MonoBehaviour
{
	public static TotalPool instance;

	public List<Rune> totalPool;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		string filepath = Application.dataPath + "/rune31/Scripts/GameData/" + "Rune.json";
		string jsons = File.ReadAllText(filepath);
		var _totalPool = JsonConvert.DeserializeObject<_Runes>(jsons);
		totalPool = _totalPool.Runes;
		
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		
		//Debug.Log(totalpool.Runes[0].name);
		//Debug.Log(totalpool.Runes.Count);
	}
	
	void Start()
	{
		Debug.Log("runeTotalPool: "+totalPool.Count);	
	}
	void OnChangeTotalPool(List<Rune> pool)
	{
		// reward에 의한 lock -> unlock
	}
}
