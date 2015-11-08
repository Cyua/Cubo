using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
	
	public AudioClip mainBGM;
	public AudioClip tutorialBGM;
	public AudioClip gameBGM;
	
	private string previousScene = "mainMenu";
	private AudioSource audioMgr;
	private static AudioController instance = null;
	// Use this for initialization
	void Start () {
		audioMgr = FindObjectOfType (typeof(AudioSource)) as AudioSource;
	}
	
	// Update is called once per frame
	void OnLevelWasLoaded () {
		string currentScene = Application.loadedLevelName;
		Debug.Log ("current: "+currentScene);
		Debug.Log ("p: " + previousScene);
		if ((currentScene == "mainMenu" || currentScene == "selectLevel") 
			&& (previousScene != "mainMenu" && previousScene != "selectLevel")) {
			audioMgr.clip = mainBGM;
			audioMgr.Play(1);
			//			Debug.Log(currentScene);
		} else if (currentScene == "tutorial") {
			audioMgr.clip = tutorialBGM;
			audioMgr.PlayDelayed(1);
			//			Debug.Log(currentScene);
		} else if(currentScene == "level"){
			audioMgr.clip = gameBGM;
			audioMgr.Play();
			//			Debug.Log("game");
		}
		previousScene = currentScene;
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
