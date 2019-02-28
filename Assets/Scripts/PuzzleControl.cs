using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleControl : MonoBehaviour {

	public static PuzzleControl singleton;

	public int selectedPuzzle;
	public Button[] puzzleButtons;
	public Sprite puzzleCleared;
	int currentLevel;
	void Awake () {
		if (singleton == null) {
			singleton = this;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (singleton);
		//CheckPuzzlesCleared ();
		StartCoroutine (BlinkCurrentPuzzle ());
	}

	public void selectPuzzle (int selection) {
		selectedPuzzle = selection;
	}

	public void runGame () {
		SceneManager.LoadScene ("main");
	}

	public void CheckPuzzlesCleared () {
		currentLevel = PlayerPrefs.GetInt ("PuzzleCleared", 1);
		Debug.Log (currentLevel);
		for (int i = 0; i < puzzleButtons.Length; ++i) {
			if (i >= currentLevel) puzzleButtons[i].interactable = false;
			else if (i < (currentLevel - 1)) puzzleButtons[i].GetComponent<Image> ().sprite = puzzleCleared;
		}
	}
	bool blink = false;
	IEnumerator BlinkCurrentPuzzle () {
		if (blink) {
			puzzleButtons[currentLevel - 1].transform.localScale = Vector3.one;
			blink = false;
		} else {
			puzzleButtons[currentLevel - 1].transform.localScale = Vector3.one * 1.2f;
			blink = true;
		}
		yield return new WaitForSeconds (0.5f);
		StartCoroutine (BlinkCurrentPuzzle ());
	}
}