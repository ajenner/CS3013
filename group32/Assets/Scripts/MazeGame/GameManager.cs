using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazeFab;
	private Maze myMaze;

	private void Start () {
		BeginGame();
	}

	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	private void BeginGame () {
		myMaze = Instantiate (mazeFab) as Maze;
		StartCoroutine (myMaze.generate ());
	}

	private void RestartGame () {
		Destroy(myMaze.gameObject);
		StopAllCoroutines ();
		BeginGame ();
	}
}
