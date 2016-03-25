using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {
	public MazeCell cellFab;
	public IntVector2 size;
	private MazeCell[,] cells;
	public MazePassage passageFab;
	public MazeWall wallFab;

	public float generateDelay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public MazeCell getCell(IntVector2 coord){
		return cells [coord.x, coord.z];
	}

	public IEnumerator generate(){
		WaitForSeconds delay = new WaitForSeconds (generateDelay);
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep (activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep (activeCells);
		}
	}

	private void DoFirstGenerationStep(List<MazeCell> activeCells){
		activeCells.Add (CreateCell (RandomCoord));
	}

	private void DoNextGenerationStep(List<MazeCell> activeCells){
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells [currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt (currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coords = currentCell.coordinates + direction.ToIntVector2 ();
		if (ContainsCoord (coords)) {
			MazeCell neighbour = getCell (coords);
			if (neighbour == null) {
				neighbour = CreateCell (coords);
				CreatePassage (currentCell, neighbour, direction);
				activeCells.Add (neighbour);
			} else {
				CreateWall (currentCell, neighbour, direction);
				//activeCells.RemoveAt (currentIndex);
			}
		} else {
			CreateWall (currentCell, null, direction);
			//activeCells.RemoveAt (currentIndex);
		}
	}

	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate(cellFab) as MazeCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction){
		MazePassage passage = Instantiate (passageFab) as MazePassage;
		passage.Initialize (cell, otherCell, direction);
		passage = Instantiate (passageFab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction){
		MazeWall wall = Instantiate (wallFab) as MazeWall;
		wall.Initialize (cell, otherCell, direction);
		if(otherCell != null){
			wall = Instantiate (wallFab) as MazeWall;
			wall.Initialize (otherCell, cell, direction.GetOpposite ());
		}
	}

	public IntVector2 RandomCoord{
		get {
			return new IntVector2 (Random.Range (0, size.x), Random.Range (0, size.z));
		}
	}

	public bool ContainsCoord(IntVector2 coord){
		return coord.x >= 0 && coord.x < size.x && coord.z >= 0 && coord.z < size.z;
	}
}
