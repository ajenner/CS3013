﻿using UnityEngine;
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
			SetLocation (edge.otherCell);
		}
	}

	private void Update(){
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Move (MazeDirection.North);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Move (MazeDirection.East);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Move (MazeDirection.West);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Move (MazeDirection.South);
		}
	}
}
