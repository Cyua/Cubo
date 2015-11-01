using UnityEngine;
using System.Collections;

public class SingleCube : MonoBehaviour {
	private bool marked = false;
	private bool knocked = false;
	private Renderer[] render;
	private Vector3 initialPos;

	private int enlarge = 0;
	private float resizeScale = 1.1f;

	private Texture2D texFB, texLR, texTB;

	public GameObject front, back, left, right, top, bottom;
	
	public Material originalMat;
	public Material newMat;

	// Use this for initialization
	void Start () {
		Vector3 ini= new Vector3 ();
		initialPos = transform.position;
		render = new Renderer[6];
		render[0] = front.GetComponent<Renderer> ();
		render[1] = back.GetComponent<Renderer> ();
		render[2] = left.GetComponent<Renderer> ();
		render[3] = right.GetComponent<Renderer> ();
		render[4] = top.GetComponent<Renderer> ();
		render[5] = bottom.GetComponent<Renderer> ();

		foreach (Renderer r in render)
			r.material = originalMat;
		ResetHint ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScale ();
	}

	void ResetHint() {
		front.GetComponent<Renderer> ().material.mainTexture = texFB;
		back.GetComponent<Renderer> ().material.mainTexture = texFB;
		left.GetComponent<Renderer> ().material.mainTexture = texLR;
		right.GetComponent<Renderer> ().material.mainTexture = texLR;
		top.GetComponent<Renderer> ().material.mainTexture = texTB;
		bottom.GetComponent<Renderer> ().material.mainTexture = texTB;
	}

	public void SetHint(Texture2D texFB, Texture2D texLR, Texture2D texTB) {
		this.texFB = texFB;
		this.texLR = texLR;
		this.texTB = texTB;
	}

	void OnMouseDown(){
		if (Input.GetKey ("x") && !knocked) {
			marked = !marked;
			if (marked)
				foreach (Renderer r in render)
					r.material = newMat;
			else
				foreach (Renderer r in render)
					r.material = originalMat;
			ResetHint();
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
