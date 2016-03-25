using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazeFab;
	private Maze myMaze;

	public Player playerFab;
	private Player myPlayer;
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
