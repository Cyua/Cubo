using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {
	public GameObject instantiate;
	public float rotate_speed;

	private float rotate_vertical;
	private float rotate_horizontal;
	private Quaternion initial_rotation;
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

	void Start () {
		initial_rotation = transform.rotation;
		int[,,] shape = new int[,,] {
			{{0,0,0,0,0}, {0,1,0,1,0}, {0,0,0,0,0}},
			{{0,1,1,1,0}, {1,1,1,1,1}, {0,1,1,1,0}},
			{{0,1,1,1,0}, {1,1,1,1,1}, {0,1,1,1,0}},
			{{0,0,1,0,0}, {0,1,1,1,0}, {0,0,1,0,0}},
			{{0,0,0,0,0}, {0,0,1,0,0}, {0,0,0,0,0}}
		};
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
