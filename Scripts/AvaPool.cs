using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class AvaPool : MonoBehaviour
{
	public List<Rune> avapool;
	void Start()
	{
		var tp = GameObject.Find("TotalPool").GetComponent<TotalPool>();
		foreach(Rune rune in tp.totalpool.Runes)
		{
			if(rune.collection == "Base" || rune.collection ==  "Unlock")
			{
				avapool.Add(rune);
			}
		}
		//Debug.Log(avapool.Runes[0].name);
		//Debug.Log(avapool.Runes.Count);
	}
}
