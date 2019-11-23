using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//touchrune을 8개에 대해 하나를 쓸지
//각각에 적용해야할지.
//각각에 적용하자.
public class TouchRune : MonoBehaviour
{
	bool istouch = false;
	public Vector2 touchPos;
	public Sprite runesImg;
	Rune presentRune; 
	public GetPool gp;
	int index;

	[SerializeField]
	public string runeclass;

	public TouchRune(string rc)
	{
		runeclass = rc;
	}
	
	void Awake()
	{
		gp = GameObject.Find("GetPool").GetComponent<GetPool>();
		System.Random rand = new System.Random();
		index = rand.Next(gp.getpool.Runes.Count);
		presentRune = gp.getpool.Runes[index];
		//getRune(presentRune);
	}
	
	void Update()
	{
		runeTouch();
	}
	
	void runeTouch()
	{
		if (Input.touchCount >0)
		{
			Touch touch = Input.GetTouch(0);
			RaycastHit obj; // 이렇게 하면 현재 게임오브젝트를 뜻할수 있는거야?
			
			Ray touchray = Camera.main.ScreenPointToRay(touch.position);
			
			Physics.Raycast (touchray, out obj);
			
			if (obj.collider != null)
			{
				if (istouch == false)
				{
					istouch = true;
					
					switch(touch.phase)
					{
						case TouchPhase.Began :
							Console.Write("touch");
							touchPos = touch.position;
							break;
						case TouchPhase.Moved :
							break;
						case TouchPhase.Ended :
							if ((Mathf.Abs(touch.position.x) < Mathf.Abs(touchPos.x) + 3.0) &&
								(Mathf.Abs(touch.position.y) < Mathf.Abs(touchPos.y) + 3.0))
							{
								break;
							}
							else
							{
								Console.Write("magicspace_coroutine");
								Debug.Log(presentRune);
								//magicspace(); // 현재 위치의 룬의 정보를 magicspace 로 옮김.
								System.Random rand = new System.Random();			
								index = rand.Next(gp.getpool.Runes.Count);
								presentRune = gp.getpool.Runes[index];
								//getRune(presentRune); // getpool 에서 하나를 가져옴.
								break;
							}
						// 근데 move 되고 end 되는거아님?? 그니까 end를 위에 두 상황에 다 넣어야됨.
					}
				}
				else
				{
					istouch = false;	
				}
			}
		}
	}

	//void getRune(Rune newrune)
	//{
	//	RuneImg = Resources.Load<Sprite>(newrune.runePath);
	//	GetComponent(SpriteRenderer).sprite = RuneImg;
	//}
}
