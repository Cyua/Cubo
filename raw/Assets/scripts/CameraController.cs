using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float view_value;
	public float move_speed;

//	private float rotate_horizontal;
//	private float rotate_vertical;
	private Vector3 initial_position;

	public void resetCamera(){
		transform.Translate(initial_position-transform.position,Space.World);
	}
	
	void Start() {
		initial_position = transform.position;
	}

	void Update () {
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {  //scroll mouse wheel
			transform.Translate (new Vector3 (0, 0, 10 * Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * view_value));
		}
		if (Input.GetMouseButton (0)) {   //press left mouse button
			transform.Translate(Vector3.left*Input.GetAxis("Mouse X")*move_speed);
			transform.Translate(Vector3.up*Input.GetAxis("Mouse Y")*-move_speed);
		}
		//rotate_horizontal = Input.GetAxis ("Horizontal");
		//rotate_vertical = Input.GetAxis ("Vertical");

		//transform.RotateAround(Vector3.zero,Vector3.up,Time.deltaTime*rotate_speed*rotate_horizontal);
			
		//transform.RotateAround(Vector3.zero,Vector3.right,Time.deltaTime*rotate_speed*rotate_vertical);
	}
}
