using UnityEngine;
using System.Collections;

public class SingleCube : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		gameObject.SetActive(false);
	}
}
