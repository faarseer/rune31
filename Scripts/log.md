total pool : lock, unlock 모든 rune이 존재하는 pool
ava pool : unlock 된 rune 이 존재하는 pool -> json 에서 unlock된 rune 불러오기, basic rune
get pool : 인게임에서 핸드로 갈 수 있게 얻어진 rune pool 
hand pool : hand 8장에 지금 나온 rune pool

20191217
hit collider 두개 받냐?

20200227
RuneHand.cs 수정중...
GetRune 메서드를 이벤트로 바꾸게 되면, 감시를 어떻게 할지랑, TotalPool...GetPool 까지를 먼저 수정해야할듯.

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

20200229
runehands -0.04 -1.02
runehand1 -2.62
runehand2 -1.96
runehand3 -1.3
runehand4 -0.64
runehand5 0.02
runehand6 0.68
runehand7 1.34
runehand8 2
runehand9 2.66

20200302
reward이벤트에 의한 OnChangeTotalPool, OnChangeAvaPool, OnChangeGetPool eventmethod
equipment는 따로 풀을 만들어야되네 얘도.
totalequip, avaequip, get은 필요없네.근데 지금끼고 있는게 뭔지..presentpool
object수정하면서 json도 수정하게 만들어야됨. -> total 수정 후 전체 다시 읽는 event 만들어야됨.
이를 위해서는 데미지 시스템 선행하는게 좋음.

