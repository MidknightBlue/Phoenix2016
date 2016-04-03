using UnityEngine;
using System.Collections;
using System.Linq;

public class Pattern : MonoBehaviour {
    private int[,] pattern = {
        {1, 3, 0, 0, 2, 4, 0, 0},
        {1, 0, 3, 5, 1, 0, 3, 5},
        {3, 0, 2, 1, 0, 5, 4, 3},
        {1, 2, 3, 4, 5, 6, 7, 8},
        {6, 0, 6, 4, 3, 0, 6, 2}
    };

    private int[,] tempo = {
        {1, 0, 1, 0, 1, 0, 1, 0},
        {4, 4, 0, 0, 4, 4, 0, 0},
        {5, 5, 5, 0, 5, 5, 5, 0},
        {7, 0, 0, 0, 7, 0, 0, 0},
        {6, 0, 0, 6, 6, 0, 0, 6}
    };

	private bool isTempo = false;
	private float roundDelay = 1f;
	private float startTime = 0.0f;
	private float timeStep = 0.5f;
	private int[] recordedPattern = new int[8];
	private int[] currentPattern;
	private int lastIndex = 0;
	private int wrongCnt = 0;
	private int rightCnt = 0;
	private int recordedCnt = 0;
	private int currentCnt = 0;

    public PlayOnClick[] XylophoneKeys = new PlayOnClick[8];
	[System.NonSerialized] public bool isRecording = false;
	[System.NonSerialized] public bool isPlaying = false;



    // Use this for initialization
    void Start () {
		StartCoroutine(playBack (getPattern ()));
    }
	
	// Update is called once per frame
	void Update () {
		if (isRecording && ((isTempo && Time.time - startTime > timeStep * recordedPattern.Length)
			|| (!isTempo && recordedCnt >= currentCnt))) {
			isRecording = false;
			//isPlaying is set to true to keep startRecording from being called before evaluations
			isPlaying = true;
			evaluatePattern ();
		}
	}

	void evaluatePattern(){
		if (isTempo) {
			for (int i = 0; i < recordedPattern.Length; i++) {
				if (recordedPattern [i] != currentPattern [i]) {
					StartCoroutine (wrong ());
					return;
				}
			}
			StartCoroutine (right ());
		} else {
			int i = 0;
			int j = 0;
			string test1;
			while (i < currentPattern.Length-1 || j < recordedPattern.Length-1) {
				while (i < currentPattern.Length - 1 && currentPattern [i] == 0) {
					i++;
				}

				if (currentPattern [i] != recordedPattern [j]) {
					StartCoroutine (wrong ());
					i = currentPattern.Length;
					j = recordedPattern.Length;
					return;
				} else {
					if (i < currentPattern.Length - 1 || j == recordedPattern.Length-1) {
						i++;
					}
					if (j < recordedPattern.Length - 1 || i == currentPattern.Length-1) {
						j++;
					}
				}
				while (j < recordedPattern.Length - 1 && recordedPattern [j] == 0) {
					j++;
				}
			}
			StartCoroutine (right ());
		}
	}

	IEnumerator wrong(){
		wrongCnt++;
		//Play "Oops!"
		Debug.Log("Woops!");
		updateFileController(false);
		yield return new WaitForSeconds (roundDelay);
		StartCoroutine(playBack (currentPattern));
	}

	IEnumerator right(){
		rightCnt++;
		//Play "Yipee!"
		Debug.Log("Yipee!");
		updateFileController(true);
		yield return new WaitForSeconds (roundDelay);
		StartCoroutine(playBack (getPatOrTemp ()));
	}

	void updateFileController(bool correct){

	}

	int calcKeys(int[] pattern){
		int numOfKeys = 0;
		foreach (int num in pattern) {
			if (num > 0) {
				numOfKeys++;
			}
		}
		return numOfKeys;
	}

	int[] getPatOrTemp(){
		int diceRoll = Random.Range (0, 3);
		if (diceRoll >= 2) {
			return getPattern ();
		} else {
			return getTempo ();
		}
	}

    public int[] getPattern()
    {
		int index;
		isTempo = false;
		do{
	        index = Random.Range(0, 5);
			currentPattern = new int[] {pattern[index, 0], pattern[index, 1], pattern[index, 2], pattern[index, 3], pattern[index, 4],
	            pattern[index, 5], pattern[index, 6], pattern[index, 7]};
			currentCnt = calcKeys (currentPattern);
		}while(lastIndex == index);
		lastIndex = index;
		return currentPattern;
    }

    public int[] getTempo()
    {
		int index;
		isTempo = true;
		do{
	        index = Random.Range(0, 5);
			currentPattern = new int[] {tempo[index, 0], tempo[index, 1], tempo[index, 2], tempo[index, 3], tempo[index, 4],
	            tempo[index, 5], tempo[index, 6], tempo[index, 7]};
			currentCnt = 0;
		}while(lastIndex == index * 2);
		lastIndex = index * 2;
		return currentPattern;
    }

	public void startRecording(int pressedKey){
		isRecording = true;
		for (int i=0; i<recordedPattern.Length; i++) {
			recordedPattern [i] = 0;
		}
		if (isTempo) {
			startTime = Time.time;
		} else {
			recordedCnt = 1;
		}
		recordedPattern [0] = pressedKey;
	}

	public void recordSound(int pressedKey){
		if (isTempo) {
			int timeSlot = Mathf.RoundToInt(((Time.time - startTime - (timeStep/2))/ (timeStep * recordedPattern.Length)) * recordedPattern.Length);
			if (timeSlot < 0) {
				timeSlot = 0;
			} else if (timeSlot > currentPattern.Length-1) {
				timeSlot = currentPattern.Length - 1;
			}

			if (recordedPattern [timeSlot] == 0) {
				recordedPattern [timeSlot] = pressedKey;
			} else {
				recordedPattern [timeSlot] = -1;
			}
		} else {
			recordedPattern [recordedCnt] = pressedKey;
			recordedCnt++;
		}

	}

    IEnumerator playBack(int[] playMe)
    {
		isPlaying = true;
        for (int currentNote = 0; currentNote < playMe.Length; ++currentNote)
        {
            if (playMe[currentNote] != 0)
            {
                XylophoneKeys[playMe[currentNote]-1].playSound();
            }

			yield return new WaitForSeconds(timeStep);
        }
		isPlaying = false;
    }
}
