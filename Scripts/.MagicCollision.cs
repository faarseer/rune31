using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCollision : MonoBehaviour
{
	public delegate void OnStartDamageSystemEvent(GameObject beater, GameObject beaten);
	public event OnStartDamageSystemEvent OnStartDamageSystem;

	void OnParticleCollision(GameObject other)
	//파티클이 콜라이더와 충돌하면 이벤트 발생함.
	{
		BoxCollider2D collider = other.GetComponent<BoxCollider2D>();
		if(collider)
		{
			OnStartDamageSystem(gameObject, other);
			//gameObject는 마법일것이고, 그의 parent에 player나 enemy가 존재하도록.
		}
	}
}

public class DamageSystem : MonoBehaviour
{
	//event subscribe 하는 것을 MagicCollision이 만들어질때 코드로 구현해야됨.
	// MagicPool도 싱글톤으로 만들어라 걍.
	int DecideEleSup(EleSup beaterSup, EleSup beatenSup, float sup)
	{
		// fire, ice, lightning, earth
		if(beaterSup == EleSup.Fire & beatenSup == EleSup.Earth)
		{
			return sup;
		}
		else
		{
			if((int)beaterSup > (int)beatenSup)
			{
				return sup;
			}
			if((int)beaterSup < (int)beatenSup)
			{
				return 1.0/sup;
			}
			if((int)beaterSup == (int)beatenSup)
			{
				return 1.0;
			}
		}
	}

	void OnStartDamageSystem(GameObject beater, GameObject beaten)
	{
		gameObject beaterPt = beater.transform.parent.parent.parent;
		gameObject beatenPt= beater.transform.parent.parent.parent;
		if(beaterPt.tag == "Enemy" & beatenPt.tag == "Player")
		{
			EnemytoPlayerDamageSystem(beaterPt, beatenPt,beater);
		}
		if(beaterPt.tag == "Player" & beatenPt.tag == "Enemy")
		{
			PlayertoEnemyDamageSystem(beaterPt, beatenPt,beater);
		}
		if(beaterPt.tag == "Enemy" & beatenPt.tag == "DefMagic")
		{
			EnemytoDefMagicDamageSystem(beaterPt, beatenPt,beater);
		}
		if(beaterPt.tag == "Player" & beatenPt.tag == "DefMagic")
		{
			EnemytoDefMagicDamageSystem(beaterPt, beatenPt,beater);
		}
	}

	void EnemytoPlayerDamageSystem(GameObject attenemy, GameObject hitplayer, GameObject magicPrefab)
	{
	}
	void PlayertoEnemyDamageSystem(GameObject attplayer, GameObject hitenemy, GameObject magicPrefab)
	{
		Player player = attplayer.GetComponent<Player>();
		Enemy enemy = hitenemy.GetComponent<Enemy>();
		//Magic magic = singleton magic에서 찾아오기.
		//int dmg = (player.dmg * (1+player.mdDmg)) * (
	}
	void EnemytoDefMagicDamageSystem(GameObject attenemy, GameObject hitmagic, GameObject magicPrefab)
	{
	}
	void PlayertoDefMagicDamageSystem(GameObject attplayer, GameObject hitmagic, GameObject magicPrefab)
	{
	}
}
