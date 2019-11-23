using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class AvaPool : MonoBehaviour
// MonoBehaviour 가 있어야 start() 돌아가는데, 
//TotalPool에서 totalrune 만드는거를 메서드로 하고,
//ITotalPool 만들어서 totalrune 만드는 메서드를 가져오는 방법이있음.
//근데 이렇게 했을때 TotalPool 에서 Awake()에 totalrune 만드는 메서드를 실행시켜야함?
// 또한 이것은 method 의 기본기능이 json을 읽어오는 것이기에 두번 읽는다는 단점.
// 그냥 TotalPool.totalrune 을 불러오면 json 을 한번만 읽어도되는데..
{
	public _Runes avapool;

	void Awake()
	{
		TotalPool tp = GameObject.Find("TotalPool").GetComponent<TotalPool>();
		
		foreach(Rune rune in tp.totalpool.Runes)
		{
		if(rune.collection == "Base" || rune.collection ==  "Unlock")
			{
			avapool.Runes.Add(rune);
			}
		}
		Debug.Log(avapool.Runes[0].name);
		Debug.Log(avapool.Runes.Count);
	}
}
