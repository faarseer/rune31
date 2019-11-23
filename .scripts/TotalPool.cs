using System;
using System.Collections;
using System.Collections.Generics;
using UnityEngine;
using System.IO;

public class TotalPool : MonoBehaviour
{
	public dictionary<string, Rune> TotalPool;
	public string TotalRuneclass;
	public string name;
	public string collection;
	public string Description;
	private string RuneDataFileName = "Rune.json";

	void Start()
	{
		LoadRuneData();
	}

	private void LoadRuneData()
	{
		string filePath = Path.Combine(Application.GameData, RuneDataFileName);
		
		if(File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			Rune loadedData = JsonUtility.FromJson<Rune>(dataAsJson);
			TotalRuneclass = loadedData.rclass;
			TotalRunecollection = loadedData.collection;
			TotalRuneDsc = loadedData.Description;
			TotalName = loadedData.name;
		}
		else
		{
			Debug.LogError("cannot load rune");
		}
	}


