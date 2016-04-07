using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazeFab;
	private Maze myMaze;

	public Player playerFab;
	private Player myPlayer;

	public Player2 player2Fab;
	private Player2 myPlayer2;

	bool win;

	private void Start () {
		win = false;
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
			win = true;
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
		win = false;
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

    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.yellow;
        GUI.Button(new Rect(Screen.width - 90, Screen.height - 50, 70, 20), "Options");

		if (win) {
			style.fontSize = 60;
			style.normal.textColor = Color.yellow;
			style.fontStyle = FontStyle.Bold;
			GUI.Label(new Rect((Screen.width / 2.0f) - (.095f * (Screen.width)), 100, 500, 200), "WINNER!", style);
			style.fontSize = 15;
			GUI.Label(new Rect((Screen.width / 2.0f) - (.11f * (Screen.width)), 160, 500, 200), "Press menu to play again or return to the lobby...", style);
			style.fontStyle = FontStyle.Normal;
		}
    }
}
