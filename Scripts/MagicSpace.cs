using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class MagicSpace : MonoBehaviour
{
	private string filepath;
	private string jsons;
	public Dictionary<string,Dictionary<string,Dictionary<string,_Magic>>> avamagic = new Dictionary<string,Dictionary<string,Dictionary<string,_Magic>>>();
	public Dictionary<string,Rune> magicspace = new Dictionary<string,Rune>();
	public int chant {get;set;}
	public _Magic present;
	private GameObject MagicViewer;
	private GameObject Rune_Compo_Viewer;
	
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
		Debug.Log("runepool:\t"+runepool["Forme_Rune"].Count);
		Debug.Log("avamagic_count:\t"+avamagic.Count);
		//List<string> avakeys = new List<string>(avamagic.Keys);
		//Debug.Log("keys?:\t"+avakeys[0]);
		//Debug.Log("magic?:\t"+avamagic["Circle"]["Fire"]["Rho"].name);
	}
	
	void Update()
	{
		//ParticleSystem ps = GetComponent<ParticleSystem>();
		if((chant % 3 == 0) & (chant !=0))
		{
			StartCoroutine(Makemagic());
			Debug.Log(chant+"\tMakeMagic Done"+"\t"+present.name);
		}
	}
			
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
			if(avamagic.ContainsKey(magicspace["Forme_Rune"].name))
			{
				string forme = magicspace["Forme_Rune"].name;
				if(avamagic[forme].ContainsKey(magicspace["Element_Rune"].name)) // magic element 쪽에 null dict 으로 만들면 안됨. 혹은 코드추가해야됨.
				{
					ismagic = true;
					string element = magicspace["Element_Rune"].name;
					if(avamagic[forme][element].ContainsKey(magicspace["Cast_Rune"].name))
					{
						string cast = magicspace["Cast_Rune"].name;
						present = avamagic[forme][element][cast];
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
					Debug.Log("err by element"+magicspace["Element_Rune"].name);	
					chant = 0;
					magicspace["Forme_Rune"] = default(Rune);
					magicspace["Element_Rune"] = default(Rune);
					magicspace["Cast_Rune"] = default(Rune);
					yield return new WaitForSeconds(1.0f);
				}
			}
		}
	}
	
	IEnumerator Rune_Compo_View()
	{
		Rune_Compo_Viewer = new GameObject();
		Rune_Compo_Viewer.transform.parent = this.transform;
		yield return new WaitForSeconds(1.0f);
	}

	IEnumerator MagicView()
	{
		MagicViewer = new GameObject();
		MagicViewer.transform.parent = this.transform;

		MagicViewer.AddComponent<BoxCollider2D>();
		yield return new WaitForSeconds(1.0f);
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

	void properties_change()
	{
	}
}
