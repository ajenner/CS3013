using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour {
	public IntVector2 coordinates;
	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public MazeCellEdge GetEdge(MazeDirection direction) {
		return edges [(int)direction];
	}

	public void SetEdge(MazeDirection direction, MazeCellEdge edge){
		edges [(int)direction] = edge;
	}
}
