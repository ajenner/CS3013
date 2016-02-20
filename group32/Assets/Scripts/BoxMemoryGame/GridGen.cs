using UnityEngine;
using System.Collections;

public class GridGen : MonoBehaviour {
	public Tile tilePrefab;

	//Difficulty vars
	public int width = 3;
	public int height = 4;

	//Positioning vars
	public float distanceBetweenTiles = 1.0f;

	//Game
	static Tile[,] grid;
	static ArrayList assets;
	static ArrayList usedAssets;
	static ArrayList tilesUnassigned;

	// Use this for initialization
	void Start () {
		CreateAssets ();
		CreateTiles ();
	}

	void CreateAssets(){
		//Initalise the array with some random assets for use in the boxes.

	}

	void CreateTiles () {
		grid = new Tile[width, height];

		float xOffSet = 0.0f;
		float zOffSet = 0.0f;

		for (int x = 0; x < width; x++) {
			xOffSet += distanceBetweenTiles;
			zOffSet = 0;
			for (int y = 0; y < height; y++) {
				Tile newTile = (Tile)Instantiate (tilePrefab, new Vector3 (transform.position.x + xOffSet, transform.position.y, transform.position.z + zOffSet),
					               transform.rotation);
				grid [x, y] = newTile;
				zOffSet += distanceBetweenTiles;
			}
		}
		AssignPictures ();
	}

	void AssignPictures(){
		tilesUnassigned = new ArrayList ();
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tilesUnassigned.Add(grid [x, y]);
			}
		}
		//tilesAssigned = new ArrayList ();

		//Select 2 unassigned tiles
		while (tilesUnassigned.Count != 0) {
			//Generate first random tile
			int randomX = Random.Range (0, width);
			int randomY = Random.Range (0, height);

			Tile tile1 = grid [randomX, randomY];
			//Check to see if it's unassinged
			if (tilesUnassigned.Contains (tile1)) {
				//Generate random pair
				int randomX2 = Random.Range (0, width);
				int randomY2 = Random.Range (0, height);

				//Make sure the same index isnt picked.
				Tile tile2 = grid [randomX2, randomY2];
				if (randomX == randomX2 && randomY == randomY2) {
					continue;
				}
				//Check to see if second tile is unassinged
				if (tilesUnassigned.Contains (tile2)) {
					//Give them both the same contents and make them aware of their twin
					grid[randomX, randomY].setPair(grid[randomX2, randomY2]);
					grid [randomX2, randomY2].setPair (grid [randomX, randomY]);

					//TODO Add in content of boxes here.

					//Add them to the assigned list. Remove them from the unassinged list.
					tilesUnassigned.Remove(tile1);
					tilesUnassigned.Remove(tile2);

					Debug.Log ("Tile [" + randomX + "," + randomY + "] and tile [" + randomX2 + "," + randomY2 + "] are now paired"); 
				} else {
					//It's assigned already, move on
					continue;
				}

			} else {
				//Is assigned already, move on.
				continue;
			}
				
		}
	}
}
