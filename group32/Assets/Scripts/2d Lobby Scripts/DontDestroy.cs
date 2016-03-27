using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(transform.gameObject);
		}
	}
}
