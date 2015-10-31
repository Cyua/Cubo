using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {
	public GameObject instantiate;
	public float rotate_speed;

	private float rotate_vertical;			//rotate cubes
	private float rotate_horizontal;		//rotate cubes
	private Quaternion initial_rotation;	//record the initial rotation
	private ArrayList rawInput = new ArrayList();
	private int[,,] shape;					//original shape of cubes
	private int[,,] win;					//final shape of cubes, which win the game
	private int[,,] current;				//current axis
	private GameObject cubes;				//cube model
	private int l=0,w=0,h=0;				//length, width, height of the shape
	private int mx=0,my=0,mz=0;				//center vector3 of the shape
	private float px=0,py=0,pz=0;			//center vector3 of the Cubes
	private bool isWin = false;				//judge if the game is over

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


	//if read "level.txt", flag = 0; else flag = 1;
	void readInput(string target,int flag){
		TextAsset level = (TextAsset)Resources.Load (target);
		string temp = level.text;
		string[] first = temp.Split ('\n');			//fill the rawInput
		h = first.Length;
		rawInput.Clear();
		foreach (string x in first) {
			string[] second = x.Split(',');
			w = second.Length;
			foreach(string y in second){
				string[] third = y.Split(' ');
				l = third.Length;
				foreach(string z in third){
					int node = int.Parse(z);
					rawInput.Add(node);
				}
			}
		}
		int count = 0;
		if (flag == 0) {
			shape = new int[h, w, l];
		} else {
			win = new int[h, w, l];
		}
		for (int i = 0; i < h; i++) {
			for (int j = 0; j < w; j++) {
				for (int k = 0; k < l; k++) {
					if(flag==0){
						shape[i, j, k] = (int)rawInput[count++];
					}
					else{
						win[i, j, k] = (int)rawInput[count++];
					}
				}
			}
		}
	}


	void recvMessage(Vector3 o){			//receive message from the children
		int x = (int)o.x;
		int y = (int)o.y;
		int z = (int)o.z;
		int k = (int)(x+mx-px);
		int i = (int)(h-my+py-y);
		int j = (int)(z+mz-pz);
		current [i, j, k] = 0;
		if (compareArrays ()) {
			isWin = true;
		}
	}


	bool compareArrays(){					//compare current and win, judge if the game is over
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				for (int k = 0; k < l; k++)
					if(win[i, j, k] != current[i, j, k])
						return false;
		return true;
	}

	void Start () {
		initial_rotation = transform.rotation;
		readInput ("level1",0);
		readInput ("win1",1);
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
					}
				}
			}
		}
	}

	void Update () {
		// Rotate the cube as mouse drags
		if (Input.GetMouseButton(0) && !Input.GetKeyDown("space")) {
			rotate_vertical = Input.GetAxis ("Mouse Y");
			rotate_horizontal = Input.GetAxis ("Mouse X");
			transform.RotateAround(transform.position,Vector3.right,Time.deltaTime*rotate_speed*rotate_vertical);
			transform.RotateAround(transform.position,Vector3.down,Time.deltaTime*rotate_speed*rotate_horizontal);
		}
	}
}
