using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GetPool : MonoBehaviour
{
	AvaPool avaPool;

	#region Singleton
	public static GetPool instance;
	private void Awake()
	{
		//DontDestroyOnLoad(gameObject);
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;

		avaPool = AvaPool.instance;
	}
	#endregion

	public List<Rune> getPool;
	public Dictionary<string,List<Rune>> runePool = new Dictionary<string, List<Rune>>();
	
	public delegate void OnChangeRuneHandEvent(GameObject g);
	public static event OnChangeRuneHandEvent OnChangeRuneHand;
	
	private Vector3 firstPoint;

	void Start()
	{
		startgetpool();
		Debug.Log("ele:"+runePool["Element_Rune"].Count+"\nforme"+
		runePool["Forme_Rune"].Count+"\ncast"+runePool["Cast_Rune"].Count);
	}
	
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			firstPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		if(Input.GetMouseButtonUp(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(firstPoint, Vector3.zero);
			if (hit.collider.tag == "RuneHand")
			{
				OnChangeRuneHand(hit.transform.gameObject);
			}
		}
	}
	
	void startgetpool()
	{
		// startrune 정하는 시스템 및 스프라이트 만들어야됨.
		getPool = avaPool.avaPool;
		runePool.Add("Element_Rune", new List<Rune>());
		runePool.Add("Forme_Rune", new List<Rune>());
		runePool.Add("Cast_Rune", new List<Rune>());
		foreach(Rune rune in getPool)
		{
			if(rune.rclass == "Element_Rune")
			{
				runePool["Element_Rune"].Add(rune);
			}
			if(rune.rclass == "Cast_Rune")
			{
				runePool["Cast_Rune"].Add(rune);
			}
			if(rune.rclass == "Forme_Rune")
			{
				runePool["Forme_Rune"].Add(rune);
			}
		}
	}

	void OnChangeGetPool(List<Rune> pool)
	{
		//reward 에 의한 lock -> unlock
	}
}
