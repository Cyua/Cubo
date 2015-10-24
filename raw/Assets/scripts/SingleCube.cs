using UnityEngine;
using System.Collections;

public class SingleCube : MonoBehaviour {
	private bool isClicked;
	private Renderer render;
	private Vector3 initialPos;
	public Material originalMat;
	public Material newMat;
	// Use this for initialization
	void Start () {
		isClicked = false;
		initialPos = transform.position;
		render = GetComponent<Renderer> (); 
	}
	
	// Update is called once per frame
	void Update () {
		if (isClicked && Input.GetKeyDown("space")) {
			gameObject.SetActive(false);
			GameObject.Find("Cubes").SendMessage("recvMessage",initialPos);
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
