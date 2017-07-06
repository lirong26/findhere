﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAdapter : MonoBehaviour {
    public GameObject imageField;

	// Use this for initialization
	void Start () {
		
	}
	
	public void setBytesToImage(byte[] bytes,int width,int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);
        imageField.GetComponent<Renderer>().material.mainTexture = texture;
        Vector3 scale = imageField.transform.localScale;
        imageField.transform.localScale = new Vector3(
            scale.x * width / height, scale.y, scale.z);
    }

}
