﻿using UnityEngine;
using System.Collections;

public class SingleCube : MonoBehaviour {
	private bool marked = false;
	private bool knocked = false;
	private bool beJudged = false;
	private bool locked = false;

	private Renderer[] render;
	private Vector3 initialPos;

	private int enlarge = 0;
	private float resizeScale = 1.1f;
	
	private Texture2D texFB, texLR, texTB;

	public GameObject front, back, left, right, top, bottom;
	
	public Material originalMat;
	public Material newMat;
	public Material mistakeMat;

	// Use this for initialization
	void Start () {
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
		if (!locked) {
			if (Input.GetKey ("x") && !knocked) {
				marked = !marked;
				if (marked)
					foreach (Renderer r in render)
						r.material = newMat;
				else
					foreach (Renderer r in render)
						r.material = originalMat;
				ResetHint ();
			} else if (Input.GetKey ("space") && !marked) {
				knocked = true;
//			gameObject.SetActive (false);
				beJudged = true;
				GameObject.Find ("Cubes").SendMessage ("recvMessage", initialPos);
			}
			else if (Input.GetKey ("space") && marked)
				enlarge = 1;
		}
		else if (Input.GetKey ("space"))
			enlarge = 1;
	}

	void waitJudge(bool isLegal){
		if (beJudged) {
			if(isLegal){
				gameObject.SetActive(false);
			} else {
				locked = true;
				foreach (Renderer r in render)
					r.material = mistakeMat;
				ResetHint();
				enlarge=1;
			}
			beJudged = false;
		}
	}

	void lockCube(bool winOrLost){
		locked = true;
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

	//for tutorial Knock, don't change this
	void tutorialKnock(int stepCount){
		if (stepCount == 3) {
			if ((transform.position.x == -2 || transform.position.x == 0 || 
				transform.position.x == 2) && transform.position.y == 6) {
				gameObject.SetActive (false);
			}
			if ((transform.position.x == -2 || transform.position.x == -1 || transform.position.x == 1 || 
				transform.position.x == 2) && transform.position.y == 2) {
				gameObject.SetActive (false);
			}
			if ((transform.position.x == -2 || transform.position.x == 2) && transform.position.y == 3) {
				gameObject.SetActive (false);
			}
		} else if (stepCount == 6) {
			if ((initialPos.x == 1 || initialPos.x == -1)
				&& initialPos.y == 6 && (initialPos.z == -4 || initialPos.z == -2)) {
				gameObject.SetActive (false);
			}
		} else if (stepCount == 7) {
			if (initialPos.x == 1
				&& initialPos.y == 6 && initialPos.z == -3) {
				foreach (Renderer r in render)
					r.material = mistakeMat;
				ResetHint ();
			}
		} else if (stepCount == 8) {
			if (initialPos.x == -1
				&& initialPos.y == 6 && initialPos.z == -3) {
				foreach (Renderer r in render)
					r.material = newMat;
				ResetHint ();
			}
		} else if (stepCount == 10) {
			if ((initialPos.x == 2 || initialPos.x == -2)
				&& (initialPos.y == 5 || initialPos.y == 4) && (initialPos.z == -4 || initialPos.z == -2)) {
				gameObject.SetActive (false);
			}
			if ((initialPos.x == 1 || initialPos.x == -1)
				&& initialPos.y == 3 && (initialPos.z == -4 || initialPos.z == -2)) {
				gameObject.SetActive (false);
			}
			if (initialPos.x == -0
				&& initialPos.y == 2 && (initialPos.z == -4 || initialPos.z == -2)) {
				gameObject.SetActive (false);
			}
		} else if (stepCount == 11) {
			foreach (Renderer r in render)
				r.material = mistakeMat;
		}
	}
}
