using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventoryUI : MonoBehaviour
{
	Inventory inven;

	public GameObject inventoryPanel;
	bool isactive = false;
	public Button[] slots;
	public Transform slotHolder;
	private List<string> rewardlist;
	
	private void Start()
	{
		inven = Inventory.instance;
		//rewardlist = GameObject.Find("Reward").GetComponent<RewardList>().rewardlist;	
		foreach(Transform child in inventoryPanel.transform)
		{
			Debug.Log("inventorychild:" + child.gameObject.name);
		}
		slots = slotHolder.GetComponentsInChildren<Button>();
		
		inven.onSlotCountChange0 += SlotChange;
		inventoryPanel.SetActive(isactive);
	}
	
	private void SlotChange(int val)
	{
		for(int i = 0; i< slots.Length; i++)
		{
			if(i < inven.SlotCnt)
				slots[i].interactable = true;
			else
				slots[i].interactable = false;
		}
	}
	
	public void AddSlot()
	{
		//child = Instantiate(Resources.Load("Prefabs/slot"), new Vector3(0,0,0), Quaternion.identity);
		//child.transform.parent = slotHolder;
		inven.SlotCnt++;
	}
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			isactive = !isactive;
			inventoryPanel.SetActive(isactive);

		}
	}
}
