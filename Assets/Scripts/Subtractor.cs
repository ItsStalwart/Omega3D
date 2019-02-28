using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subtractor : GridUnit {

	public int type;
	//input signal 1
	public Signal input1;

	//input signal 2
	public Signal input2;

	public string outputDirection;

	void FixedUpdate(){
		attenuateSignal ();
	}

	public void attenuateSignal(){
		int outputStrength = 0;
		int outputType = 0;
		if (type == 0) { 
			if (rotation == 0) {
				outputDirection = "down";
				if (x - 1 >= 0 && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "")
					input1 = GameControl.grid [x - 1, y].output;
				if (x + 1 <= GameControl.singleton.puzzleMatrix.GetLength(1)-1 && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "")
					input2 = GameControl.grid [x + 1, y].output;
			}if (rotation == 1) {
				outputDirection = "left";
				if (y - 1 >= 0 && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "")
					input1 = GameControl.grid [x, y - 1].output;
				if (x + 1 <= GameControl.singleton.puzzleMatrix.GetLength(1)-1 && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "")
					input2 = GameControl.grid [x, y + 1].output;
			}if (rotation == 2) {
				outputDirection = "up";
				if (x - 1 >= 0 && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "")
					input1 = GameControl.grid [x - 1, y].output;
				if (x + 1 <= GameControl.singleton.puzzleMatrix.GetLength(1)-1 && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "")
					input2 = GameControl.grid [x + 1, y].output;
			}if (rotation == 3) {
				outputDirection = "right";
				if (y - 1 >= 0 && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "")
					input1 = GameControl.grid [x, y - 1].output;
				if (y + 1 <= GameControl.singleton.puzzleMatrix.GetLength(0)-1 && GameControl.grid [x,y + 1].pieceType != null && GameControl.grid [x, y + 1].pieceType != "")
					input2 = GameControl.grid [x, y + 1].output;
			}
		}if (type == 1) { 
			if (rotation == 0) {
				outputDirection = "left";
				if (x + 1 <= GameControl.singleton.puzzleMatrix.GetLength(1)-1 && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "")
					input1 = GameControl.grid [x + 1, y].output;
				if (y - 1 >= 0 && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "")
					input2 = GameControl.grid [x, y - 1].output;
			}if (rotation == 1) {
				outputDirection = "up";
				if (x - 1 >= 0 && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "")
					input1 = GameControl.grid [x - 1, y].output;
				if (y - 1 >= 0 && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "")
					input2 = GameControl.grid [x, y - 1].output;
			}if (rotation == 2) {
				outputDirection = "right";
				if (x - 1 >= 0 && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "")
					input1 = GameControl.grid [x - 1, y].output;
				if (y + 1 <= GameControl.singleton.puzzleMatrix.GetLength(0)-1 && GameControl.grid [x,y + 1].pieceType != null && GameControl.grid [x, y + 1].pieceType != "")
					input2 = GameControl.grid [x, y + 1].output;
			}if (rotation == 3) {
				outputDirection = "down";
				if (x + 1 <= GameControl.singleton.puzzleMatrix.GetLength(1)-1 && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "")
					input1 = GameControl.grid [x + 1, y].output;
				if (y + 1 <= GameControl.singleton.puzzleMatrix.GetLength(0)-1 && GameControl.grid [x,y + 1].pieceType != null && GameControl.grid [x, y + 1].pieceType != "")
					input2 = GameControl.grid [x, y + 1].output;
			}		
		}if (type == 2) { 
			if (rotation == 0) {
				outputDirection = "right";
				if (x - 1 >= 0 && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "")
					input1 = GameControl.grid [x - 1, y].output;
				if (y - 1 >= 0 && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "")
					input2 = GameControl.grid [x, y - 1].output;
			}
			if (rotation == 1) {
				outputDirection = "down";
				if (x - 1 > 0 && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "")
					input1 = GameControl.grid [x - 1, y].output;
				if (y + 1 <= GameControl.singleton.puzzleMatrix.GetLength(0)-1 && GameControl.grid [x,y + 1].pieceType != null && GameControl.grid [x, y + 1].pieceType != "")
					input2 = GameControl.grid [x, y + 1].output;
			}
			if (rotation == 2) {
				outputDirection = "left";
				if (x + 1 <= GameControl.singleton.puzzleMatrix.GetLength(1)-1 && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "")
					input1 = GameControl.grid [x + 1, y].output;
				if (y + 1 <= GameControl.singleton.puzzleMatrix.GetLength(0)-1 && GameControl.grid [x,y + 1].pieceType != null && GameControl.grid [x, y + 1].pieceType != "")
					input2 = GameControl.grid [x, y + 1].output;
			}
			if (rotation == 3) {
				outputDirection = "up";
				if (x + 1 <= GameControl.singleton.puzzleMatrix.GetLength(1)-1 && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "")
					input1 = GameControl.grid [x + 1, y].output;
				if (y - 1 >= 0 && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "")
					input2 = GameControl.grid [x, y - 1].output;
			}
		}
		if (input1 != null && input2 != null) {
			if (input1.strength > input2.strength) {
				outputStrength = input1.strength - input2.strength;
				outputType = input1.type;
			} else if (input2.strength > input1.strength) {
				outputStrength = input2.strength - input1.strength;
				outputType = input2.type;
			} else {
				outputStrength = 0;
				outputType = input1.type;
			}
			output = new Signal (outputType, outputStrength, outputDirection);
		}
	}
	void Start(){
		this.pieceType = ("Attenuator");
	}

	public override void Inspect(){
		if(monitor != null) {
			Destroy(monitor);
			return;
		}
		if (input1 == null || input2 == null) {
			monitor = GameControl.singleton.ShowMonitor ("O Supressor não está ativo!", transform.position);
		} else {
			monitor = GameControl.singleton.ShowMonitor ("Subtraindo " + input1.strength + "W da Energia de Tipo " + input1.type + " e " + input2.strength + "W da Energia de Tipo " + input2.type + " em " + output.strength + "W da Energia de Tipo " + output.type + "!", transform.position);
		}
	}
	public override void lightsout(){
	}
	public override void highlight () {
		//this.gameObject.GetComponent<MeshRenderer> ().material.color = Color.blue;
	}
}
