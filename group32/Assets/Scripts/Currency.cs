using UnityEngine;
using System.Collections;

public class Currency : MonoBehaviour {
	private float currencyRemaining;
	// Use this for initialization
	void Start () {
		currencyRemaining = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
