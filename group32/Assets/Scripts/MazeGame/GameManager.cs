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
		StartCoroutine (BeginGame());
	}
}
