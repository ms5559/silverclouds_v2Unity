using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Drawing : MonoBehaviour {

	public GameObject prefab1;

	public Color[] colors;
	public List<GameObject> cubeList;

	public int counter;
	public int index;

	private Color paintColor;

	void Start(){

		cubeList = new List<GameObject>();

	}

	void Update(){

		if (Input.GetKeyDown("k")){

			counter++;
			paintColor = colors[counter];

		}

		if (counter > colors.Length-1) counter = 0;


		if (Input.GetKeyDown("f")){
			
			foreach(GameObject cubes in cubeList)
     		{
         		Destroy(cubes);
         		//cubeList.Clear();
     		}
		}
	}


		//called when the touch or a click is detected!!!!!!!!!!!!
	void OnMouseDrag(){


		//place the curose at the position where the tap/click take place
		print("dragging");
		Vector3 p=Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.3F));
		//Instantiate(prefab1, p, Random.rotation);
		GameObject cube = Instantiate (prefab1, p, Random.rotation);
		cubeList.Add(cube);
		cubeList[index].GetComponent<Renderer>().material.color = paintColor;
		index++;
		//prefab1.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
		//prefab1.transform.rotation = 
		
	}
	
	
	
	
	

}

