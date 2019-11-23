using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GetPool : MonoBehaviour
{
	public _Runes getpool;

	void Awake()
	{
		startgetpool();
	}

	void Addrune(Rune newrune)
	{
		getpool.Runes.Add(newrune); //한개 추가
	}

	void Poprune(Rune removerune)
	{
		getpool.Runes.Remove(removerune); //한개 삭제
	}
	
	void startgetpool()
	{
		// startrune 정하는 시스템 및 스프라이트 만들어야됨.
		AvaPool ap = GameObject.Find("AvaPool").GetComponent<AvaPool>();
		getpool = ap.avapool;
	}
}
