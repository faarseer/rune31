using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json) // string item
    {
		// newjson = String.Format("{\"{0}\":", item) + json + "}";
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Runes;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Runes = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Runes = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Runes;
    }
}

[Serializable]
public class Rune
{
	public string rclass;
	
	public string name;

	public string collection;

	public string description;

}

public class TotalPool : MonoBehaviour
{
	void Start()
	{
		string filepath = Application.dataPath + "/Scripts/GameData/" + "Rune.json";
		string jsons = File.ReadAllText(filepath);
		Debug.Log(jsons);
		var totalrune = JsonHelper.FromJson<Rune>(jsons);
		Debug.Log(totalrune[0].name);
	}

	//T LoadRuneData<T>(string loadPath, string fileName)
	//{
	//	string filepath = loadPath + "/Scripts/GameData/" + "Rune.json";
	//	if(File.Exists(filepath))
	//	{
	//		string jsonString = File.ReadAllText(filepath);
	//		Debug.Log(jsonString);
	//		return JsonHelper.FromJson <T>(jsonString);
	
	//	else
	//	{
	//		Debug.LogError("error");
	//		return default(T);
	//	}
	//}
}
