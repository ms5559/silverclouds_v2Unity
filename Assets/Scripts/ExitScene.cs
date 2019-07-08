using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScene : MonoBehaviour {

	public string LevelName;

	public void Exit(){
		Application.LoadLevel(LevelName);
	}
}
