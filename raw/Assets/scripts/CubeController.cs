using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CubeController : MonoBehaviour {
	public GameObject instantiate;
	public float rotate_speed;
	public Texture2D[,] hintTex = new Texture2D[10, 3];
	public Text remAttemptText;

	private float rotate_vertical;			//rotate cubes
	private float rotate_horizontal;		//rotate cubes
	private Quaternion initial_rotation;	//record the initial rotation
	private int[,,] shape;					//original shape of cubes
	private int[,,] win;					//final shape of cubes, which win the game
	private int[,] hintHW, hintHL, hintWL;	//hints
	private int[,,] current;				//current axis
	private GameObject cubes;				//cube model
	private int l=0,w=0,h=0;				//length, width, height of the shape
	private int mx=0,my=0,mz=0;				//center vector3 of the shape
	private float px=0,py=0,pz=0;			//center vector3 of the Cubes

	private bool isWin = false;				//judge if the game is win
	private bool isLost = false;			//judge if the game is lost
	private int remAttempt = 3;				//count the mistakes, if larger than 3, game lost
	private string oriAttemptText;
	public Rect windowRect1;
	public Rect windowRect2;

	private string levelNum=0;				//determine load which level	
	/*****************************************/
	//display the windows when win or lost
	void OnGUI() {
	 	windowRect1 = new Rect((float)Screen.width/2-150, 30, 300, 80);
		windowRect2 = new Rect((float)Screen.width/2-150, 30, 300, 80);
		if(isLost)
			windowRect1 = GUI.Window(0, windowRect1, DoMyWindow, "You lost");
		if(isWin)
			windowRect2 = GUI.Window(1, windowRect2, DoMyWindow, "You Win");
	}
	void DoMyWindow(int windowID) {

		float buttonX1 = windowRect1.width / 10;								//button on the left
		float buttonY1 = windowRect1.height / (float)2.5;
		float buttonWidth = windowRect1.width / 3;
		float buttonHeight = windowRect1.height / (float)2.5;

		float buttonX2 = buttonX1 + buttonWidth + windowRect1.width / (float)6.5;		//button on the right



		if (windowID == 0) {		//lost the game
			if (GUI.Button (new Rect (buttonX1,buttonY1,buttonWidth,buttonHeight), "Back")) {
				Application.LoadLevel("selectLevel");
			}
			if (GUI.Button (new Rect (buttonX2, buttonY1, buttonWidth, buttonHeight), "Replay")) {
				Application.LoadLevel("level1");
			}
		} else if (windowID == 1) {
			if (GUI.Button (new Rect (buttonX1, buttonY1, buttonWidth, buttonHeight), "Back")) {
				Application.LoadLevel("selectLevel");
			}
			if (GUI.Button (new Rect (buttonX2, buttonY1, buttonWidth, buttonHeight), "Next")) {
				Application.LoadLevel("level2");
			}
		}
	}
	/*****************************************/

	public void resetCubes(){
		transform.rotation = initial_rotation;
	}

	int returnMiddle(int x){
		double tempf = x * 1.0 / 2;
		int tempi = x / 2;
		if (tempf > tempi)
			return tempi+1;
		else
			return tempi;
	}

	Texture2D LoadTex(int number, int type) {
		Texture2D tex = null;
		string filename;
		string append;
		if (number == 0 && type == 1)
			tex = (Texture2D)Resources.Load ("Textures/null");
		else {
			if (type == 0)
				append = "";
			else if (type == 1)
				append = "_c";
			else
				append = "_s";
			filename = "Textures/t" + number + append;
			tex = (Texture2D)Resources.Load (filename);
		}
		return tex;
	}

	Texture2D TransformHint(int number) {
		if (number == -1)
			return hintTex [0, 1];
		else if (number < 10)
			return hintTex [number, 0];
		else if (number < 100)
			return hintTex [number / 10, 1];
		else
			return hintTex [number / 100, 2];
	}

	//read "level.txt"
	void readInput(string target){
		TextAsset level = (TextAsset)Resources.Load (target);
		string[] lines = level.text.Split('\n');			//fill the rawInput

		// First line: Height, width, length
		string[] hwl = lines[0].Split(',');
		h = int.Parse(hwl[0]);
		w = int.Parse(hwl[1]);
		l = int.Parse(hwl[2]);

		shape = new int[h, w, l];
		win = new int[h, w, l];
		hintHW = new int[h, w];
		hintHL = new int[h, l];
		hintWL = new int[w, l];

		// Read shape
		for (int i = 0; i < h; i++) {
			string line = lines[i+1];
			string[] second = line.Split(',');
			for (int j = 0; j < w; j++) {
				string[] third = second[j].Split(' ');
				for (int k = 0; k < l; k++) {
					win[i, j, k] = int.Parse(third[k]);
					shape[i, j, k] = 1;
				}
			}
		}

		// Read hint
		// height-width
		string[] hw = lines[h+1].Split(',');
		for (int i = 0; i < h; i++) {
			string[] second = hw [i].Split (' ');
			for (int j = 0; j < w; j++)
				hintHW [i, j] = int.Parse (second [j]);
		}
		// height-length
		string[] hl = lines[h+2].Split(',');
		for (int i = 0; i < h; i++) {
			string[] second = hl [i].Split (' ');
			for (int j = 0; j < l; j++)
				hintHL [i, j] = int.Parse (second [j]);
		}

		// width-length
		string[] wl = lines[h+3].Split(',');
		for (int i = 0; i < w; i++) {
			string[] second = wl [i].Split (' ');
			for (int j = 0; j < l; j++)
				hintWL [i, j] = int.Parse (second [j]);
		}
	}

	/*********************   handle the messages   **********************/
	void recvMessage(Vector3 o){			//receive message from the children
		int x = (int)o.x;
		int y = (int)o.y;
		int z = (int)o.z;
		int k = (int)(x+mx-px);
		int i = (int)(h-my+py-y);
		int j = (int)(z+mz-pz);

		if (win [i, j, k]==1) {			//the cube is pressed false
			GameObject [] childCube = GameObject.FindGameObjectsWithTag("cubeTag");
			foreach(GameObject itemCube in childCube){
				itemCube.SendMessage("waitJudge",false);
			}
			remAttempt--;
			remAttemptText.text = oriAttemptText + remAttempt;	//update the Text
			if(remAttempt<=0){			//just three opportunity!
				isLost = true;			//lost the game!
				isWin = false;
				Debug.Log(false);	
			}
		} else {
			GameObject [] childCube = GameObject.FindGameObjectsWithTag("cubeTag");
			foreach(GameObject itemCube in childCube){
				itemCube.SendMessage("waitJudge",true);
			}
			current [i, j, k] = 0;
			if (compareArrays ()) {
				isWin = true;
				Debug.Log ("win");
			}
		}

		if (isWin || isLost) {				//lock the cubes
			GameObject [] childCube = GameObject.FindGameObjectsWithTag("cubeTag");
			foreach(GameObject itemCube in childCube){
				itemCube.SendMessage("lockCube",true);
			}
		}
	}

	/**/
	bool compareArrays(){					//compare current and win, judge if the game is over
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				for (int k = 0; k < l; k++)
					if(win[i, j, k] != current[i, j, k])
						return false;
		return true;
	}

	void Start () {
		//initial Text Area
		oriAttemptText = remAttemptText.text;
		remAttemptText.text = oriAttemptText + remAttempt;

		// load textures;
		for (int i = 0; i <= 9; i++)
			for (int j = 0; j <= 2; j++)
				hintTex[i, j] = LoadTex(i, j);

		initial_rotation = transform.rotation;
		readInput ("level2");
		current = shape;
		mx = returnMiddle (l)-1;  //calculate the middle point
		my = returnMiddle (h);
		mz = returnMiddle (w)-1;

		px = transform.position.x;  //parent's position
		py = transform.position.y;
		pz = transform.position.z;

		for (int i = 0; i < h; i++) {
			for (int j = 0; j < w; j++) {
				for (int k = 0; k < l; k++) {
					if (shape[i,j,k] == 1) {
						cubes = Instantiate (instantiate, transform.position, transform.rotation) as GameObject;
						cubes.transform.SetParent(this.transform,true);
						cubes.transform.position = new Vector3(k-mx+px, h-i-my+py, j-mz+pz);
						cubes.GetComponent<SingleCube>().SetHint(TransformHint(hintHL[i, k]), 
						                                         TransformHint(hintHW[i, j]),
						                                         TransformHint(hintWL[j, k]));
					}
				}
			}
		}
	}

	void Update () {
		// Rotate the cube as mouse drags
		if (Input.GetMouseButton (0) && !Input.GetKeyDown ("space")) {
			rotate_vertical = Input.GetAxis ("Mouse Y");
			rotate_horizontal = Input.GetAxis ("Mouse X");
			transform.RotateAround (transform.position, Vector3.right, Time.deltaTime * rotate_speed * rotate_vertical);
			transform.RotateAround (transform.position, Vector3.down, Time.deltaTime * rotate_speed * rotate_horizontal);
		}
	}
}
