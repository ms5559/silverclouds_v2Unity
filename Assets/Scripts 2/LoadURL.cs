using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadURL : MonoBehaviour {

	public string URL;

	public void LoadTheURL (){

		Application.OpenURL(URL);

	}
}
