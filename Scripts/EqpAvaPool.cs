using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

public class EqpAvaPool : MonoBehaviour
{
	EqpTotalPool totalPool;

	public static EqpAvaPool instance;

	public List<_Equipment> avaPool;
	
	void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		
		totalPool = EqpTotalPool.instance;

		avaPool = 
			(from item in totalPool.totalPool
			where (item.collection == "Base" & item.collection == "UnLock")
			select item).ToList();
	}
}
