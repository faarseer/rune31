using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicOnHit : MonoBehaviour
{
	private GameObject prObject; 
	
	void Awake()
	{
		prObject =transform.parent.parent.gameObject;
	}

	void OnCollisionEnter(Collision collision)
	{	
		if(prObject.tag == "Player")
		{
		}
		if(prObject.tag == "Enemy")
		{
		
		}
	}
}
	
