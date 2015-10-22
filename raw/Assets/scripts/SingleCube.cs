using UnityEngine;
using System.Collections;

public class SingleCube : MonoBehaviour {
	private bool isClicked;
	private Renderer render;
	public Material originalMat;
	public Material newMat;

	// Use this for initialization
	void Start () {
		isClicked = false;
		render = GetComponent<Renderer> (); 
	}
	
	// Update is called once per frame
	void Update () {
		if (isClicked && Input.GetKeyDown("space")) {
			gameObject.SetActive(false);
		}
	}

	void OnMouseDown(){
		isClicked = !isClicked;
		if (isClicked) {
			render.material = newMat;
		} else {
			render.material = originalMat;
		}
	}
}
