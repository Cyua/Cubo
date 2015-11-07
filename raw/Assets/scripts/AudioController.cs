using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	private static AudioController instance = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static AudioController Instance
	{
		get
		{ 
			return instance;
		}
	}


	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}
		
		DontDestroyOnLoad(this.gameObject);
	}
}
