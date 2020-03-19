using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneHand : MonoBehaviour
{
	//수정후 확인할 점 : RuneHand 스크립트를 공유하는 9개의 오브젝트 문제.
	GetPool getPool;
	
	private Rune _presentRune;
	public Rune presentRune{
		get { return _presentRune; }
		set { _presentRune = value; }
	}
	private string _runeClass;
	public string runeClass{
		get { return _runeClass; }
		set { _runeClass = value; }
	}
	
	private Sprite _runeImg;
	public Sprite runeImg{
		get { return _runeImg; }
		set { _runeImg = value; }
	}
	
	private int _clickNum;
	public int clickNum{
		get { return _clickNum; }
		set { _clickNum = value; }
	}

	private List<Rune> runeList = new List<Rune>();
	
	public delegate void OnChangeMagicSpaceEvent(string rClass, Rune prRune);
	public event OnChangeMagicSpaceEvent OnChangeMagicSpace;
	
	public delegate void OnChangeRuneCompoEvent(string rClass, Rune prRune, int clNum);
	public event OnChangeRuneCompoEvent OnChangeRuneCompo;
	
	[SerializeField]
	public string rc;

	void Awake()
	{
		//GetPool singleton
		getPool = GetPool.instance;
		//GetPool.OnChangeRune event subscribe
		GetPool.OnChangeRuneHand += OnChangeRuneHand;
	}

	void Start()
	{
		_runeClass = rc;
		runeList = getPool.runePool[_runeClass];
		ChangeNextRune();
		ChangeRuneHandView();
		_clickNum = 0;
	}

	void OnChangeRuneHand(GameObject g)
	{
		if(g == gameObject)
		{
			_clickNum += 1;
			OnChangeMagicSpace(_runeClass, _presentRune);
			OnChangeRuneCompo(_runeClass, _presentRune, _clickNum);
			
			//change presentRune
			ChangeNextRune();
			ChangeRuneHandView();
		}
	}
	
	void ChangeNextRune()
	{
		System.Random rand = new System.Random();
		int index = rand.Next(runeList.Count);
		_presentRune = runeList[index];
		Debug.Log("RuneListCount:\t"+runeList.Count+"\tRuneClass:"+_runeClass+"\tindex:"+index+"\tgameObject:"+gameObject+"\trunename:"+_presentRune.name);
	}
	
	void ChangeRuneHandView()
	{
		_runeImg = Resources.Load(_presentRune.sprite, typeof(Sprite)) as Sprite;
		GetComponent<SpriteRenderer>().sprite = _runeImg;
	}
}
