using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

public class Player : MonoBehaviour
{
	public static Player instance;

	EqpAvaPool eqpAvaPool;
	ChrAvaPool chrAvaPool;

	private int _dmg;
	private float _mdDmg;
	private float _magicPenetration;
	private float _magicResistance;
	private EleSup _ele;
	private float _eleSup;
	private int _health;
	private int _sheild;
	private _Magic _presentMagic;
	private _Character _presentChr;

	Dictionary<string, float> runeProficiency = new Dictionary<string,float>();
	Dictionary<string, _Equipment> presentEqp = new Dictionary<string, _Equipment>();

	[SerializeField]
	public int dmg{
		get { return _dmg; }
		set { _dmg = value; }
	}
	public float mdDmg{
		get { return _mdDmg; }
		set { _mdDmg = value; }
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
			if(health == 0) OnPlayerDead ();
		}
	}
	public int sheild {
		get { return _sheild; }
		set { _sheild = value; }
	}
	public _Magic presentMagic {
		get { return _presentMagic; }
		set { _presentMagic = value; }
	}
	
	public GameObject enemies;

	void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;

		eqpAvaPool = EqpAvaPool.instance;
		chrAvaPool = ChrAvaPool.instance;
		PresentPlayer();
		
		foreach(MagicViewer mv in enemies.GetComponentsInChildren<MagicViewer>())
		{
			mv.OnHit += OnMagicHit;
		}

	}

	public delegate void OnPlayerDeadEvent();
	public event OnPlayerDeadEvent OnPlayerDead;

	void Update()
	{
	}
	
	void PlayerDead()
	{
		//Dead_Sign
		//SceneManagement.SceneManager.LoadScene("InGame_Lose");
	}
	
	void OnMagicHit(_Magic prMagic, GameObject beatObj, GameObject hitObj)
	{
	}

	void PresentPlayer()
	{
		string filepath = Application.dataPath + "/rune31/Scripts/GameData/";
		_Player player = JsonConvert.DeserializeObject<_Player>(File.ReadAllText(filepath + "Player.json"));
		
		_presentChr = chrAvaPool.avaPool[player.character];

		foreach(string eqp in player.equipment.Keys)
		{
			foreach(_Equipment e in eqpAvaPool.avaPool)
			{
				if(e.name == player.equipment[eqp])
				{
					presentEqp.Add(eqp, e);
				}
			}
		}

		runeProficiency = player.runeProficiency;
		_magicPenetration = _presentChr.magicPenetration;
		_magicResistance = _presentChr.magicResistance;
		_health = _presentChr.health;

		var mpQuery = 
			(from eqp in presentEqp
			select eqp.Value.magicPenetration);

		var mrQuery = 
			(from eqp in presentEqp
			select eqp.Value.magicResistance);
		
		var hQuery = 
			(from eqp in presentEqp
			select eqp.Value.health);
		
		var mdDmgQuery = 
			(from eqp in presentEqp
			select eqp.Value.mdDmg);

		foreach(var q in mpQuery)
		{
			_magicPenetration += (float)q;
		}
		foreach(var q in mrQuery)
		{
			_magicResistance += (float)q;
		}
		foreach(var q in hQuery)
		{
			_health += (int)q;
		}
		foreach(var q in mdDmgQuery)
		{
			_mdDmg += (float)q;
		}
		_dmg = (int)((float)_dmg*(1.0+_mdDmg));
	}
}
