using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {
	public GameObject instantiate;
	public float rotate_speed;

	private float rotate_vertical;			//rotate cubes
	private float rotate_horizontal;		//rotate cubes
	private Quaternion initial_rotation;	//record the initial rotation
	private ArrayList rawInput = new ArrayList();
	private int[,,] shape;
	private GameObject cubes;

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

	void genShape(string target){
		TextAsset level = (TextAsset)Resources.Load (target);
		string temp = level.text;
		int h = 0;
		int w = 0;
		int l = 0;
		string[] first = temp.Split ('\n');			//fill the rawInput
		h = first.Length;
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
		shape = new int[h, w, l];
		for (int i = 0; i < h; i++) {
			for(int j = 0; j < w; j++){
				for(int k = 0; k < l; k++){
					shape[i,j,k] = (int)rawInput[count++];
				}
			}
		}

	}

	void Start () {
		initial_rotation = transform.rotation;
		genShape ("level1");

		int l = shape.GetLength(2);    // 5
		int w = shape.GetLength(1);    // 3
		int h = shape.GetLength(0);    // 5

		int mx = returnMiddle (l)-1;  //calculate the middle point
		int my = returnMiddle (h);
		int mz = returnMiddle (w)-1;

		float px = transform.position.x;  //parent's position
		float py = transform.position.y;
		float pz = transform.position.z;

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
		rotate_vertical = Input.GetAxis ("Vertical");
		rotate_horizontal = Input.GetAxis ("Horizontal");
		transform.RotateAround(transform.position,Vector3.right,Time.deltaTime*rotate_speed*rotate_vertical);
		transform.RotateAround(transform.position,Vector3.down,Time.deltaTime*rotate_speed*rotate_horizontal);
	}
}
