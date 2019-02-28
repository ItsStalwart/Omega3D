using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridUnit : MonoBehaviour {

	public int x;
	public int y;
	public int rotation;

	public bool flowing;

	public Signal input;

	public Signal output;

	public GridUnit source;

	public string pieceType = null;

	protected GameObject monitor;

	void Update() {
		if(!GameControl.inspecting && monitor != null) Destroy(monitor);
	}

	void OnMouseEnter () {
		if (!EventSystem.current.IsPointerOverGameObject ())
			highlight ();
	}

	void OnMouseExit () {
			lightsout ();
	}

	public void receiveSignal () {
		input = source.output;
	}

	protected void setSource () {
		if (input.direction.Contains ("left") && GameControl.grid[x, y - 1].pieceType != null) {
			source = GameControl.grid[x - 1, y];
		}
		if (input.direction.Contains ("right") && GameControl.grid[x, y + 1].pieceType != null) {
			source = GameControl.grid[x + 1, y];
		}
		if (input.direction.Contains ("up") && GameControl.grid[x + 1, y].pieceType != null) {
			source = GameControl.grid[x, y + 1];
		}
		if (input.direction.Contains ("down") && GameControl.grid[x - 1, y].pieceType != null) {
			source = GameControl.grid[x, y - 1];
		}
	}

	public virtual void highlight () {
		this.gameObject.GetComponent<MeshRenderer> ().material = GameControl.singleton.highlighted;
	}

	public virtual void lightsout () {
		this.gameObject.GetComponent<MeshRenderer> ().material = GameControl.singleton.normal;
	}
	public virtual void unitSpawn () {
		GameControl.spawnSelectedUnit (x, y);
	}

	public virtual void Inspect () {
		/*if(monitor != null) {
			Destroy(monitor);
			return;
		}
		monitor = GameControl.singleton.ShowMonitor ("Empty space!!!", transform.position);*/
	}
	public void OnMouseDown () {
		if (!EventSystem.current.IsPointerOverGameObject () && !GameControl.inspecting)
			unitSpawn ();
		else if(GameControl.inspecting)
			Inspect ();
	}

}