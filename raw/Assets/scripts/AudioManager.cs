using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
	
	public Slider slider;
	public Toggle toggle;

	private AudioSource audioMgr;
	//private AudioClip clip;
	// Use this for initialization
	void Start () {
		audioMgr = FindObjectOfType (typeof(AudioSource)) as AudioSource;
		slider.value = Mathf.MoveTowards (slider.value, audioMgr.volume, (float)1.0);
		toggle.isOn = audioMgr.isPlaying;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void changeVoice(){
		audioMgr.volume = slider.value;
	}

	public void turnOff(){
		if (toggle.isOn) {
			audioMgr.Play ();
		} else {
			audioMgr.Stop();
		}
	}
}
