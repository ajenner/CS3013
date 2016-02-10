using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	/**
	 * @param scene Index of the scene in build settings that is to be loaded
	 */
	public void LoadScene(int scene){
		SceneManager.LoadScene (scene);
	}
}
