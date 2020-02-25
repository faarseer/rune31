using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RuneDeckUI : MonoBehaviour
{
	public GameObject RuneDeckPanel;
	public Button[] slots;
	public Transform slotHolder;
	private List<string> runedecklist;

	private void Start()
	{
		slots = slotHolder.GetComponentsInChildren<Button>();
		
		//inven.onSlotCountChange0 += SlotChange;
		//inventoryPanel.SetActive(isactive);
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
	
	private void Update()
	{
	}
}

public class RuneDeck
{
	public List<string> runedecklist = new List<string>();
}

