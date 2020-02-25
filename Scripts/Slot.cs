using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
	//누르고 떼는 것을 감지하기 위해, IPointerDownHandler, IPointerUpHander 를 상속.
	
	public Canvas canvas;
	//bool check = false;
	Vector2 defaultposition;
	Transform _startparent;
	GraphicRaycaster gr;
	PointerEventData ped;

	//public event EventHandler ChangeContentParent;

	private void Start()
	{
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		gr = canvas.GetComponent<GraphicRaycaster>();
		ped = new PointerEventData(null);
	}

	private void Update()
	{
	}
	
	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		_startparent = this.transform.parent;
		Debug.Log("startparent:"+_startparent.gameObject.name);
		transform.SetParent(GameObject.FindGameObjectWithTag("UI Canvas").transform);
		defaultposition = this.transform.position;
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		Vector2 currentpos = Input.mousePosition;
		ped.position = Input.mousePosition;
		Debug.Log(currentpos);
		this.transform.position = currentpos;
	}

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)
	{
		//Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Debug.Log(mousePos);
		List<RaycastResult> result = new List<RaycastResult>();
		gr.Raycast(ped, result);
		int l = 0;
		foreach(RaycastResult i in result)
		{
			Debug.Log(string.Format("graphicraycast{0}\t",l)+result[l].gameObject.name);
			l++;
			if(i.gameObject.name == "Viewport")
			{
				Debug.Log("Aaa"+i.gameObject.transform.GetChild(0));
				transform.SetParent(i.gameObject.transform.GetChild(0));
				Debug.Log("change??"+transform.parent.gameObject.name);
				//ChangeContentParent(this.gameObject, EventArgs.Empty);
			}
		}
		//Debug.Log("graphic raycast0\t"+result[0].gameObject.name);
		//Debug.Log("hitobject:"+hit.collider.gameObject.name);
		//transform.SetParent(result[0].gameObject.transform);
		//transform.SetParent(_startparent);
		//this.transform.position = Input.mousePosition;
		//transform.SetParent();
	}

	void IDropHandler.OnDrop(PointerEventData eventData)
	{
	}
}

