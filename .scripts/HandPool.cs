using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPool : AvaPool, MonoBehaviour
{
	public HandDic = new Dictionary<string, Rune>();
	public RuneProb = Prob(); // avapool 에서 매 게임마다 prob() method 를 돌려서 새로운 dic<string, float> 을 만들어내야 함.
	
	// hand 에 있는 rune 이 사용될때 trigger, switch new Rune into hand & moving effect to magicspace.
	// trigger 나 새로운 룬 스프라이트를 switch 하는 것, moving effet 등이 유니티 monobehaviour 에 있다고 생각함.
}
