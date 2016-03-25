﻿using UnityEngine;
using System.Collections;

public abstract class MazeCellEdge : MonoBehaviour {
	public MazeCell cell, otherCell;
	public MazeDirection direction;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction){
		this.cell = cell;
		this.otherCell = cell;
		this.direction = direction;
		cell.SetEdge (direction, this);
		transform.parent = cell.transform;
		transform.localPosition = Vector3.zero;
	}
}
