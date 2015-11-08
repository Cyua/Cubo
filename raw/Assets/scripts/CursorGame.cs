using UnityEngine;
using System.Collections;

public class CursorGame : MonoBehaviour {
	private bool isClick = false;

	public Texture mouseTexture;
	public Texture mouseTextureClicked;
	// Use this for initialization
	void Start () {
		UnityEngine.Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			isClick = true;
		} else {
			isClick = false;
		}
	}

	void OnDestroy(){
		UnityEngine.Cursor.visible = true;
	}

	void OnGUI()
	{
		if (isClick) {
			Vector3 mousePos = Input.mousePosition;
			GUI.DrawTexture (new Rect (mousePos.x, Screen.height - mousePos.y, 
			                           mouseTexture.width, mouseTexture.height), mouseTextureClicked);
		} else {
			Vector3 mousePos = Input.mousePosition;
			GUI.DrawTexture (new Rect (mousePos.x, Screen.height - mousePos.y, 
			                           mouseTexture.width, mouseTexture.height), mouseTexture);
		}
	}
}