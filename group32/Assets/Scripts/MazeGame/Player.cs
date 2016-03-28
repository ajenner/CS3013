using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public MazeCell currentCell;
	public Player2 otherPlayer;
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
//		if (checkWin ()) {
//			Debug.Log ("Winner winner chicken dinner");
//		}

		if (Input.GetKeyDown (KeyCode.W)) {
			//Debug.Log ("North");
			Move (MazeDirection.North);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			//Debug.Log ("East");
			Move (MazeDirection.East);
		} else if (Input.GetKeyDown (KeyCode.A)) {
			//Debug.Log ("West");
			Move (MazeDirection.West);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			//Debug.Log ("South");
			Move (MazeDirection.South);
		}
	}

	private bool checkWin(){
		if (otherPlayer == null) {
			return false;
		}
		if(this.currentCell == otherPlayer.currentCell) {
			return true;
		}
		return false;
	}
}
