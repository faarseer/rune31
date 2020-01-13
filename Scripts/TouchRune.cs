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
	private Rune _presentRune; 
	public Rune presentRune
	{
		get
		{
			return _presentRune;
		}
		set
		{
			_presentRune = value;
		}
	}
	private GetPool gp;
	// private int index;
	private List<Rune> runelist = new List<Rune>();
	private Sprite runeImg; // 이거는 랜덤하게.
	//public Vector2 touchPos; // 인터페이스에서 각각 직접 지정.
	private bool gDraw;
	private RaycastHit2D hit; // raycast로부터 정보를 얻기위해 사용되는 구조체
	private Ray touchray; // origin 에서 시작해서 direction 방햐으로 나아가는 무한대 길이의 선.
	private Vector3 firstpoint; // mouse firstpoint
	private bool istouch;
	//private string runeclass;	
	public string runeclass
	{
		get
		{
			if(rune_num <3)
			{
				return "Forme_Rune";
			}
			if(rune_num >2 && rune_num < 5)
			{
				return "Element_Rune";
			}
			if(rune_num > 4)
			{
				return "Cast_Rune";
			}
			else
			{
				return null;
			}
		}
		private set
		{
			runeclass = value;
		}

	}// 인터페이스에서 각각 직접 지정해줘야됨.
	
	[SerializeField]
	public int rune_num;
	
	void Start()
	{
		gp = GameObject.Find("GetPool").GetComponent<GetPool>();
		runelist = gp.runepool[runeclass];
		//Debug.Log("runeclass:"+runeclass+"\n"+"object:"+gameObject.name);
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
			//	}
			//}
			firstpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//runeImg = Resources.Load(String.Format("{0}",presentRune.prsd_sprite), typeof(Sprite)) as Sprite;
			//istouch = true;
			//if(istouch)
			//{
				//hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = runeImg;
			//}
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			//istouch = false;
			hit = Physics2D.Raycast(firstpoint, Vector3.zero); // Raycast(원점, 방향)
			gDraw = false;
			if (hit.collider != null)
			{
				if (hit.collider.gameObject == this.gameObject)
				{
					if (gDraw == false){
						gDraw = true;
					} else {
						gDraw = false;
					}
					//Debug.Log("thisis,"+gameObject.name); // 이거는 여러개 나오고
					//Debug.Log("touched runeis,"+hit.collider.gameObject.name); // 이거는 하나 나오고
					GameObject ob= hit.collider.gameObject;
					//Debug.Log("selected ob:"+ob.name);//이거는 한개 나옴 ㅋㅋ
					//Debug.Log("ob.runeclass:"+ob.GetComponent<TouchRune>().runeclass);
					//Debug.Log("runelistcount"+ob.GetComponent<TouchRune>().runelist.Count);//이거 세개 풀 다나오는데?
					Debug.Log("pressed rune : "+ob.GetComponent<TouchRune>()._presentRune.name);
					MagicSpace ms = GameObject.Find("MagicSpace").GetComponent<MagicSpace>();
					if(ms.magicspace.ContainsKey(runeclass))
					{
						ms.magicspace[runeclass] = _presentRune;
					}
					else
					{
						ms.magicspace.Add(runeclass, _presentRune);
					}
					ms.chant += 1;
					Debug.Log("chant:"+ms.chant);
					Debug.Log("which class, rune:"+runeclass+"\t"+_presentRune.name);
					ob.GetComponent<TouchRune>().getRune();
				}
			}
		}
	}

	void getRune(bool nohit = false)
	{
		System.Random rand = new System.Random();
		int index = rand.Next(runelist.Count);
		_presentRune = runelist[index];
		//Debug.Log("object"+this.gameObject.name+"\nindex:"+index+"\ncount:"+runelist.Count+"\nrunename:"+runelist[index].name);
		runeImg = Resources.Load(String.Format("{0}",_presentRune.sprite), typeof(Sprite)) as Sprite;
		if(nohit)
		{
			GetComponent<SpriteRenderer>().sprite = runeImg;
		}
		else
		{
			GetComponent<SpriteRenderer>().sprite = runeImg;
		}
	}
}
