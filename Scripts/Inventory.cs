using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region Singleton
	public static Inventory instance;
	private void Awake()
	{
		if(instance != null) // null 이 아니면, 이 게임오브젝트 없앰.
		{
			Destroy(gameObject);
			return;
		}
		instance = this; // null이고 게임오브젝트 클래스로 인스턴스 만듦.
	}
	#endregion
	
	//delegate 를 사용해서 slotCnt의 값이 변경되면 다른 곳에서 알수 있도록
	public delegate void OnSlotCountChange(int val); // delegate 선언
	public OnSlotCountChange onSlotCountChange0; // delegate 인스턴스화

	private int slotCnt; // 슬롯의 개수를 정할 int 변수 선언
	public int SlotCnt
	{
		get => slotCnt; // get { return slotCnt; } 랑 같음
		set
		{
			slotCnt = value;
		//	onSlotCountChange0 = new OnSlotCountChange(onlyreturn); // set 안에서 delegate 호출
			onSlotCountChange0(slotCnt);
			//delegate.Invoke(parameter) 는 delegate(parameter)랑 같다.
		}
	}
	
	private void onlyreturn(int val)
	{
	}

	public void Start()
	{
		slotCnt = 4;
	}
}

