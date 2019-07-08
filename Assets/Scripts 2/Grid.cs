using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public GameObject block1;

	public float worldWidth = 100;

	public float worldHeight = 100;

	public float spawnSpeed = 0.01F;

	public float size = 0.5F;

	public Vector3 GetNearestPointOnGrid(Vector3 position)
	{

		position -= transform.position;

		float xCount = Mathf.Round(position.x / size);
		float yCount = Mathf.Round(position.y / size);
		float zCount = Mathf.Round(position.z / size);

		Vector3 result = new Vector3 ( 
			(float) xCount * size,
			(float) yCount * size,
			(float) zCount * size);

		result += transform.position;

		return result;

	}

	private void OnDrawGizmos () {

		Gizmos.color = Color.yellow;

		for (float x = this.transform.position.x; x < worldWidth; x += size)
		{
			for (float z = this.transform.position.z; z < worldHeight; z += size)
			{
				var point = GetNearestPointOnGrid (new Vector3 (x, this.transform.position.y, z));
				Gizmos.DrawSphere (point, 0.08f);

			}

		}

	}

	void Start () {

		StartCoroutine (CreateWorld());
		
	}
	
	void Update () {
		
	}

	IEnumerator CreateWorld () {


			yield return new WaitForSeconds(spawnSpeed);

			
			for (float x = 0; x < worldWidth; x += size)
			{

				yield return new WaitForSeconds(spawnSpeed);
			

				for (float z = 0; z < worldHeight; z += size)
				{
					
				yield return new WaitForSeconds(spawnSpeed);

				GameObject block = Instantiate(block1);

				block.transform.position = new Vector3 (this.transform.position.x + x, this.transform.position.y, this.transform.position.z + z);
				
				}
			}	
	}


}
