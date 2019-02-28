using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : GridUnit {

	public string inputDirection;
	public int inputType;
	public int inputLowerLimit;
	public int inputUpperLimit;
	public bool cleared;



	void FixedUpdate(){
		if (inputDirection.Contains("left") && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "") {
			source = GameControl.grid [x - 1, y];
			receiveSignal ();
		}if (inputDirection.Contains("right") && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "") {
			source = GameControl.grid [x + 1, y];
			receiveSignal();
		}if (inputDirection.Contains("up") && GameControl.grid [x,y + 1].pieceType != null && GameControl.grid [x, y + 1].pieceType != "") {
			source = GameControl.grid [x, y + 1];
			receiveSignal();
		}if (inputDirection.Contains("down") && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "") {
			source = GameControl.grid [x, y - 1];
			receiveSignal();
		}
		if(input != null)
		clearCheck ();
	}

	void Start(){
		pieceType = "Receiver";
		this.output = null;
	}

	void clearCheck(){
		if (input.strength <= inputUpperLimit && input.strength >= inputLowerLimit && input.type == inputType) {
			cleared = true;
			GameControl.singleton.winCheck ();
		} else {
			cleared = false;
		}
	}

	public override void Inspect(){
		if(monitor != null) {
			Destroy(monitor);
			return;
		}
		if (!cleared)
			monitor = GameControl.singleton.ShowMonitor ("Receptor aceitando Energia entre " + inputLowerLimit + "W e " + inputUpperLimit + "W do Tipo " + inputType + " em sua entrada " + inputDirection + " para ficar ativo!", transform.position);
		else
			monitor = GameControl.singleton.ShowMonitor ("Receptor está conectado à Matriz Energética!", transform.position);
	}
	public override void lightsout(){
	}
	public override void unitSpawn(){
	}
	public void setInput(string dir, int typ, int low, int hig){
		this.inputDirection = dir;
		this.inputType = typ;
		this.inputLowerLimit = low;
		this.inputUpperLimit = hig;
		if (inputDirection.Contains("left") ){
			this.gameObject.transform.Rotate (0, 0, 180);
		}if (inputDirection.Contains("right")) {
			this.gameObject.transform.Rotate (0, 0, 0);
		}if (inputDirection.Contains("up")) {
			this.gameObject.transform.Rotate (0, 0, 90);
		}if (inputDirection.Contains("down") ){
			this.gameObject.transform.Rotate (0, 0, -90);
		}
	}
	public override void highlight () {
		//this.gameObject.GetComponent<MeshRenderer> ().material.color = Color.blue;
	}
}
