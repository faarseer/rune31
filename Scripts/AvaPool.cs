using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class AvaPool : MonoBehaviour
{
	TotalPool totalPool;	
	
	public static AvaPool instance;

	public List<Rune> avaPool;

	void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		
		totalPool = TotalPool.instance;

		foreach(Rune rune in totalPool.totalPool)
		{
			if(rune.collection == "Base" || rune.collection ==  "Unlock")
			{
				avaPool.Add(rune);
			}
		}
		//Debug.Log(avapool.Runes[0].name);
		//Debug.Log(avapool.Runes.Count);
	}

	void Start()
	{
		Debug.Log("runeAvaPool: "+avaPool.Count);	
	}
	
	void OnChangeAvaPool(List<Rune> pool)
	{
		//reward에 의한 lock -> unlock
	}
}
