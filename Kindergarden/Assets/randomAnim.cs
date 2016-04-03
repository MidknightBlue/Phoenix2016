using UnityEngine;
using System.Collections;

public class randomAnim : MonoBehaviour {
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        animator.speed = Random.Range(0, 3000);

        StartCoroutine(wait(Random.Range(0, 5)));

        animator.speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator wait(int seed)
    {
        yield return new WaitForSeconds(seed);
    }
}
