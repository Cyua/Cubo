using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float view_value;
	public float move_speed;

	private Vector3 initial_position;

	public void resetCamera(){
		transform.Translate(initial_position-transform.position,Space.World);
	}
	void Start() {
		initial_position = transform.position;
	}

	void Update () {
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {  //scroll mouse wheel
			float zAxis = 10 * Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * view_value;
			if((zAxis+transform.position.z)>=-15 && (zAxis+transform.position.z) <= -5)			//limit the camera
				transform.Translate (new Vector3 (0, 0, zAxis));
			else if((zAxis+transform.position.z)<-15){
				transform.position = new Vector3(initial_position.x,initial_position.y,-15);
			}
			else{
				transform.position = new Vector3(initial_position.x,initial_position.y,-5);
			}
		}
	}
}
