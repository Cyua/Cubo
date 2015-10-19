using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	public float rotate_speed;

	private float rotate_vertical;
	private float rotate_horizontal;
	private Quaternion initial_rotation;

	public void resetCubes(){
		transform.rotation = initial_rotation;
	}

	void Start () {
		initial_rotation = transform.rotation;
	}

	void Update () {
		rotate_vertical = Input.GetAxis ("Vertical");
		rotate_horizontal = Input.GetAxis ("Horizontal");
		transform.RotateAround(transform.position,Vector3.right,Time.deltaTime*rotate_speed*rotate_vertical);
		transform.RotateAround(transform.position,Vector3.down,Time.deltaTime*rotate_speed*rotate_horizontal);
	}
}
