using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {
	public Camera m_Camera;
	
	void Start() {
		m_Camera = Camera.main;
		//GetComponent<Canvas>().worldCamera = GameObject.Find("Camera View Board UI").GetComponent<Camera>();
	}

	void LateUpdate () {
		transform.LookAt (transform.position + m_Camera.transform.rotation * Vector3.forward,
			m_Camera.transform.rotation * Vector3.up);
	}
}