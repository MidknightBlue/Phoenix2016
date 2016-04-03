using UnityEngine;
using System.Collections;

public class PlayOnClick : MonoBehaviour {

	private AudioSource sound;

	// Initializing the Audio Source
	void Start () {
		sound = GetComponent<AudioSource> ();
	}

	void OnMouseDown(){
		sound.Play ();
	}



	// Update is called once per frame
	void Update () {
	
	}
}
