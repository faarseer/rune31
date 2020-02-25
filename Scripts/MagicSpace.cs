using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class MagicSpace : MonoBehaviour
{
	public bool onmagic{get;set;}
	private string filepath;
	private string jsons;
	public Dictionary<string,Dictionary<string,Dictionary<string,_Magic>>> avamagic = new Dictionary<string,Dictionary<string,Dictionary<string,_Magic>>>();
	public Dictionary<string,Rune> magicspace = new Dictionary<string,Rune>();
	public int chant {get;set;}
	public _Magic present;
	
	private GameObject MagicLoad;
	
	[SerializeField]
	private GameObject MagicViewer;
	
	//public Component touchrune; 
	// 여기서의 문제점. touchrune 스크립트를 여러개가 공유하는데, 어떻게 지정을 하는지?
	// 생각되는 방법으로는 각각의 오브젝트마다 다 지정을하는건데, 이러면 public static으로 만든 이벤트에 영향이?
	// 그리고 매우 불편한데, 그러면 touchrune을 하나의 오브젝트(runeset)으로 해서 만들어야하는지?
	void Awake()
	{
		//touchrune.Onchangerune += Onchangerune;	
	}
	
	void Onchangerune(string rc, Rune pr)
	{
		if(magicspace.ContainsKey(rc))
		{
			magicspace[rc] = pr;
		}
		else
		{
			magicspace.Add(rc, pr);
		}
		chant += 1;
	}

	void Start()
	{
		filepath = Application.dataPath + "/rune31/Scripts/GameData/" + "new_Magic.json";
		jsons = File.ReadAllText(filepath);
		_Magics totalmagic = JsonConvert.DeserializeObject<_Magics>(jsons);
		
		Dictionary<string,List<Rune>> runepool = GameObject.Find("GetPool").GetComponent<GetPool>().runepool;
		foreach(string forme in totalmagic.Magics.Keys)
		{
			foreach(Rune Forme in runepool["Forme_Rune"])
			{
				if(forme == Forme.name)
					{
						avamagic.Add(forme, totalmagic.Magics[forme]);
					}
			}
		}
		chant = 0;
		onmagic = false;
		Debug.Log("runepool:\t"+runepool["Forme_Rune"].Count);
		Debug.Log("avamagic_count:\t"+avamagic.Count);
		magicspace.Add("Forme_Rune", default(Rune));
		magicspace.Add("Element_Rune", default(Rune));
		magicspace.Add("Cast_Rune", default(Rune));
		//List<string> avakeys = new List<string>(avamagic.Keys);
		//Debug.Log("keys?:\t"+avakeys[0]);
		//Debug.Log("magic?:\t"+avamagic["Circle"]["Fire"]["Rho"].name);
	}
	
	void Update()
	{
		//ParticleSystem ps = GetComponent<ParticleSystem>();
		if((chant % 3 == 0) & (chant !=0))
		{
			onmagic = true;	
			Invoke("onmagictofalse", 2.0f);
			StartCoroutine(Makemagic());
			Debug.Log(chant+"\tMakeMagic Done"+"\t"+present.name);
		}
	}
	
	//이것도 magicspace 를 다른 클래스로 저장해놓고 이벤트로 고친다?
	IEnumerator Makemagic()
	{
		bool ismagic = false;
		chant = 0;	
		if(ismagic)
		{
			yield return new WaitForSeconds(1.0f);
		}
		
		else	
		{
			if((magicspace["Forme_Rune"] != null) & (magicspace.ContainsKey("Forme_Rune")))
			{
				if(avamagic.ContainsKey(magicspace["Forme_Rune"].name))
				{
					string forme = magicspace["Forme_Rune"].name;
					if((magicspace["Element_Rune"] != null) & (magicspace.ContainsKey("Element_Rune"))) // magic element 쪽에 null dict 으로 만들면 안됨. 혹은 코드추가해야됨.
					{
						if(avamagic[forme].ContainsKey(magicspace["Element_Rune"].name))
						{
							ismagic = true;
							string element = magicspace["Element_Rune"].name;
							if((magicspace["Cast_Rune"] != null) & (magicspace.ContainsKey("Cast_Rune")))
							{
								if(avamagic[forme][element].ContainsKey(magicspace["Cast_Rune"].name))
								{
									string cast = magicspace["Cast_Rune"].name;
									present = avamagic[forme][element][cast];
									MagicView(present);
									chant = 0;
									magicspace["Forme_Rune"] = default(Rune);
									magicspace["Element_Rune"] = default(Rune);
									magicspace["Cast_Rune"] = default(Rune);
									Debug.Log(String.Format("chant magic : {0}", present.name));
									yield return new WaitForSeconds(1.0f);
								}
								else
								{
									present = avamagic[forme][element]["Rho"];
									MagicView(present);
									chant = 0;
									magicspace["Forme_Rune"] = default(Rune);
									magicspace["Element_Rune"] = default(Rune);
									magicspace["Cast_Rune"] = default(Rune);
									Debug.Log(String.Format("chant magic : {0}", present.name));
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
							Debug.Log("err by element"+magicspace["Element_Rune"].name);	
							MagicView(null,false);
							chant = 0;
							magicspace["Forme_Rune"] = default(Rune);
							magicspace["Element_Rune"] = default(Rune);
							magicspace["Cast_Rune"] = default(Rune);
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

	// 이것또한 magicviewer 를 클래스로 만들고 이벤트로 고친다
	// 필요성 1. 컴포넌트 추가해야하 것이 많음.
	// 필요성 2. 공격이나 수비에 들어가는 오브젝트를 따로 할거임.
	
	//public delegate void OnMagicViewEvent(_Magic magic, bool success);
	//public static event OnMagicViewEvent OnMagicView;

	void MagicView(_Magic magic, bool success = true)
	{
		if(!success)
		{
			//MagicLoad = Resources.Load("Prefabs/Magic/Fail");
			MagicLoad = Resources.Load("Prefabs/heal") as GameObject;
		}
		else
		{
			if(magic.prefab != null)
			{
				MagicLoad = Resources.Load(magic.prefab) as GameObject;
			}
			else
			{
				MagicLoad = Resources.Load("Prefabs/Fireball_Anim") as GameObject;
			}
		}
		MagicViewer = Instantiate(MagicLoad, new Vector3(-2,0,-8), Quaternion.identity);
		MagicViewer.name = magic.name;
		MagicViewer.transform.parent = this.transform;
		MagicViewer.AddComponent<BoxCollider2D>();
		MagicViewer.GetComponent<BoxCollider2D>().size = new Vector2(1.0f,1.0f);
		MagicViewer.AddComponent<MagicViewer>();
	}

	void onmagictofalse()
	{
		if(onmagic)
		{
			onmagic = false;
		}
	}
			//foreach(string forme in avamagic.Keys) // 똑같은 룬 계속 누를때에 대한 코딩 필요없는 이유.
			//{
			//	if(magicspace["Forme_Rune"].name == forme)
			//	{
			//		if(magicspace["Element_Rune"].name == magic.Element_Rune)
			//		{
			//			ismagic = true;
			//			if(magicspace["Cast_Rune"].name == magic.Cast_Rune) // elemental_cast 의한 강화마법
			//			{
			//				properties_change();//파티클 컴포넌트 체인지.
			//				present = magic;
			//				chant = 0; // magic rune 기본 만들어놓기.
			//				magicspace["Forme_Rune"] = default(Rune);
			//				magicspace["Element_Rune"] = default(Rune);
			//				magicspace["Cast_Rune"] = default(Rune);
			//				Debug.Log(String.Format("chant magic : {0}", present.name));
			//				break;
			//			}
			//			else if((magicspace["Cast_Rune"].name != "Fire_cast") &
			//					(magicspace["Cast_Rune"].name != "Ice_cast") &
			//					(magicspace["Cast_Rune"].name != "Lightning_cast") &
			//					(magicspace["Cast_Rune"].name != "Earth_cast") &
			//					(magicspace["Cast_Rune"].name != "Rho") &
			//					(magic.Cast_Rune == null)) // 그외 경우 지금 안되는거 : 같은 속성과 속성캐스트일때 마법나가야됨.
			//			{
			//				properties_change();
			//				present = magic;
			//				magicspace["Forme_Rune"] = default(Rune);
			//				magicspace["Element_Rune"] = default(Rune);
			//				magicspace["Cast_Rune"] = default(Rune);
			//				chant = 0;
			//				Debug.Log(String.Format("chant magic : {0}", present.name));
			//			}
			//			else // 강화마법 잘못 썼을때.
			//			{
			//				chant =0;
			//				Debug.Log(	"err by elemental_cast\n"+magicspace["Forme_Rune"].name
			//							+"\n"+magicspace["Element_Rune"].name
			//							+"\n"+magicspace["Cast_Rune"].name);
			//			}
			//		}
			//	}
			//}
			//if(!(ismagic))
			//{
			//	Debug.Log("cant chant magic");
			//	chant = 0;
			//	magicspace["Forme_Rune"] = default(Rune);
			//	magicspace["Element_Rune"] = default(Rune);
			//	magicspace["Cast_Rune"] = default(Rune);
			//}
}
