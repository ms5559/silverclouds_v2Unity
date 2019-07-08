using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Switchcolor : MonoBehaviour {

	public GameObject rainbowFartOnButton;
	public Color[] colors;
	

    void Start() {

    }

    void Update(){

    	//Color theColor = gameObject.GetComponent<SpriteRenderer>().color; 
    	Color theColor = colors[Random.Range(0, colors.Length)];
    	//gameObject.GetComponent<SpriteRenderer>().color = theColor + new Color32(0,0,0,255);
    	//rainbowFartOnButton.GetComponent<Image>().sourceImage.color = theColor;
    	gameObject.GetComponent<Image>().color = theColor + new Color32(0,0,0,255);
    	//gameObject.GetComponent<SpriteRenderer>().color = theColor;
    }

}