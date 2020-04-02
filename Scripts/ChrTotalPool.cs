using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

public class ChrTotalPool : MonoBehaviour
{
	public static ChrTotalPool instance;

	//public Dictionary<string,Dictionary<string,Dictionary<string,_Equipment>>> totalPool;
	public Dictionary<string,_Character> totalPool;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		
		DontDestroyOnLoad(gameObject);
		string filepath = Application.dataPath + "/rune31/Scripts/GameData/" + "Character.json";
		string jsons = File.ReadAllText(filepath);
		totalPool = JsonConvert.DeserializeObject<_Characters>(jsons).Character;
	}
	
	void Start()
	{
		Debug.Log("ChrTotalPool :"+totalPool.Keys.Count);
	}

	void OnChangeTotalPool(List<_Character> pool)
	{
		// reward에 의한 lock -> unlock
	}
}
