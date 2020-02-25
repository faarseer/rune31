using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardList : MonoBehaviour
{
	public List<string> rewardlist = new List<string>();

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}
}
