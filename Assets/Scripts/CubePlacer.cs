using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour {

	private Grid grid;

	public void Awake () { 

		grid = FindObjectOfType<Grid>();
	}

	void Start () {
		
	}
	
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hitInfo))
			{
				PlaceCubeNear(hitInfo.point);
			}

		}
		
	}

	private void PlaceCubeNear (Vector3 nearPoint) {

		var FinalPosition = grid.GetNearestPointOnGrid(nearPoint);

		//GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = FinalPosition;

		//GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;

	}
}
