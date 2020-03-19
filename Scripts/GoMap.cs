using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoMap : MonoBehaviour
{
	[SerializeField]
	private Vector3 touchpoint;
	private RaycastHit2D hit;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			touchpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		if (Input.GetMouseButtonUp(0))
		{
			hit = Physics2D.Raycast(touchpoint, Vector3.zero);		
			if (hit.collider.gameObject == gameObject)
			{
				//SceneManager.LoadScene("MapScene", LoadSceneMode.Single);
			}
		}
	}
}
