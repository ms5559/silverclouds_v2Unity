using UnityEngine;

public class Rotate : MonoBehaviour 
{
	public int speed;

    void Update() 
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.right * Time.deltaTime * speed);

        // ...also rotate around the World's Y axis
        transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World);
    }
}