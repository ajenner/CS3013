using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private MazeCell currentCell;
	// Use this for initialization
	void Start () {
	
	}

	public void SetLocation(MazeCell cell){
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
	}

	private void Move(MazeDirection direction){
		MazeCellEdge edge = currentCell.GetEdge (direction);
		if (edge is MazePassage) {
			Debug.Log ("Is this working?");
			SetLocation (edge.otherCell);
		}
	}

	void Update (){
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Debug.Log ("North");
			Move (MazeDirection.North);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Debug.Log ("East");
			Move (MazeDirection.East);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Debug.Log ("West");
			Move (MazeDirection.West);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Debug.Log ("South");
			Move (MazeDirection.South);
		}
	}
}
