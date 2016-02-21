using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	//Tile Pair
	Tile pair;

	//Identity
	int id;

	//Content
	GameObject content;
	public TextMesh displayText;
	public Material materialIdle;
	public Material materialLightUp;

	//Gameplay bools
	public bool occupied = false;
	public bool selected = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPair(Tile x){
		this.pair = x;
	}

	public void setID(int x){
		this.id = x;
		displayText.text = this.id.ToString ();
	}

	void OnMouseOver(){
		gameObject.GetComponent<Renderer> ().material = materialLightUp;
	}
	 
	void OnMouseExit(){
		gameObject.GetComponent<Renderer> ().material = materialIdle;
	}

	void OnMouseDown(){
		Debug.Log ("Tile " + id + " has been clicked");
	}
		
}
