using UnityEngine;
using System.Collections;

public class PlayOnClick : MonoBehaviour {

	public Pattern patGen;
	public int key;

	private AudioSource sound;
	private Renderer rend;
	private Color startingColor;
	private float startingX;
	private float shakeOff = 0.12f;
	private float startTime = 0.0f;
	private float shakeDuration = 0.48f;

	// Initializing the Audio Source
	void Awake () {
		sound = GetComponent<AudioSource> ();
		rend = GetComponent<Renderer> ();
		startingColor = rend.material.color;
		startingX = gameObject.transform.position.x;
	}

	// Update is called once per frame
	void Update () {
		Vector3 temp = gameObject.transform.position;
		if (startingX > temp.x && startingX - temp.x > 0.005f && Time.time - startTime < shakeDuration) {
			temp.x += ((startingX - temp.x) * 2) - 0.005f;
		} else if (startingX < temp.x && temp.x - startingX > 0.005f && Time.time - startTime < shakeDuration) {
			temp.x -= ((temp.x - startingX) * 2) - 0.005f;
		} else {
			temp.x = startingX;
			rend.material.color = startingColor;
		}
		gameObject.transform.position = temp;
	
	}

	void OnMouseDown(){
		if (patGen != null && !patGen.isPlaying) {
			playSound ();
			if (!patGen.isRecording) {
				patGen.startRecording (key);
			} else {
				patGen.recordSound (key);
			}
		} else if(patGen == null){
			playSound ();
		}
	}

	public void playSound(){
		Vector3 temp = gameObject.transform.position;
		temp.x -= shakeOff;
		gameObject.transform.position = temp;
		rend.material.color = Color.white;
		startTime = Time.time;
		sound.Play ();
	}

}
