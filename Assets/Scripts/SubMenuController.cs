using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuController : MonoBehaviour {
	public bool open = false;
	bool move = false;

	void Update () {
		if (move) {
			if (open) {
				if (GetComponent<RectTransform> ().anchoredPosition.x < 0) {
					GetComponent<RectTransform> ().anchoredPosition += new Vector2 (25, 0);
				} else {
					GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, GetComponent<RectTransform> ().anchoredPosition.y);
					move = false;
				}
			} else {
				if (GetComponent<RectTransform> ().anchoredPosition.x > -325) {
					GetComponent<RectTransform> ().anchoredPosition -= new Vector2 (25, 0);
				} else {
					GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-325, GetComponent<RectTransform> ().anchoredPosition.y);
					move = false;
				}
			}
		}
	}

	public void OpenClose () {
		open = !open;
		move = true;
	}
}