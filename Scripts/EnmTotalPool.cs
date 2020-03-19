using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

public class EnmTotalPool : MonoBehaviour
{
	public static EnmTotalPool instance;

	//public Dictionary<string,Dictionary<string,Dictionary<string,_Equipment>>> totalPool;
	public Dictionary<string,_Enemy> totalPool;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		
		DontDestroyOnLoad(gameObject);
		string filepath = Application.dataPath + "/rune31/Scripts/GameData/" + "Enemy.json";
		string jsons = File.ReadAllText(filepath);
		totalPool = JsonConvert.DeserializeObject<_Enemies>(jsons).Enemies;
		//Debug.Log("in EnmTotalPool"+"\n"+totalPool.Keys.Count);
	}
}
