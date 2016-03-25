using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour {
	public IntVector2 coordinates;
	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
	private int initializedEdgeCount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public MazeCellEdge GetEdge(MazeDirection direction) {
		return edges [(int)direction];
	}

	public bool IsFullyInitialized{
		get {
			return initializedEdgeCount == MazeDirections.Count;
		}
	}

	public void SetEdge (MazeDirection direction, MazeCellEdge edge) {
		edges [(int)direction] = edge;
		initializedEdgeCount += 1;
	}

	public MazeDirection RandomUninitializedDirection{
		get {
			int skips = Random.Range (0, MazeDirections.Count - initializedEdgeCount);
			for (int i = 0; i < MazeDirections.Count; i++) {
				if (edges [i] == null) {
					if (skips == 0) {
						return (MazeDirection) i;
					}
					skips--;
				}
			}
			throw new System.InvalidOperationException ("MazeCell has no uninited directions left. Time to cry");
		}
	}
}
