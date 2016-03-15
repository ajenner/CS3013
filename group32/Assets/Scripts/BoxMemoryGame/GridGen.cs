using UnityEngine;
using System.Collections;

public class GridGen : MonoBehaviour {
	public Tile tilePrefab;

	bool isFirstPlayerTurn = true;
	bool inEnable = true;

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

	//GameControl
	Tile p1Selected;
	Tile p2Selected;
	int winCount;

	// Use this for initialization
	void Start () {
		winCount = width * height;
		if ((width * height) % 2 != 0) {
			throw new System.InvalidOperationException ("Could not create game. Not an even number in the grid");
		}

		if ((width * height) / 2 > 30) {
			throw new System.InvalidOperationException ("Too large. Max number of cubes is 30 (6 * 5)");
		}
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
				//Set tile's parent
				newTile.setParent (this);
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

		int idNumber = 0;
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

					grid [randomX2, randomY2].setID (idNumber);
					grid [randomX, randomY].setID (idNumber);

					//TODO Add in content of boxes here.

					//Add them to the assigned list. Remove them from the unassinged list.
					tilesUnassigned.Remove(tile1);
					tilesUnassigned.Remove(tile2);

					idNumber++;
				//	Debug.Log ("Tile [" + randomX + "," + randomY + "] and tile [" + randomX2 + "," + randomY2 + "] are now paired"); 
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

	void OnGUI()
	{
		if (winCount == 0)
			GUI.Label (new Rect (Screen.width/2.0f, Screen.height/2.0f, 500, 200), "WINNER!"); 
		else
			GUI.Label (new Rect (10, 10, 100, 100),
				(inEnable) ? ((isFirstPlayerTurn) ? "Player 1's turn" : "Player 2's turn") :"");

	}

	public void notify(Tile tile){

		//Debug.Log ("Notify was called " + isFirstPlayerTurn);
		if (isFirstPlayerTurn) {
			p1Selected = tile;
		} else {
			p2Selected = tile;
			inEnable = false;

			bool isMatch = p1Selected.checkPair (p2Selected);

			if (isMatch) {
				p1Selected.match ();
				p2Selected.match ();
				winCount -= 2;
				inEnable = true;
				//Debug.Log ("Matched! " + isMatch);
			} else {
				StartCoroutine(unSelectAfterDelay());
				//Debug.Log ("No match! " + isMatch);
			}
		}
		isFirstPlayerTurn = !isFirstPlayerTurn;
	}

	public bool inputEnable()
	{
		return this.inEnable;
	}


	IEnumerator unSelectAfterDelay(){
		yield return new WaitForSeconds (0.2f);

		Debug.Log ("Unselecting");
		this.p2Selected.unSelect();
		this.p1Selected.unSelect();
		inEnable = true;
		yield return null;
	}
}
