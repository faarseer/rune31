using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GetPool : MonoBehaviour
{
	public List<Rune> getpool;
	public Dictionary<string,List<Rune>> runepool = new Dictionary<string, List<Rune>>();

	void Start()
	{
		startgetpool();
		Debug.Log("ele:"+runepool["Element_Rune"].Count+"\nforme"+
		runepool["Forme_Rune"].Count+"\ncast"+runepool["Cast_Rune"].Count);
		
	}

	void Addrune(Rune newrune)
	{
		getpool.Add(newrune); //한개 추가
	}

	void Poprune(Rune removerune)
	{
		getpool.Remove(removerune); //한개 삭제
	}
	
	void startgetpool()
	{
		// startrune 정하는 시스템 및 스프라이트 만들어야됨.
		AvaPool ap = GameObject.Find("AvaPool").GetComponent<AvaPool>();
		getpool = ap.avapool;
		List<Rune> elist = new List<Rune>();
		List<Rune> flist = new List<Rune>();
		List<Rune> clist = new List<Rune>();
		runepool.Add("Element_Rune", elist);
		runepool.Add("Forme_Rune", flist);
		runepool.Add("Cast_Rune", clist);
		foreach(Rune rune in getpool)
		{
			if(rune.rclass == "Element_Rune")
			{
				runepool["Element_Rune"].Add(rune);
			}
			if(rune.rclass == "Cast_Rune")
			{
				runepool["Cast_Rune"].Add(rune);
			}
			if(rune.rclass == "Forme_Rune")
			{
				runepool["Forme_Rune"].Add(rune);
			}
		}
	}
}
