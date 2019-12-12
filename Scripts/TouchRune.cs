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
	Rune presentRune; 
	GetPool gp;
	int index;
	Dictionary<String,List<Rune>> runepool;
	public Sprite runeImg; // 이거는 랜덤하게.
	//public Vector2 touchPos; // 인터페이스에서 각각 직접 지정.
	bool gDraw = false;
	RaycastHit2D hit; // raycast로부터 정보를 얻기위해 사용되는 구조체
	Ray touchray; // origin 에서 시작해서 direction 방햐으로 나아가는 무한대 길이의 선.
	Vector3 firstpoint; // mouse firstpoint

	[SerializeField]
	public string runeclass; // 인터페이스에서 각각 직접 지정해줘야됨.
	
	void Start()
	{
		gp = GameObject.Find("GetPool").GetComponent<GetPool>();
		runepool = gp.runepool;
		getRune(true);
	}
	
	void Update()
	{
		runeTouch();
	}

	void runeTouch()
	{
		if (Input.GetMouseButtonDown(0)) //touchCount : 터치 GetMouseButton : 마우스
		{
			//Touch touch = Input.GetTouch(0); // 첫번째 인풋 터치 변수
			
			//switch(touch.phase)
			//{
			//	case TouchPhase.Began :
			//		Console.Write("touch");
			//		touchPos = touch.position;
			//		break;
			//	case TouchPhase.Moved :
			//		break;
			//	case TouchPhase.Ended :
			//		if ((Mathf.Abs(touch.position.x) < Mathf.Abs(touchPos.x) + 3.0) &&
			//			(Mathf.Abs(touch.position.y) < Mathf.Abs(touchPos.y) + 3.0))
			//		{
			//			break;
			//		}
			//		else
			//		{
			//			Console.Write("magicspace_coroutine");
			//			//magicspace(); // 현재 위치의 룬의 정보를 magicspace 로 옮김.
			//			getRune();
			//			break;
			//		}
			//}
			firstpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			Console.Write("magicspace_corutine");
			hit = Physics2D.Raycast(firstpoint, Vector2.zero); // Raycast(원점, 방향)

			if (hit.collider != null)
			{
				if (gDraw == false){
					gDraw = true;
				} else {
					gDraw = false;
				}
				Debug.Log(hit.collider.gameObject.name);
				Debug.Log(hit.collider.gameObject.GetComponent<TouchRune>().runeclass.ToString());
				Debug.Log(runepool[runeclass].Count);
				getRune();
			}
		}
	}

	void getRune(bool init = false)
	{
		System.Random rand = new System.Random();
		index = rand.Next(runepool[runeclass].Count);
		presentRune = runepool[runeclass][index];
		//Debug.Log(presentRune.sprite);
		runeImg = Resources.Load(String.Format("{0}",presentRune.sprite), typeof(Sprite)) as Sprite;
		if(init)
		{
			GetComponent<SpriteRenderer>().sprite = runeImg;
		}
		else
		{
			GameObject.Find(hit.collider.gameObject.name).GetComponent<SpriteRenderer>().sprite = runeImg;
		}
	}
}
