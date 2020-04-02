using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

public class Enemy : MonoBehaviour
{
	//마법관통력, 마법저항력, 룬상성+값, 체력, 장비뭘끼는지, 
	
	Player player;
	EnmTotalPool enmTotalPool;
	MgcTotalPool mgcTotalPool;
	GameObject magicPrefab;
	Enemies enemies;

	private int _dmg;
	private int _attDmg;
	private float _attRate; //seconds
	private float _magicPenetration;
	private float _magicResistance;
	private EleSup _ele;
	private float _eleSup;
	private int _health;
	private int _sheild;
	private _Magic _inherentMagic;
	private _Enemy _presentEnemy;
	private Sprite _enemyImg;
	private float _mgcCool;
	private int _maxDrop;
	private Dictionary<string, int> _reward = new Dictionary<string, int>();

	List<_Magic> mgcPool;

	[SerializeField]
	public int areanum; // 직접 설정 1~4
	public int dmg{
		get { return _dmg; }
		set { _dmg = value; }
	}
	public float magicPenetration {
		get	{ return _magicPenetration; }
		set { _magicPenetration = value; }
	}
	public float magicResistance {
		get	{ return _magicResistance; }
		set { _magicResistance = value;}
	}
	public EleSup ele{
		get { return _ele; }
		set { _ele = value; }
	}
	public float eleSup{
		get { return _eleSup; }
		set { _eleSup = value; }
	}
	public int health {
		get { return _health; }
		set {
			_health = value;
			if(health == 0)
				{
					OnEnemyDead (_maxDrop, _reward);
					RemoveEnemy();
				}
		}
	}
	public int sheild {
		get { return _sheild; }
		set { _sheild = value; }
	}
	public _Magic inherentMagic {
		get { return _inherentMagic; }
		set { _inherentMagic = value; }
	}
	
	public int attDmg {
		get { return _attDmg; }
		set { _attDmg = value; }
	}
	public float attRate {
		get { return _attRate; }
		set { _attRate = value; }
	}
    public Sprite enemyImg {
		get { return _enemyImg; }
		set { _enemyImg = value; }
	}
	public float mgcCool {
		get { return _mgcCool; }
		set { _mgcCool = value; }
	}

	public Dictionary<string,float> reward = new Dictionary<string,float>();
	
	public GameObject magicViewer;	
	
	void Awake()
	{
		player = Player.instance;
		enmTotalPool = EnmTotalPool.instance;
		mgcTotalPool = MgcTotalPool.instance;
		magicViewer.GetComponent<MagicViewer>().OnHit += OnHitMagic;
		enemies = Enemies.instance;
	}
	void Start()
	{
		presentEnemy();
		//Debug.Log("Enemy Layer:"+gameObject.layer);
	}

	void Update()
	{
		//StartCoroutine(NormalAttack());
		if(_inherentMagic != null)
		{
			Debug.Log("Enemy Magic");
			StartCoroutine(SpellMagic());
		}
	}

	void presentEnemy() // 단순하게  고쳐라
	{	
		string filepath = Application.dataPath + "/rune31/Scripts/GameData/";
		List<_Dungeon> dungeon;
		dungeon = JsonConvert.DeserializeObject<_Dungeons>(File.ReadAllText(filepath + "DungeonData.json")).Dungeons;
		if(dungeon[0].enemy[areanum.ToString()] == null)
		{
			Destroy(gameObject);
		}
		else
		{
			AddEnemy();
			//Debug.Log("in Enemy"+enmTotalPool.totalPool.Keys.Count);
			_presentEnemy = enmTotalPool.totalPool[dungeon[0].enemy[areanum.ToString()]];
			
			List<_Magic> inhQuery =  
				(from mgc in mgcTotalPool.totalList
				where mgc.name == _presentEnemy.magic
				select mgc).ToList();
			
			Debug.Log("in Enemy inhQuery"+"\n"+inhQuery.Count);

			if(inhQuery.Count != 0)
			{
				_inherentMagic = inhQuery[0];
			}
			
			_enemyImg = Resources.Load(String.Format("{0}",_presentEnemy.prefab), typeof(Sprite)) as Sprite;
			GetComponent<SpriteRenderer>().sprite = _enemyImg;
			
			_magicPenetration = _presentEnemy.magicPenetration;
			_magicResistance = _presentEnemy.magicResistance;
			_health = _presentEnemy.health;
			_attDmg = _presentEnemy.attackDmg;
			_attRate = _presentEnemy.attackRate;
			_mgcCool = _presentEnemy.mgcCool;
			_maxDrop = _presentEnemy.maxDrop;
			_reward = _presentEnemy.reward;
		}
	}
	
	private void AddEnemy()
	{
		enemies.enmCnt++;
	}
	
	private void RemoveEnemy()
	{
		enemies.enmCnt--;
	}
		

	public delegate void OnNormalAttackEvent(int dmg);
	public OnNormalAttackEvent OnNormalAttack;
	
	IEnumerator NormalAttack()
	{
		OnNormalAttack(_attDmg); // Player가 구독해야됨.
		yield return new WaitForSeconds(_attRate);
	}
	
	void OnHitMagic(_Magic prMagic, GameObject beatObj, GameObject hitObj)
	{
		Player beater = beatObj.GetComponent<Player>();
		int deal = (int)(beater.dmg *(1.0+player.mdDmg));
		_health -= deal;
		// GetComponent<Animator>(); ....
		// dmg 공식 int dmg
		// health -= dmg;
	}
	
	public delegate void OnSpellMagicEvent (_Magic inhMagic);
	public OnSpellMagicEvent OnSpellMagic;
	
	public delegate void OnEnemyDeadEvent(int mxDrp, Dictionary<string, int> rwd);
	public OnEnemyDeadEvent OnEnemyDead;

	IEnumerator SpellMagic()
	{
		OnSpellMagic(_inherentMagic);
		yield return new WaitForSeconds(_mgcCool);
	}

	IEnumerator Spell()
	{
		int pl_health = GameObject.Find("Player").GetComponent<Player>().health;
		GameObject.Find("Player").GetComponent<Player>().health = pl_health - SpellDamage();
		yield return new WaitForSeconds(10.0f);
	}
	
	int SpellDamage()
	{
		Player pl = GameObject.Find("Player").GetComponent<Player>();
		//return (int) ((inherent.dmg - pl.sheild)*(1+(pl.magic_resistance - magic_penetration)/100));
		return 0;
	}
}
