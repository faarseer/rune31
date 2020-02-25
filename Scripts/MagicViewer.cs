using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class MagicViewer : MonoBehaviour
{
	public float distance = 10.0f;
	public LayerMask islayer = 8;
	public float speed = 2.0f;
	public GameObject hitview;
	public GameObject hitload;
	
	void Update()
	{
		Debug.Log("layermask::"+LayerMask.LayerToName(8));
		StartCoroutine(hitting());
		Invoke("DestroyMagic", 5);

	}

	IEnumerator hitting()
	{
		RaycastHit2D ray = Physics2D.Raycast(transform.position,transform.right, distance, islayer);
		if(ray.collider != null)
		{
			Debug.Log("hit??"+ray.collider.gameObject.name); // layer 추가 해야겟다..
			if(ray.collider.tag == "Enemy")
			{
				Debug.Log("hit");
			}
			DestroyMagic();
		}
		transform.Translate(transform.right * speed * Time.deltaTime);
		yield return new WaitForSeconds(1.0f);
	}
	
	void DestroyMagic()
	{
		hitload = Resources.Load("Prefabs/Hit_Anim") as GameObject;
		hitview = Instantiate(hitload,new Vector3(0,0,-4), Quaternion.identity);
		this.transform.position = this.transform.parent.position;
		GameObject.Find("MagicSpace").GetComponent<MagicSpace>().onmagic = false;
		Destroy(gameObject);
		//Invoke("Destroyhit", 2);
	}

	void Destroyhit()
	{
		Destroy(hitview);
	}

}
