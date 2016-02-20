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
	static ArrayList tilesGuessed;
	static ArrayList tilesUnguessed;
	// Use this for initialization
	void Start () {
		CreateTiles ();
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
				zOffSet+=distanceBetweenTiles;
			}
		}
		//AssignPictures ();
	}

//	void AssignPictures(){
//		for (int x = 0; x < width; x++) {
//			for (int y = 0; y < height; y++) {
//				tilesUnguessed.Add (grid [x, y]);
//			}
//		}
//		tilesGuessed = new ArrayList ();
//	}
}
