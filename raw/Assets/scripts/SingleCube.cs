using UnityEngine;
using System.Collections;

public class SingleCube : MonoBehaviour {
	private bool marked = false;
	private bool knocked = false;
	private Renderer render;
	private Vector3 initialPos;

	private int enlarge = 0;
	private float resizeScale = 1.1f;
	
	public Material originalMat;
	public Material newMat;
	// Use this for initialization
	void Start () {
		Vector3 ini= new Vector3 ();
		initialPos = transform.position;
		render = GetComponent<Renderer> (); 
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScale ();
	}

	void OnMouseDown(){
		if (Input.GetKey ("x") && !knocked) {
			marked = !marked;
			if (marked) {
				render.material = newMat;
			} else {
				render.material = originalMat;
			}
		} else if (Input.GetKey ("space") && !marked) {
			knocked = true;
			gameObject.SetActive (false);
			GameObject.Find ("Cubes").SendMessage ("recvMessage", initialPos);
		} else if (Input.GetKey ("space") && marked) {
			enlarge = 1;
		}
	}

	void UpdateScale() {
		if (enlarge == 0)
			return;
		else if (enlarge == 1) {
			transform.localScale = new Vector3 (resizeScale, resizeScale, resizeScale);
			enlarge++;
		} else if (enlarge == 5) {
			transform.localScale = new Vector3 (1, 1, 1);
			enlarge = 0;
		} else
			enlarge++;
	}
}
