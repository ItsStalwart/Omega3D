using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	/*public static int[,] coords = new int[,] {{0, 1}, {0, -1}, {1, 0}, {-1, 0}};

	void CheckNeighbors(int x, int y) {
		
		for(int i = 0; i < 4; ++i){
			int _x = x + coords[i, 0];
			int _y = y + coords[i, 1];
			matrix[_x, _y];
		}
	}*/
	public static GameControl singleton;
	public static bool inspecting = false;
	public int gridSize;

	private Transform boardHolder;
	public GameObject generator;
	public GameObject receiver;
	public GameObject[] transmitter;
	public GameObject[] converter;
	public GameObject[] ampmixer;
	public GameObject[] supressor;
	public GameObject boardUnit;
	public GameObject victoryStuff;
	public GameObject transStuff;
	public GameObject convStuff;
	public GameObject ampStuff;

	private GameObject selectedUnit;
	public int selectedUnitRotation;

	public Text textInput;
	public Text textOutput;

	public static GridUnit[,] grid;
	public int [,] puzzleMatrix;
	//receptores
	public List<int> receiverTypes = new List<int>();
	public List<string> receiverDirections = new List<string>();
	public List<int> receiverLowerLimits = new List<int>();
	public List<int> receiverUpperLimits = new List<int>();
	//geradores
	public List<Signal> generatorSignals = new List<Signal>();

	protected List<Receiver> clearList = new List<Receiver>();

	public GameObject tutStuff1;
	public GameObject tutStuff2;
	public GameObject tutStuff3;

	public GameObject monitorPrefab;

	public Material normal, highlighted;

	void Awake(){
		if (singleton == null) {
			singleton = this;
		}
		fetchPuzzle ();
		grid = new GridUnit[puzzleMatrix.GetLength(0), puzzleMatrix.GetLength(1)];
		spawnBoard ();
		stablishReceivers ();
		if (PuzzleControl.singleton.selectedPuzzle == 0) {
			tutStuff1.SetActive (true);
			convStuff.SetActive (false);
			ampStuff.SetActive (false);
		}

		if (PuzzleControl.singleton.selectedPuzzle == 1) {
			tutStuff2.SetActive (true);
			convStuff.SetActive (false);
		}
		if (PuzzleControl.singleton.selectedPuzzle == 2) {
			tutStuff3.SetActive (true);
			ampStuff.SetActive (false);
		}
	}
	void Update() {
		//if(Input.GetKeyDown(KeyCode.Space)) ToggleInpect(null);
	}
	bool preventCallWinRoutineMultipleTimes = false;
	public void winCheck(){
		int allCleared = 0;
		for (int i = 0; i < clearList.Count; i++) {
			if (clearList [i].cleared) {
				allCleared++;
			} else {
				allCleared--;
			}
		}
		if (allCleared == clearList.Count && !preventCallWinRoutineMultipleTimes) {
			preventCallWinRoutineMultipleTimes = true;
			StartCoroutine ("winRoutine");
		}
	}

	public IEnumerator winRoutine(){
		victoryStuff.SetActive (true);
		PlayerPrefs.SetInt ("PuzzleCleared", PlayerPrefs.GetInt ("PuzzleCleared", 1)+1);
		yield return new WaitForSeconds (2f);
	}

	public void ReloadMenu() {
		SceneManager.LoadScene ("menu");
	}

	void spawnBoard(){
		boardHolder = new GameObject ("Board").transform;
		int genCount = 0;
		for (int i = 0; i < puzzleMatrix.GetLength(0); i++) {
			for (int j = 0; j < puzzleMatrix.GetLength(1); j++) {
				if (puzzleMatrix [i, j] == 0) {
					GameObject boardTile = Instantiate (generator, new Vector3 (i, 0, j), Quaternion.identity) as GameObject;
					boardTile.transform.SetParent (boardHolder);
					boardTile.GetComponent<GridUnit> ().x = i;
					boardTile.GetComponent<GridUnit> ().y = j;
					grid [i, j] = boardTile.GetComponent<GridUnit> ();
					grid [i, j].output = generatorSignals [genCount];
					genCount++;
					if (grid [i, j].output.direction.Contains("left") ){
						boardTile.transform.localEulerAngles = new Vector3(-90, 0, 180);
					}if (grid [i, j].output.direction.Contains("right")) {
						boardTile.transform.localEulerAngles = new Vector3(-90, 0, 0);
					}if (grid [i, j].output.direction.Contains("up")) {
						boardTile.transform.localEulerAngles = new Vector3(-90, 0, -90);
					}if (grid [i, j].output.direction.Contains("down")) {
						boardTile.transform.localEulerAngles = new Vector3(-90, 0, 90);
					}
				}
				if (puzzleMatrix [i, j] == 1) {
					GameObject boardTile = Instantiate (receiver, new Vector3 (i, 0, j), Quaternion.identity) as GameObject;
					boardTile.transform.SetParent (boardHolder);
					boardTile.GetComponent<GridUnit> ().x = i;
					boardTile.GetComponent<GridUnit> ().y = j;
					grid [i, j] = boardTile.GetComponent<GridUnit> ();
					clearList.Add (boardTile.gameObject.GetComponent<Receiver> ());
					//recCount++;
				}
				if (puzzleMatrix [i, j] == 5) {
					GameObject boardTile = Instantiate (boardUnit, new Vector3 (i, -0.5f, j), Quaternion.identity) as GameObject;
					boardTile.transform.SetParent (boardHolder);
					boardTile.GetComponent<GridUnit> ().x = i;
					boardTile.GetComponent<GridUnit> ().y = j;
					grid [i, j] = boardTile.GetComponent<GridUnit> ();
				}
			}
		}	
	}


	/*public class puzzle{
		int puzzleWidth;
		int puzzleHeight;

		receiverList(rec1,rec2...)


		void startPuzzle(){
			new int [puzzleWidth,puzzleHeight];
			jogar elementos dentro da matriz
		}
	}*/

	/*public int [,] puzzleMatrix;
	//receptores
	public List<int> receiverTypes;
	public List<string> receiverDirections;
	public List<int> receiverLowerLimits;
	public List<int> receiverUpperLimits;
	//geradores
	public List<Signal> generatorSignals;*/
	void stablishReceivers(){
		for (int i = 0; i < clearList.Count; i++) {
			clearList [i].setInput (receiverDirections [i], receiverTypes [i], receiverLowerLimits [i], receiverUpperLimits [i]);
			if (clearList[i].inputDirection.Contains("left")){
						clearList[i].transform.localEulerAngles = new Vector3(-90, 0, 180);
					}if (clearList[i].inputDirection.Contains("right")) {
						clearList[i].transform.localEulerAngles = new Vector3(-90, 0, 0);
					}if (clearList[i].inputDirection.Contains("up")) {
						clearList[i].transform.localEulerAngles = new Vector3(-90, 0, -90);
					}if (clearList[i].inputDirection.Contains("down")) {
						clearList[i].transform.localEulerAngles = new Vector3(-90, 0, 90);
					}
		}
	}
	void fetchPuzzle(){// 5 = vazio, 0 = gerador, 1 = receptor  // grid de puzzle = rotateLeft da matriz
		if (PuzzleControl.singleton.selectedPuzzle == 0) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 5, 5, 1, 5, 5, 5, 5, 5 }, // puzzleMatrix = int[#col,#lin]
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 0, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
			};
			generatorSignals.Add(new Signal(1,150,"right")); //lista de sinais dos geradores NA ORDEM QUE APARECEM NA MATRIZ
			receiverTypes.Add (1); // lista de tipos de input dos receptores NA ORDEM QUE APARECEM NA MATRIZ
			receiverDirections.Add ("right"); // lista de direções de input dos receptores NA ORDEM QUE APARECEM NA MATRIZ
			receiverLowerLimits.Add (100); // lista de limites INFERIORES de input dos receptores NA ORDEM QUE APARECEM NA MATRIZ
			receiverUpperLimits.Add (200); // lista de limites SUPERIORES de input dos receptores NA ORDEM QUE APARECEM NA MATRIZ
		}
		if (PuzzleControl.singleton.selectedPuzzle == 1) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 1, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 0, 5, 5, 5, 5, 5 }
			};
			generatorSignals.Add(new Signal (1, 200, "up"));
			receiverTypes.Add (1);
			receiverDirections.Add ("up");
			receiverLowerLimits.Add (700);
			receiverUpperLimits.Add (1000);

		}
		if (PuzzleControl.singleton.selectedPuzzle == 2) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 1, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 0, 5 }
			};
			generatorSignals.Add(new Signal (1, 400, "down"));
			receiverTypes.Add (2);
			receiverDirections.Add ("left");
			receiverLowerLimits.Add (100);
			receiverUpperLimits.Add (200);
		}
		if (PuzzleControl.singleton.selectedPuzzle == 3) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 0, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 1, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 1, 5, 5, 5, 5, 0, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
			};
			generatorSignals.Add(new Signal(1,150,"right"));
			generatorSignals.Add(new Signal (2, 150, "down"));
			receiverTypes.Add (1);
			receiverTypes.Add (3);
			receiverDirections.Add ("right");
			receiverDirections.Add ("right");
			receiverLowerLimits.Add (225);
			receiverLowerLimits.Add (75);
			receiverUpperLimits.Add (350);
			receiverUpperLimits.Add (125);
		}
		if (PuzzleControl.singleton.selectedPuzzle == 4) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 0 },
											 { 1, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
										 	 { 5, 5, 5, 5, 1, 5, 5, 5, 5, 5 }
			};			
			generatorSignals.Add(new Signal (1, 200, "right"));
			receiverTypes.Add (1);
			receiverTypes.Add (1);
			receiverDirections.Add ("left");
			receiverDirections.Add ("down");
			receiverLowerLimits.Add (150);
			receiverLowerLimits.Add (200);
			receiverUpperLimits.Add (300);
			receiverUpperLimits.Add (250);
		}
		if (PuzzleControl.singleton.selectedPuzzle == 5) {
			puzzleMatrix = new int [15, 15] {{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 0, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 0, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
										 	 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5 },
										 	 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
			};			
			generatorSignals.Add(new Signal (1, 100, "right"));
			generatorSignals.Add(new Signal (2, 200, "left"));
			generatorSignals.Add(new Signal (1, 300, "up"));
			receiverTypes.Add (1);
			receiverTypes.Add (2);
			receiverTypes.Add (3);
			receiverDirections.Add ("left");
			receiverDirections.Add ("up");
			receiverDirections.Add ("down");
			receiverLowerLimits.Add (75);
			receiverLowerLimits.Add (200);
			receiverLowerLimits.Add (200);
			receiverUpperLimits.Add (300);
			receiverUpperLimits.Add (250);
			receiverUpperLimits.Add (700);
		}					
	
	}
	public static void spawnSelectedUnit(int px,int py){
		if (GameControl.singleton.selectedUnit != null) {
			GameObject boardTile = Instantiate (GameControl.singleton.selectedUnit, GameControl.singleton.boardHolder) as GameObject;
			boardTile.transform.position = new Vector3 (px, 0, py);
			boardTile.GetComponent<GridUnit> ().x = px;
			boardTile.GetComponent<GridUnit> ().y = py;
			boardTile.GetComponent<GridUnit> ().rotation = GameControl.singleton.selectedUnitRotation;
			Destroy (grid [px, py].gameObject);
			grid [px, py] = boardTile.GetComponent<GridUnit> ();
			if (GameControl.singleton.selectedUnitRotation == 1) {
				boardTile.transform.localEulerAngles = new Vector3(-90, 0, 90);
			}if (GameControl.singleton.selectedUnitRotation == 2) {
				boardTile.transform.localEulerAngles = new Vector3(-90, 0, 180);
			}if (GameControl.singleton.selectedUnitRotation == 3) {
				boardTile.transform.localEulerAngles = new Vector3(-90, 0, 270);
			}
			Debug.Log (GameControl.singleton.selectedUnit + " spawned at " + px.ToString () + "," + py.ToString () + " at rotation " + GameControl.singleton.selectedUnitRotation + "!");
		} else {
			Debug.Log ("This message should not show up");
		}
	}

	public void selectPiece(int selector){
		if (selector == 0) {
			selectedUnit = transmitter [0];
		}
		if (selector == 1) {
			selectedUnit = transmitter [1];
		}
		if (selector == 2) {
			selectedUnit = transmitter [2];	
		}
		if (selector == 3) {
			selectedUnit = transmitter [3];
		}
		if (selector == 4) {
			selectedUnit = transmitter [4];
		}
		if (selector == 5) {
			selectedUnit = transmitter [5];
		}
		if (selector == 6) {
			selectedUnit = transmitter [6];
		}
		if (selector == 7) {
			selectedUnit = converter [0];
		}
		if (selector == 8) {
			selectedUnit = converter [1];
		}
		if (selector == 9) {
			selectedUnit = ampmixer [0];
		}
		if (selector == 10) {
			selectedUnit = ampmixer [1];
		}
		if (selector == 11) {
			selectedUnit = ampmixer [2];
		}
		if (selector == 12) {
			selectedUnit = supressor [0];
		}
		if (selector == 13) {
			selectedUnit = supressor [1];
		}
		if (selector == 14) {
			selectedUnit = supressor [2];
		}
		Debug.Log (selectedUnit);
	}

	public void updateMonitor(string inputText){
		textInput.text = inputText;
	}

	public GameObject ShowMonitor(string inputText, Vector3 position){
		Debug.Log(position);
		GameObject monitor = Instantiate(monitorPrefab);
		monitor.transform.position = position;
		monitor.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = inputText;
		return monitor;
	}
	
	public void ToggleInpect(GameObject obj) {
		inspecting = !inspecting;
		if(obj != null) obj.SetActive(inspecting);
	}
}
