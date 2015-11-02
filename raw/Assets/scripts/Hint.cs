using UnityEngine;
using System.Collections;

public class Hint : MonoBehaviour {
	public GameObject front, back, top, bottom, left, right;
	public Texture2D tex;
	// Use this for initialization
	void Start () {
		front.GetComponent<Renderer> ().material.mainTexture = tex;
		back.GetComponent<Renderer> ().material.mainTexture = tex;
		top.GetComponent<Renderer> ().material.mainTexture = tex;
		bottom.GetComponent<Renderer> ().material.mainTexture = tex;
		left.GetComponent<Renderer> ().material.mainTexture = tex;
		right.GetComponent<Renderer> ().material.mainTexture = tex;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
