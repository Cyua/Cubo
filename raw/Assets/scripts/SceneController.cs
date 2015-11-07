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

<<<<<<< HEAD
	public void setting(){
		Application.LoadLevel("setting");
	}
=======
>>>>>>> 53c1f178d17787e8290b357e3c59cb589749691f

	public void Level1(){
		Application.LoadLevel("level1");
	}

	public void Level2(){
		Application.LoadLevel("level2");
	}

	public void Level3(){
		Application.LoadLevel("level3");
	}
	
	public void Level4(){
		Application.LoadLevel("level4");
	}

	public void Level5(){
		Application.LoadLevel("level5");
	}
	
	public void Level6(){
		Application.LoadLevel("level6");
	}


	public void BackToMain(){
		Application.LoadLevel("mainMenu");
	}

	public void Exit(){
		Application.Quit ();
	}
}
