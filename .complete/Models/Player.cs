using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player {
	public Hero hero;
	public Dictionary<string,Equipment> equipments = new Dictionary<string,Equipment>();
	public float penetration;
	public float resistance;
	public Crowd_Control cc = new CC (); // 이거는 CC내의 method 를 계산하고 나오는 상태이상 값을 받아야 할것같음.
	// 인게임에서 가지고 있는 룬세팅

	public Player(Hero hero, Dictionary equipments, float penetration, float resistance){
	this.hero = hero
	this.equipments = equipments
	this.penetration = penetration
	this.resistance = resistance
	}

}
