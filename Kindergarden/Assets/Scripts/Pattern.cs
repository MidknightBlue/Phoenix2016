using UnityEngine;
using System.Collections;

public class Pattern : MonoBehaviour {
    private int[,] pattern = {
        {1, 3, 0, 0, 2, 4, 0, 0},
        {1, 0, 3, 5, 1, 0, 3, 5},
        {3, 0, 2, 1, 0, 5, 4, 3},
        {6, 8, 7, 5, 0, 0, 0, 0},
        {6, 0, 6, 4, 3, 0, 6, 2}
    };

    private int[,] tempo = {
        {1, 0, 1, 0, 1, 0, 1, 0},
        {4, 4, 0, 0, 4, 4, 0, 0},
        {5, 5, 5, 0, 5, 5, 5, 0},
        {7, 0, 0, 0, 7, 0, 0, 0},
        {6, 0, 0, 6, 6, 0, 0, 6}
    };

    //GameObject[] XylophoneKeys = new GameObject[8]; 



    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public int[] getPattern()
    {
        int index = Random.Range(0, 5);

        return new int[] {pattern[index, 0], pattern[index, 1], pattern[index, 2], pattern[index, 3], pattern[index, 4],
            pattern[index, 5], pattern[index, 6], pattern[index, 7]};
    }

    public int[] getTempo()
    {
        int index = Random.Range(0, 5);

        return new int[] {tempo[index, 0], tempo[index, 1], tempo[index, 2], tempo[index, 3], tempo[index, 4],
            tempo[index, 5], tempo[index, 6], tempo[index, 7]};
    }

    IEnumerator playBack(int[] playMe)
    {
        for (int currentNote = 0; currentNote < playMe.Length; ++currentNote)
        {
            if (playMe[currentNote] != 0)
            {
                //XylophoneKeys[currentNote].playSound();
            }

            yield return new WaitForSeconds(2);
        }
    }
}
