using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour {
	public MazeCell cellFab;
	public IntVector2 size;
	private MazeCell[,] cells;

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
		/*Lineral Cell generation*/
//		for (int x = 0; x < size.x; x++) {
//			for (int z = 0; z < size.z; z++) {
//				yield return delay;
//				createCell (new IntVector2(x, z));
//			}
//		}

		/*Random Cell Generation*/
		IntVector2 coord = RandomCoord;
		while (ContainsCoord (coord) && getCell(coord) == null) {
			yield return delay;
			createCell(coord);
			//Extension methods allow this to work, so nice
			coord += MazeDirections.RandomDirection.ToIntVector2();
		}

	}

	private void createCell(IntVector2 coord){
		MazeCell newCell = Instantiate(cellFab) as MazeCell;
		cells [coord.x, coord.z] = newCell;
		newCell.name = "Maze Cell " + coord.x + ", " + coord.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (coord.x - size.x * 0.5f + 0.5f, 0f, coord.z - size.z * 0.5f + 0.5f);
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
