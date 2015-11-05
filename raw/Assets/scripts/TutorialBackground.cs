using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class TutorialBackground : MonoBehaviour {
	public Texture image;  
	// Use this for initialization
	void Awake(){
		gameObject.AddComponent<GUITexture>();  
	}
	void Start () {

		GetComponent<GUITexture>().texture = image;   
		GetComponent<GUITexture>().pixelInset = new Rect(65, 0, Screen.width, Screen.height);
	}

}
