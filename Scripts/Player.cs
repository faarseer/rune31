using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class Player : MonoBehaviour
{
	//마법관통력, 마법저항력, 룬상성+값, 체력, 장비뭘끼는지, 
	
	Dictionary<string, float> rune_proficiency = new Dictionary<string,float>();

	[SerializeField]
	public float magic_penetration = 10.0f;
	public float magic_resistance = 10.0f;
	public float rune_superiority = 1.3f;
	public int health = 100;
	public _Magic present;
	public int sheild = 10;

	void Awake()
	{
		presentPlayer();
	}

	void Start()
	{
	}

	void Update()
	{
		StartCoroutine(IfDead());
		StartCoroutine(Spell());
	}

	void presentPlayer()
	{
		//플레이어가 자신의 캐릭터를 설정하면, 그게 json에 저장되고 불러옴.
		//GameData/Player,GameData/Character, GameData/Equipment
		string filepath = Application.dataPath + "/rune31/Scripts/GameData/";
		_Player Player_Data = JsonConvert.DeserializeObject<_Player>(File.ReadAllText(filepath + "Player.json"));
		_Equipments Equipments_Data =JsonConvert.DeserializeObject<_Equipments>(File.ReadAllText(filepath + "Equipment.json"));
		_Characters Characters_Data = JsonConvert.DeserializeObject<_Characters>(File.ReadAllText(filepath + "Character.json"));
		//---//
		_Character character = Characters_Data.Characters[Player_Data.Player.character]; 
		
		_Equipment wand = Equipments_Data.Equipments[Player_Data.Player.equipment["weapon"]];
		_Equipment robe = Equipments_Data.Equipments[Player_Data.Player.equipment["robe"]];
		rune_proficiency = Player_Data.Player.rune_proficiency;
		
		magic_penetration += characters.magic_penetration + wand.magic_penetration + robe.magic_penetration;
		magic_resistance += characters.magic_resistance + wand.magic_resistance + robe.magic_resistance;
		health += characters.health + wand.health + robe.health;
		//equipment character 특성들 다 저장해야됨.
	}

	IEnumerator Spell()
	{
		MagicSpace ms = GameObject.Find("MagicSpace").GetComponent<MagicSpace>();
		int en_health = GameObject.Find("Enemy").GetComponent<Enemy>().health;
		if((ms.chant % 3 ==0) & (ms.chant !=0))
		{
			present = ms.present;
			GameObject.Find("Enemy").GetComponent<Enemy>().health = en_health - SpellDamage();
		}
		yield return new WaitForSeconds(1.0f);
	}

	int SpellDamage()
	{
		Enemy en = GameObject.Find("Enemy").GetComponent<Enemy>();
		return (int) ((present.dmg - en.shield)*(1+(en.magic_resistance-magic_penetration)/100));
	}
	
	IEnumerator IfDead()
	{
		if(health == 0)
		{
			//SceneManagement.SceneManager.LoadScene("InGame_Lose");
		}
		yield return new WaitForSeconds(0.1f);
	}
}
