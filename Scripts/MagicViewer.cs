using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class MagicViewer : MonoBehaviour
{
	//private float distance = 10.0f;
	private _Magic _presentMagic;
	private LayerMask enmMask;
	private LayerMask plyMask;
	GameObject hitview;
	private GameObject magicPrefab;
	
	[SerializeField]
	public MagicSpace magicSpace;
	public float speed = 0.5f;
	
	public delegate void OnDestroyRuneCompoViewEvent();
	public OnDestroyRuneCompoViewEvent OnDestroyRuneCompoView;
	
	public delegate void OnHitEvent(_Magic magic, GameObject who, GameObject whom);
	public OnHitEvent OnHit;
	
	public GameObject prObject;

	void Awake()
	{
		enmMask = LayerMask.GetMask("Enemy");
		plyMask = LayerMask.GetMask("Player");
		if(prObject.tag == "Player")
		{
			magicSpace.OnChangeMagicView += OnChangeMagicView;
		}
		if(prObject.tag == "Enemy")
		{
			prObject.GetComponent<Enemy>().OnSpellMagic += OnSpellMagic;
		}
	}

	void OnChangeMagicView(_Magic magic, bool success)
	{
		Invoke("DestroyRuneCompoView",1);
		if(!success)
		{
			Debug.Log("Magic fail");
		}
		else
		{
			if(magic.prefab == null)
			{
				Debug.Log("Magic prefab null");
			}
			else
			{
				_presentMagic = magic;
				magicPrefab = Instantiate(Resources.Load(magic.prefab) as GameObject,
										transform.position, Quaternion.identity);
				// isatt bool값을 매직 프리팹이나 json 에서 불러와야됨
				magicPrefab.transform.parent = gameObject.transform;
				magicPrefab.AddComponent<BoxCollider2D>();
				magicPrefab.transform.localScale += new Vector3(0.1F,0,0);
				Debug.Log("magicPrefab startposition:"+ magicPrefab.transform.position);
				StartCoroutine(hitting(magicPrefab,enmMask));
			}
		}
	}
	
	void OnSpellMagic(_Magic magic)
	{
		_presentMagic = magic;
		magicPrefab = Instantiate(Resources.Load(magic.prefab) as GameObject,
								transform.position, Quaternion.identity);
		magicPrefab.transform.parent = gameObject.transform;
		magicPrefab.AddComponent<BoxCollider2D>();
		StartCoroutine(hitting(magicPrefab, plyMask));
	}
	
	IEnumerator hitting(GameObject g, LayerMask layer)
	{
		if(layer == plyMask)
		{	
			RaycastHit2D ray = Physics2D.Raycast(g.transform.position,g.transform.right, layer);
			if(ray.collider.tag == "Enemy")
			{
				Debug.Log("Enemy hit:" + ray.collider.gameObject.name);
				OnHit(_presentMagic, prObject, g);
				DestroyMagic(g);
			}
			else
			{
				g.transform.Translate(g.transform.right * speed * Time.deltaTime);
			}
		}

		if(layer == enmMask)
		{
			RaycastHit2D ray = Physics2D.Raycast(g.transform.position,-g.transform.right, layer);
			if(ray.collider.tag == "Player")
			{
				Debug.Log("Player hit");
				OnHit(_presentMagic, prObject, g);
				//onPlayerHit(_presentMagic, g);
				DestroyMagic(g);
			}
			else
			{
				g.transform.Translate(-g.transform.right * speed * Time.deltaTime);
			}
		}
		//Debug.Log("change position:"+ g.transform.position.ToString());
		yield return new WaitForSeconds(0.2f);
	}
	
	void DestroyMagic(GameObject g)
	{
		Debug.Log("hit start");
		hitview = Instantiate(Resources.Load("Prefabs/Hit_Anim") as GameObject, g.transform.position, Quaternion.identity);
		GameObject.Find("MagicSpace").GetComponent<MagicSpace>().onMagic = false;
		//Destroy(gameObject);
		Invoke("Destroyhit", 2);
	}

	void Destroyhit()
	{
		Destroy(hitview);
	}

	void DestroyRuneCompoView()
	{
		OnDestroyRuneCompoView();
	}
}
