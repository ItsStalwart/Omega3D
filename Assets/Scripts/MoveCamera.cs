using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
	public Camera cameraTop;
	public Transform target;
	bool scroll = true;

	void Start() {
		target.position = new Vector3(((GameControl.singleton.puzzleMatrix.GetLength(0)/2f) - 0.5f), 0, ((GameControl.singleton.puzzleMatrix.GetLength(1)/2f) - 0.5f));
		cameraTop.transform.position = new Vector3(((GameControl.singleton.puzzleMatrix.GetLength(0)/2f) - 0.5f), 25, ((GameControl.singleton.puzzleMatrix.GetLength(1)/2f) - 0.5f));
		GetComponent<Camera>().orthographicSize = GameControl.singleton.puzzleMatrix.GetLength(1)/2f;
		cameraTop.orthographicSize = GameControl.singleton.puzzleMatrix.GetLength(1)/2f + 0.5f;
		target.GetChild(1).SetParent(null);
	}
	void LateUpdate () {
		
		//Rotate
		if(Input.GetKey(KeyCode.Q)) {
			target.RotateAround (target.position, Vector3.up, 45*Time.deltaTime);
			//AdjustRotationTarget();
		} else if(Input.GetKey(KeyCode.E)) {
			target.RotateAround (target.position, Vector3.up, -45*Time.deltaTime);
			//AdjustRotationTarget();
		}

		//Move
		if(Input.GetKey(KeyCode.A)) {
			target.Translate(10*Vector3.left * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.D)) {
			target.Translate(10*Vector3.right * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.W)) {
			target.Translate(10*Vector3.forward * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.S)) {
			target.Translate(10*Vector3.back * Time.deltaTime);
		}
		//Zoom
		if(scroll && Input.GetAxis("Mouse ScrollWheel") != 0) {
			GetComponent<Camera>().orthographicSize = Mathf.Clamp((GetComponent<Camera>().orthographicSize - Input.GetAxis("Mouse ScrollWheel")*2), 1, GameControl.singleton.puzzleMatrix.GetLength(1)/2f);
		}
	}

	void AdjustRotationTarget() {
			transform.LookAt(target.position, Vector3.up);
			transform.SetParent(null);
			Vector3 v = transform.position - target.position;
			v.y = 0;
			target.LookAt(target.position - v, Vector3.up);
			transform.SetParent(target);
	}

	public void SetScroll(bool s) {
		scroll = s;
	}
}
