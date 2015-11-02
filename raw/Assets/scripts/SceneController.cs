using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toSelectLevel(){
		Application.LoadLevel("selectLevel");
	}

	public void Level1(){
		Application.LoadLevel("level1");
	}

	public void Level2(){
		Application.LoadLevel("level2");
	}

	public void BackToMain(){
		Application.LoadLevel("mainMenu");
	}
}
