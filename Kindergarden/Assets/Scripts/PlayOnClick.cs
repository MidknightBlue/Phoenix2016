using UnityEngine;
using System.Collections;

public class PlayOnClick : MonoBehaviour {

	private AudioSource sound;
	private Renderer rend;
	private Color startingColor;
	private float startingX;
	private float shakeOff = 0.18f;

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
		if (startingX > temp.x && startingX - temp.x > 0.005f) {
			temp.x += ((startingX - temp.x) * 2) - 0.005f;
		} else if (startingX < temp.x && temp.x - startingX > 0.005f) {
			temp.x -= ((temp.x - startingX) * 2) - 0.005f;
		} else {
			rend.material.color = startingColor;
		}
		gameObject.transform.position = temp;
	
	}

	void OnMouseDown(){
		playSound ();
	}

	public void playSound(){
		Debug.Log (rend);
		Vector3 temp = gameObject.transform.position;
		temp.x -= shakeOff;
		gameObject.transform.position = temp;
		rend.material.color = Color.white;
		sound.Play ();
	}

}
