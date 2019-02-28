using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseEnterCity : MonoBehaviour {

	// Use this for initialization
	public GameObject text;
	void show(){
		Debug.Log("Mostra o texto!");
		text.SetActive(true);
	}

	void hide(){
		Debug.Log("Apague o texto!");
		text.SetActive(false);
	}
	public void enter(){
		if (!text.activeSelf) {
			show();
		}
	}
	public void exit(){
		if(text.activeSelf){
			hide();
		}
	}
}
