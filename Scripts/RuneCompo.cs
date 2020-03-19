using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneCompo : MonoBehaviour
{
	private GameObject runeCompoView;
	
	[SerializeField]
	public GameObject RuneHands;
	public MagicViewer magicViewer;

	void Awake()
	{
		Component[] runeHands = RuneHands.GetComponentsInChildren<RuneHand>();
		foreach (RuneHand runeHand in runeHands)
		{
			runeHand.OnChangeRuneCompo += OnChangeRuneCompo;
		}
		magicViewer.OnDestroyRuneCompoView += OnDestroyRuneCompoView;
	}

	void OnChangeRuneCompo(string rClass, Rune prRune, int clNum)
	{
		Debug.Log("RuneCompo Change");
		Destroy(runeCompoView);
		ChangeRuneCompo(rClass, prRune, clNum);
		//준석아 이거 프리팹 어케되냐.
		//ChangeRuneCompo();
		//ChangeRUneCompoView();
	}
	
	void ChangeRuneCompo(string rClass, Rune prRune, int clNum)
	{
		//바뀐 프리팹이 그냥 추가하는거라면, clNum을 받아서 만들면됨.
		if(rClass == "Forme_Rune")
		{
			runeCompoView = Instantiate(Resources.Load("Prefabs/Rune_Circle/Rune_1piece") as GameObject, 
									transform.position, Quaternion.identity);
			runeCompoView.transform.parent = transform;
			//child의 sprite renderer presentrune으로 변경.
		}
		if(rClass == "Element_Rune")
		{
			runeCompoView = Instantiate(Resources.Load("Prefabs/Rune_Circle/Rune_2pieces") as GameObject, 
									transform.position, Quaternion.identity);
			runeCompoView.transform.parent = transform;
			//child의 sprite renderer presentrune으로 변경.
		}
		if(rClass == "Cast_Rune")
		{
			runeCompoView = Instantiate(Resources.Load("Prefabs/Rune_Circle/Rune_3pieces") as GameObject, 
									transform.position, Quaternion.identity);
			runeCompoView.transform.parent = transform;
			//child의 sprite renderer presentrune으로 변경.
		}
		
		for(int i=0; i< runeCompoView.transform.childCount; i++)
		{
			runeCompoView.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>().Play(true);
		}
	}
	
	void OnDestroyRuneCompoView()
	{
		Destroy(runeCompoView);
	}
}
