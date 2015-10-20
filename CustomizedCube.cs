using UnityEngine;
using System.Collections;

public class CustomizedCube : MonoBehaviour {
	GameObject[,,] cubes;
	// Use this for initialization
	void Start () {
		int[,,] shape = new int[,,] {
			{{0,0,0,0,0}, {0,1,0,1,0}, {0,0,0,0,0}},
			{{0,1,1,1,0}, {1,1,1,1,1}, {0,1,1,1,0}},
			{{0,1,1,1,0}, {1,1,1,1,1}, {0,1,1,1,0}},
			//{{0,1,1,1,0}, {1,1,1,1,1}, {0,1,1,1,0}},
			{{0,0,1,0,0}, {0,1,1,1,0}, {0,0,1,0,0}},
			{{0,0,0,0,0}, {0,0,1,0,0}, {0,0,0,0,0}}
		};
		int l = shape.GetLength(2);    // 2
		int w = shape.GetLength(1);    // 5
		int h = shape.GetLength(0);    // 4
		cubes = new GameObject[h,w,l];

		for (int i = 0; i < h; i++) {
			for (int j = 0; j < w; j++) {
				for (int k = 0; k < l; k++) {
					if (shape[i,j,k] == 1) {
						cubes[i,j,k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
						cubes[i,j,k].transform.position = new Vector3(k, h - i, j);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
