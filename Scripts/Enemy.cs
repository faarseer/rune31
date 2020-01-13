using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class Enemy : MonoBehaviour
{
	//마법관통력, 마법저항력, 룬상성+값, 체력, 장비뭘끼는지, 
	
	[SerializeField]
	public int areanum; // 직접 설정 1~4
	public string _name;
	public float dmg;
	public float magic_penetration;
	public float magic_resistance;
	public int health;
	public float attackDmg;
	public float attackRate;
	public _Magic inherent;
	public Dictionary<string,float> reward = new Dictionary<string,float>();
	public int shield = 10;
    public Sprite Img;
	
	void Start()
	{
		Debug.Log(areanum);
		presentEnemy();
	}

	void Update()
	{
		StartCoroutine(IfDead());
		StartCoroutine(normalAttack());
	//	Spell();
	}

	void presentEnemy() // 특정 몬스터 불러오기 작업해야됨.
	{
		string filepath = Application.dataPath + "/rune31/Scripts/GameData/";
		_Enemies Enemypool = JsonConvert.DeserializeObject<_Enemies>(File.ReadAllText(filepath + "Enemy.json"));
		_Dungeons DungeonData = JsonConvert.DeserializeObject<_Dungeons>(File.ReadAllText(filepath + "DungeonData.json"));
		_Enemy present_enemy = Enemypool.Enemies[DungeonData.Dungeons[0].enemy[areanum.ToString()]];
		Debug.Log(present_enemy.name);
		_name = present_enemy.name;
		dmg = present_enemy.dmg;
		magic_penetration = present_enemy.magic_penetration;
		magic_resistance= present_enemy.magic_resistance;
		health = present_enemy.health;
		attackDmg = present_enemy.attackDmg;
		attackRate = present_enemy.attackRate;
		//inherent = present_enemy.magic; // 받아온 enemy.magic은 스트링이니까 매직json열어서 이름에 맞게 불러와라.
		reward = present_enemy.reward;
		Img = Resources.Load(String.Format("{0}",present_enemy.sprite), typeof(Sprite)) as Sprite;
		GetComponent<SpriteRenderer>().sprite = Img;
		//여기 그냥 리스트로 합쳐서 대입해라.	
	}

	IEnumerator normalAttack()
	{
		Player pl = GameObject.Find("Player").GetComponent<Player>();
		float pl_magic_resistance = pl.magic_resistance;
		// 노말어택에 대한 데미지 공식.
		yield return new WaitForSeconds(5.0f);
	}
	
	IEnumerator IfDead()
	{
		if(health == 0)
		{
			//SceneManagement.SceneManager.LoadScene("InGame_Win");
		}
		yield return new WaitForSeconds(0.1f);
	}

	IEnumerator Spell()
	{
		int pl_health = GameObject.Find("Player").GetComponent<Player>().health;
		GameObject.Find("Player").GetComponent<Player>().health = pl_health - SpellDamage();
		yield return new WaitForSeconds(10.0f);
	}
	
	int normalAttack_Dmg()
	{
		//노말어택 데미지 공식.
		return 1;
	}
	
	int SpellDamage()
	{
		Player pl = GameObject.Find("Player").GetComponent<Player>();
		//return (int) ((inherent.dmg - pl.sheild)*(1+(pl.magic_resistance - magic_penetration)/100));
		return 0;
	}
}
