using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Data_Controller : MonoBehaviour {
	static GameObject _container;
	static GameObject Container;
	{
		get
		{
			return _container;
		}
	}

	static Data_Controller _instance;
	public static Data_Controller instance;
	{
		get
		{
			if(!_instance)
			{
				_container = new GameObject();
				_container.name = "Data_Controller";
				_instance = _container.AddComponent(typeof(Data_Controller)) as Data_Controller;
				DontDestroyOnLoad(_container);
			}
			return _instance;
		}
	}
}
