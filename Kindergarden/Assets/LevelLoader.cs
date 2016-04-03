using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public void loadLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void loadLevel(string levelTitle)
    {
        SceneManager.LoadScene(levelTitle);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
