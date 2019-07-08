using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTexture : MonoBehaviour
{
    
    public Material YUVMaterial;
    public Material copyMaterial;


    void Start () {


        Texture2D texture = YUVMaterial.mainTexture as Texture2D;


        GetComponent<Renderer>().material.mainTexture = texture;


        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = ((x & y) != 0 ? Color.white : Color.gray);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
    
    }

}