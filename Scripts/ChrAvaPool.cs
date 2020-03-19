using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

public class ChrAvaPool : MonoBehaviour
{
	ChrTotalPool totalPool;

	public static ChrAvaPool instance;

	public Dictionary<string,_Character> avaPool;
	
	void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		
		totalPool = ChrTotalPool.instance;
		
		avaPool = totalPool.totalPool
					.Where(chr => chr.Value.collection == "Base" | chr.Value.collection == "UnLock")
					.ToDictionary(	chr => chr.Key,
									chr => chr.Value);
		
		Debug.Log("ChrAvaPool"+"\n"+avaPool.Keys.Count);

	}
}
