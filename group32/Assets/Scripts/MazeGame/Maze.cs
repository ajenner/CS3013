using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour {
	public int sizeX, sizeZ;
	public MazeCell cellFab;
	private MazeCell[,] cells;

	public float generateDelay;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator generate(){
		WaitForSeconds delay = new WaitForSeconds (generateDelay);
		cells = new MazeCell[sizeX,sizeZ];
		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				yield return delay;
				createCell (x, z);
			}
		}
	}

	private void createCell(int x, int z){
		MazeCell newCell = Instantiate(cellFab) as MazeCell;
		cells [x, z] = newCell;
		newCell.name = "Maze Cell " + x + ", " + z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (x - sizeX * 0.5f + 0.5f, 0f, z - sizeZ * 0.5f + 0.5f);
	}
}
