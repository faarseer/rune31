using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

public class EqpTotalPool : MonoBehaviour
{
	public static EqpTotalPool instance;

	//public Dictionary<string,Dictionary<string,Dictionary<string,_Equipment>>> totalPool;
	public List<_Equipment> totalPool;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		
		DontDestroyOnLoad(gameObject);
		string filepath = Application.dataPath + "/rune31/Scripts/GameData/" + "Equipment.json";
		string jsons = File.ReadAllText(filepath);
		totalPool = JsonConvert.DeserializeObject<_Equipments>(jsons).Equipment;
	}
	
	void OnChangeTotalPool(List<_Equipment> pool)
	{
		// reward에 의한 lock -> unlock
	}
}
