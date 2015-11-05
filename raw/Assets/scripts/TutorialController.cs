﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
	public GameObject instantiate;
	public float rotate_speed;
	public Texture2D[,] hintTex = new Texture2D[10, 3];

	private float rotate_vertical;			//rotate cubes
	private float rotate_horizontal;		//rotate cubes
	private Quaternion initial_rotation;	//record the initial rotation
	private ArrayList rawInput = new ArrayList();
	private int[,,] shape;					//original shape of cubes
	private int[,,] win;					//final shape of cubes, which win the game
	private int[,] hintHW, hintHL, hintWL;	//hints
	private GameObject cubes;				//cube model
	private int l=0,w=0,h=0;				//length, width, height of the shape
	private int mx=0,my=0,mz=0;				//center vector3 of the shape
	private float px=0,py=0,pz=0;			//center vector3 of the Cubes


	private Rect penal;
	private int stepCount=0;
	private const int MAXSTEP = 10;
	private string[] tutorialStep = new string[MAXSTEP];
	void OnGUI(){
		
		//		GUIStyle background = new GUIStyle ();		//set the background image to fill the screen
		//		background.normal.background = image;
		//		GUI.Label (new Rect(0,0,Screen.width,Screen.height),"",background);
		
		penal = new Rect (0,0,Screen.width,Screen.height/4);
		penal = GUI.Window (0,penal,giveTutorial,"Welcome to Tutorial");
		
	}
	void giveTutorial(int windowId){
		if (windowId == 0) {
			GUIStyle textFont = new GUIStyle ();
			textFont.fontSize = 20;									//font size
			textFont.normal.textColor = new Color (255, 255, 255);		//font color
			
			GUI.Label (new Rect (30, 30, penal.width - 60, penal.height - 60), tutorialStep [stepCount], textFont);		//set text area
			if (GUI.Button (new Rect (penal.width - 80, penal.height - 40, 65, 30), "Next")) {
				stepCount++;
				if (stepCount >= MAXSTEP)
					stepCount = MAXSTEP - 1;
			}
		}
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
	

	void Start () {

		// load textures;
		for (int i = 0; i <= 9; i++)
			for (int j = 0; j <= 2; j++)
				hintTex[i, j] = LoadTex(i, j);

		initial_rotation = transform.rotation;
		readInput ("level1");
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
//		if (Input.GetMouseButton (0) && !Input.GetKeyDown ("space")) {
//			rotate_vertical = Input.GetAxis ("Mouse Y");
//			rotate_horizontal = Input.GetAxis ("Mouse X");
//			transform.RotateAround (transform.position, Vector3.right, Time.deltaTime * rotate_speed * rotate_vertical);
//			transform.RotateAround (transform.position, Vector3.down, Time.deltaTime * rotate_speed * rotate_horizontal);
//		}
	}
}
