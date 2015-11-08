using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void tutorial(){
		Application.LoadLevel ("tutorial");
	}

	public void toSelectLevel(){
		Application.LoadLevel("selectLevel");
	}

	public void setting(){
		Application.LoadLevel("setting");
	}

	public void Level1(){
		PlayerPrefs.SetInt ("level",1);
		Application.LoadLevel("level");
	}

	public void Level2(){
		PlayerPrefs.SetInt ("level",2);
		Application.LoadLevel("level");
	}

	public void Level3(){
		PlayerPrefs.SetInt ("level",3);
		Application.LoadLevel("level");
	}
	
	public void Level4(){
		PlayerPrefs.SetInt ("level",4);
		Application.LoadLevel("level");
	}

	public void Level5(){
		PlayerPrefs.SetInt ("level",5);
		Application.LoadLevel("level");
	}
	
	public void Level6(){
		PlayerPrefs.SetInt ("level",6);
		Application.LoadLevel("level");
	}


	public void BackToMain(){
		Application.LoadLevel("mainMenu");
	}

	public void Exit(){
		Application.Quit ();
	}
}
