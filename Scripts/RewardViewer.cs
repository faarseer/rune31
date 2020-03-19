using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardViewer : MonoBehaviour
{
	private Dictionary<string,Dictionary<string,int>> reward;
	private bool isnew;

	[SerializeField]

	private void mouseDrag()
	{
	}

	private void Update()
	{
		//reward["rune_proficiency"]는 player data 로 저장? collection이 곧 playerdata가 되게.;
		//reward["rune"]는 새로운 룬이 있으면 collection 로 저장;
		//reward["rune"]는 얻은 모든 룬은 instance object로 위치지정 드래그가능하게;
		//reward["equips"]는 새로운 장비가 있으면 collection 로 저장;
		//reward["equips"]는 얻은 모든 장비는 instance object로 위치지정 드래그가능하게;
	}

	// 스크롤가능메소드
	// 너비 얼마로 할거냐.
}
