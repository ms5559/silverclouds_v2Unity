using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMap : MonoBehaviour {

	public GameObject map;
	// Use this for initialization
	public void Start () {

		map = GameObject.FindWithTag("mapAssets");
		
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

	public void ScaleUp(){

		map.transform.localScale += new Vector3(0.01f,0.01f,0.01f);
	}

	public void ScaleDown(){

		map.transform.localScale -= new Vector3(0.01f,0.01f,0.01f);
	}
}
