using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {
	public MazeCell currentCell;
	public Player otherPlayer;

	public void SetLocation(MazeCell cell){
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
	}

	private void Move(MazeDirection direction){
		//Debug.Log (currentCell.coordinates.x + ", " + currentCell.coordinates.z);
		MazeCellEdge edge = currentCell.GetEdge (direction);
		if (edge is MazePassage) {
			SetLocation (edge.otherCell);
		}
	}

	void Update (){
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			//Debug.Log ("North");
			Move (MazeDirection.North);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			//Debug.Log ("East");
			Move (MazeDirection.East);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			//Debug.Log ("West");
			Move (MazeDirection.West);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			//Debug.Log ("South");
			Move (MazeDirection.South);
		}
	}
}