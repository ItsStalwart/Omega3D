using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : GridUnit {

	public string outputDirection;

	void Start(){
		pieceType = "Generator";
		input = null;

	}

	void FixedUpdate(){
		//emmitSignal ();
	}

	/*public void emmitSignal(){
		if (outputDirection.Contains("left") && GameControl.grid [x, y - 1].pieceType != null) {
			GameControl.grid [x, y - 1].input = output;
		}if (outputDirection.Contains("right") && GameControl.grid [x, y + 1].pieceType != null) {
			GameControl.grid [x, y + 1].input = output;
		}if (outputDirection.Contains("up") && GameControl.grid [x + 1,y].pieceType != null) {
			GameControl.grid [x + 1, y].input = output;
		}if (outputDirection.Contains("down") && GameControl.grid [x - 1, y].pieceType != null) {
			GameControl.grid [x - 1, y].input = output;
		}
	}*/

	public override void highlight(){
		
	}

	public override void Inspect(){
		if(monitor != null) {
			Destroy(monitor);
			return;
		}
		monitor = GameControl.singleton.ShowMonitor ("Gerador está sintetizando " + output.strength + "W da Energia de Tipo " + output.type + " em sua saída " + output.direction + "!", transform.position);
	}
	public override void lightsout(){
	}
	public override void unitSpawn(){
	}
}
