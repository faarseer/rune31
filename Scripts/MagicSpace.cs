using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class MagicSpace : MonoBehaviour
{
	MgcTotalPool mgcTotalPool;

	private bool _onMagic = false;
	public bool onMagic{
		get { return _onMagic; }
		set { _onMagic = value; }
	}
	
	private int _chant = 0;
	public int chant{
		get { return _chant; }
		set { _chant = value; }
	}
	
	private _Magic _presentMagic;
	public _Magic presentMagic{
		get { return _presentMagic; }
		set { _presentMagic = value; }
	}

	public Dictionary<string,Dictionary<string,Dictionary<string,_Magic>>> avaMagic = new Dictionary<string,Dictionary<string,Dictionary<string,_Magic>>>();
	public Dictionary<string,Rune> magicSpace = new Dictionary<string,Rune>();
	
	public delegate void OnChangeMagicViewEvent(_Magic magic, bool success);
	public event OnChangeMagicViewEvent OnChangeMagicView;

	[SerializeField]
	public GameObject RuneHands;
	
	//public Component touchrune; 
	// 여기서의 문제점. touchrune 스크립트를 여러개가 공유하는데, 어떻게 지정을 하는지?
	// 생각되는 방법으로는 각각의 오브젝트마다 다 지정을하는건데, 이러면 public static으로 만든 이벤트에 영향이?
	// 그리고 매우 불편한데, 그러면 touchrune을 하나의 오브젝트(runeset)으로 해서 만들어야하는지?
	
	void Awake()
	{
		Component[] runeHands = RuneHands.GetComponentsInChildren<RuneHand>();
		foreach (RuneHand runeHand in runeHands)
		{
			runeHand.OnChangeMagicSpace += OnChangeMagicSpace;
		}
		
		mgcTotalPool = MgcTotalPool.instance;

	}

	void Start()
	{
		Dictionary<string,List<Rune>> runePool = GameObject.Find("GetPool").GetComponent<GetPool>().runePool;
		foreach(string forme in mgcTotalPool.totalPool.Keys)
		{
			foreach(Rune Forme in runePool["Forme_Rune"])
			{
				if(forme == Forme.name)
					{
						avaMagic.Add(forme, mgcTotalPool.totalPool[forme]);
					}
			}
		}
		
		Debug.Log("runepool:\t"+runePool["Forme_Rune"].Count);
		Debug.Log("avamagic_count:\t"+avaMagic.Count);
		magicSpace.Add("Forme_Rune", default(Rune));
		magicSpace.Add("Element_Rune", default(Rune));
		magicSpace.Add("Cast_Rune", default(Rune));
		//List<string> avakeys = new List<string>(avamagic.Keys);
		//Debug.Log("keys?:\t"+avakeys[0]);
		//Debug.Log("magic?:\t"+avamagic["Circle"]["Fire"]["Rho"].name);
	}
	
	void Update()
	{
		Debug.Log("chant:"+_chant);
		//ParticleSystem ps = GetComponent<ParticleSystem>();
		if((_chant % 3 == 0) & (_chant !=0))
		{
			_onMagic = true;	
			StartCoroutine(MakeMagic());
			//Debug.Log(_chant+"\tMakeMagic Done"+"\t"+_presentMagic.name);
			Invoke("MakeonMagicFalse", 10.0f);
		}
	}
	
	void OnChangeMagicSpace(string rClass, Rune prRune)
	{
		if(magicSpace.ContainsKey(rClass))
		{
			magicSpace[rClass] = prRune;
		}
		else
		{
			magicSpace.Add(rClass, prRune);
		}
		
		_chant += 1;
	}
	
	//이것도 magicspace 를 다른 클래스로 저장해놓고 이벤트로 고친다?
	IEnumerator MakeMagic()
	{
		bool isMagic = false;
		_chant = 0;	
		if(isMagic)
		{
			yield return new WaitForSeconds(1.0f);
		}
		
		else	
		{
			if((magicSpace["Forme_Rune"] != null) & (magicSpace.ContainsKey("Forme_Rune")))
			{
				if(avaMagic.ContainsKey(magicSpace["Forme_Rune"].name))
				{
					string forme = magicSpace["Forme_Rune"].name;
					if((magicSpace["Element_Rune"] != null) & (magicSpace.ContainsKey("Element_Rune"))) // magic element 쪽에 null dict 으로 만들면 안됨. 혹은 코드추가해야됨.
					{
						if(avaMagic[forme].ContainsKey(magicSpace["Element_Rune"].name))
						{
							isMagic = true;
							string element = magicSpace["Element_Rune"].name;
							if((magicSpace["Cast_Rune"] != null) & (magicSpace.ContainsKey("Cast_Rune")))
							{
								if(avaMagic[forme][element].ContainsKey(magicSpace["Cast_Rune"].name))
								{
									string cast = magicSpace["Cast_Rune"].name;
									_presentMagic = avaMagic[forme][element][cast];
									Debug.Log("chant magic :" + _presentMagic.name);
									OnChangeMagicView(_presentMagic, true);
									_chant = 0;
									magicSpace["Forme_Rune"] = default(Rune);
									magicSpace["Element_Rune"] = default(Rune);
									magicSpace["Cast_Rune"] = default(Rune);
									yield return new WaitForSeconds(1.0f);
								}
								else
								{
									_presentMagic = avaMagic[forme][element]["Rho"];
									Debug.Log("chant magic :" + _presentMagic.name);
									OnChangeMagicView(_presentMagic, true);
									_chant = 0;
									magicSpace["Forme_Rune"] = default(Rune);
									magicSpace["Element_Rune"] = default(Rune);
									magicSpace["Cast_Rune"] = default(Rune);
									yield return new WaitForSeconds(1.0f);
								}
							}
							else
							{
								Debug.Log("need Cast");
								yield return new WaitForSeconds(1.0f);
							}
						}
						else
						{
							Debug.Log("err by element"+magicSpace["Element_Rune"].name);	
							OnChangeMagicView(null,false);
							chant = 0;
							magicSpace["Forme_Rune"] = default(Rune);
							magicSpace["Element_Rune"] = default(Rune);
							magicSpace["Cast_Rune"] = default(Rune);
							yield return new WaitForSeconds(1.0f);
						}
					}
					else
					{
						Debug.Log("need element");
						yield return new WaitForSeconds(1.0f);
					}
				}
				else
				{
					Debug.Log("u dont have this forme");
					yield return new WaitForSeconds(1.0f);
				}
			}
			else
			{
				Debug.Log("need forme");
				yield return new WaitForSeconds(1.0f);
			}
		}
	}

	void MakeonMagicFalse()
	{
		if(_onMagic)
		{
			_onMagic = false;
		}
	}
}
