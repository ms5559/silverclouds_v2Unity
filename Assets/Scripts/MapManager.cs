using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vimeo
{
public class MapManager : MonoBehaviour {

	private bool on = false;
	public GameObject map;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TurnMapOnOff() {

		on = !on;

		if (on) map.SetActive(true);
		if (!on) map.SetActive(false);
	

	}

}
}
