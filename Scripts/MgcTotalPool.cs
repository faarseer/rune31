using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

public class MgcTotalPool : MonoBehaviour
{
	public static MgcTotalPool instance;

	public Dictionary<string,Dictionary<string,Dictionary<string,_Magic>>> totalPool;
	public List<_Magic> totalList;

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		
		DontDestroyOnLoad(gameObject);
		string filepath = Application.dataPath + "/rune31/Scripts/GameData/" + "newMagic.json";
		string jsons = File.ReadAllText(filepath);
		totalPool = JsonConvert.DeserializeObject<_Magics>(jsons).Magics;
		
		totalList = (from f in totalPool
					from e in f.Value
					from c in e.Value
					select c.Value).ToList();
		Debug.Log("in MgcTotalPool : totalList" + "\n" + totalList.Count);
	}
	
	void OnChangeTotalPool(List<_Equipment> pool)
	{
		// reward에 의한 lock -> unlock
	}
}
