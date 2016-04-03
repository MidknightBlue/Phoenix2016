using UnityEngine;
using System.Collections;

public class SlowDown : MonoBehaviour {
    Animation anim;
    private float animationSpeed;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        animationSpeed = 1.5f;

        foreach (AnimationState state in anim)
        {
            state.speed = animationSpeed;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
