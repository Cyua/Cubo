using UnityEngine;
using System.Collections;

public class SingleCube : MonoBehaviour {
	bool isClicked;
	// Use this for initialization
	void Start () {
		isClicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isClicked && Input.GetKeyDown ("space")) {
			gameObject.SetActive(false);
		}
	}

	void OnMouseDown(){
		isClicked = !isClicked;
	}
}
