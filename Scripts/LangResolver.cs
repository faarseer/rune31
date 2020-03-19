using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class LangResolver : MonoBehaviour
{
	private const char seperator = '=';
	private readonly Dictionary<string, string> _lang = new Dictionary<string, string>();
	private SystemLanguage _language;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		ReadProperties();
		Debug.Log(_lang.Count);
	}

	private void ReadProperties()
	{
		_language = Application.systemLanguage; // need change;
		TextAsset file = Resources.Load<TextAsset>(_language.ToString());
		if(file == null)
		{
			file = Resources.Load<TextAsset>(SystemLanguage.English.ToString());
			_language = SystemLanguage.English;
		}
		foreach(var line in file.text.Split(new[]{"\n"},StringSplitOptions.None))
		{
			var prop = line.Split(seperator);
			_lang[prop[0]] = prop[1];
		}
	}

	public void ResolveTexts()
	{
		var allTexts = Resources.FindObjectsOfTypeAll<LangText>();
		foreach(var langText in allTexts)
		{
			var text = langText.gameObject.GetComponent<Text>();
			text.text = Regex.Unescape(_lang[langText.identifier]);
		}
	}
}
