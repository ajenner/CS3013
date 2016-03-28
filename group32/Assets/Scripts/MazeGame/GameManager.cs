using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazeFab;
	private Maze myMaze;

	public Player playerFab;
	private Player myPlayer;

	public Player2 player2Fab;
	private Player2 myPlayer2;

	private void Start () {
		StartCoroutine (BeginGame());
	}

	private void Update () {
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			RestartGame();
		}

		//Check for win
		if (myPlayer == null || myPlayer2 == null) {
			return;
		}
		if(myPlayer.currentCell == myPlayer2.currentCell) {
			Debug.Log ("You win");
		}
		return;
	}

	private IEnumerator BeginGame () {
		myMaze = Instantiate (mazeFab) as Maze;
		yield return StartCoroutine (myMaze.generate ());
		myPlayer = Instantiate (playerFab) as Player;
		myPlayer.SetLocation (myMaze.getCell (myMaze.RandomCoord));

		myPlayer2 = Instantiate (player2Fab) as Player2;
		myPlayer2.SetLocation (myMaze.getCell (myMaze.RandomCoord));

		myPlayer.otherPlayer = myPlayer2;
		myPlayer2.otherPlayer = myPlayer;
	}

	private void RestartGame () {
		Destroy(myMaze.gameObject);
		StopAllCoroutines ();
		if (myPlayer != null) {
			Destroy (myPlayer.gameObject);
		}
		if (myPlayer2 != null) {
			Destroy (myPlayer2.gameObject);
		}
		StartCoroutine (BeginGame());
	}

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.yellow;
        GUI.Button(new Rect(Screen.width - 90, Screen.height - 50, 70, 20), "Options");
    }
}
