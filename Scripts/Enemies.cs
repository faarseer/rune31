using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour
{
	private int _enmCnt;
	public static Enemies instance;

	public int enmCnt{
		get { return _enmCnt; }
		set { _enmCnt = value;
				if(enmCnt == 0) AllEnmDead();
		}
	}
	
	void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}

	void AllEnmDead()
	{
	}
}
