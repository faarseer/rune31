using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RewardSystem : MonoBehaviour
{
	public static RewardSystem instance;

	AvaPool avaPool;
	EqpAvaPool eqpAvaPool;
	
	Player player;
	public GameObject enemies;
	
	public List<Rune> rewardRune = new List<Rune>();
	public List<_Equipment> rewardEqp = new List<_Equipment>();

	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;

		DontDestroyOnLoad(this);
		
		avaPool = AvaPool.instance;
		eqpAvaPool = EqpAvaPool.instance;
		foreach(Enemy enemy in enemies.GetComponentsInChildren<Enemy>())
		{
			enemy.OnEnemyDead += OnChangeRewardRune;
			enemy.OnEnemyDead += OnChangeRewardEqp;
		}
	}

	void OnChangeRewardRune(int mxDrp, Dictionary<string, int> rwd)
	{
		for(int i=0;i< mxDrp;i++)
		{
			int rnd = UnityEngine.Random.Range(1,100);
			foreach(KeyValuePair<string,int> obj in rwd)
			{
				if(rnd < obj.Value)
				{
					if(obj.Key != "0")
					{
						List<Rune> _runeList;
						_runeList =
							(from rune in avaPool.avaPool
							where (rune.tier == Int32.Parse(obj.Key))
							select rune).ToList();
						int ind = new System.Random().Next(_runeList.Count);
						rewardRune.Add(_runeList[ind]);
						break; // 한번 되면 forloop 깨짐.
					}
				}
			}
		}
	}
	
	void OnChangeRewardEqp(int mxDrp, Dictionary<string, int> rwd)
	{
		for(int i=0;i< mxDrp;i++)
		{
			int rnd = UnityEngine.Random.Range(1,100);
			foreach(KeyValuePair<string,int> obj in rwd)
			{
				if(rnd < obj.Value)
				{
					if(obj.Key != "0")
					{
						List<_Equipment> _eqpList;
						_eqpList =
							(from eqp in eqpAvaPool.avaPool
							where (eqp.tier == Int32.Parse(obj.Key))
							select eqp).ToList();
						int ind = new System.Random().Next(_eqpList.Count);
						rewardEqp.Add(_eqpList[ind]);
						break;
					}
				}
			}
		}
	}
}
