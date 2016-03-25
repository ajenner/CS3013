using UnityEngine;
using System.Collections;

public enum MazeDirection {
	North,
	East,
	South,
	West
}

public static class MazeDirections {
	public const int Count = 4;

	public static MazeDirection RandomDirection{
		get {
			return (MazeDirection)Random.Range (0, Count);
		}
	}

	private static IntVector2[] vectors = {
		/*
		 * up
		 * right
		 * down
		 * left
		 */
		new IntVector2(0,1), 
		new IntVector2(1,0),
		new IntVector2(0,-1),
		new IntVector2(-1,0)
	};

	public static IntVector2 ToIntVector2 (this MazeDirection direction){
		return vectors [(int)direction];
	}
}