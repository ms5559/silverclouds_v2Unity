using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderVirtualCam : MonoBehaviour {

	public Camera virtuCamera;
	public RenderTexture myRenderTexture;
	public Texture2D myTexture2D;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		RenderTexture.active = myRenderTexture;
		myTexture2D.ReadPixels(new Rect(0, 0, myRenderTexture.width, myRenderTexture.height), 0, 0);
 		myTexture2D.Apply();
		
	}

	 public void MakeSquarePngFromOurVirtualThingy()
     {
     // capture the virtuCam and save it as a square PNG.
     
     int sqr = 512;
     
     virtuCamera.GetComponent<Camera>().aspect = 1.0f;
     // recall that the height is now the "actual" size from now on
     // the .aspect property is very tricky in Unity, and bizarrely is NOT shown in the editor
     // the editor will still incorrectly show the frustrum being screen-shaped
     
     RenderTexture tempRT = new RenderTexture(sqr,sqr, 24 );
     // the "24" can be 0,16,24 or formats like RenderTextureFormat.Default, ARGB32 etc.
     
     virtuCamera.GetComponent<Camera>().targetTexture = tempRT;
     virtuCamera.GetComponent<Camera>().Render();
     
     RenderTexture.active = tempRT;
     Texture2D virtualPhoto = new Texture2D(sqr,sqr, TextureFormat.RGB24, false);
     // false, meaning no need for mipmaps
     virtualPhoto.ReadPixels( new Rect(0, 0, sqr,sqr), 0, 0); // you get the center section
     
     RenderTexture.active = null; // "just in case" 
     virtuCamera.GetComponent<Camera>().targetTexture = null;
     //////Destroy(tempRT); - tricky on android and other platforms, take care
     
     byte[] bytes;
     bytes = virtualPhoto.EncodeToPNG();
     
     System.IO.File.WriteAllBytes( OurTempSquareImageLocation(), bytes );
     // virtualCam.SetActive(false); ... not necesssary but take care
     
     // now use the image somehow...
     //YourOngoingRoutine( OurTempSquareImageLocation() );
     }
 private string OurTempSquareImageLocation()
     {
     string r = Application.persistentDataPath + "/p.png";
     return r;
     }
}
