using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	//GridGen Parent
	GridGen parent;
	//Tile Pair
	Tile pair;

	//Identity
	int id;

	//Content
	GameObject content;
	public TextMesh displayText;
	public Material materialIdle;
	public Material materialLightUp;
	public Material materialMatched;

	//Gameplay bools
	public bool occupied = false;
	public bool selected = false;
	public bool matched = false;

	// Use this for initialization
	void Start () {
		displayText.GetComponent<Renderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.selected) {
			Debug.Log ("Trying to change colour of " + id);
			gameObject.GetComponent<Renderer> ().material = materialMatched;
			displayText.GetComponent<Renderer> ().enabled = true;
		} else if (this.matched) {
			gameObject.GetComponent<Renderer> ().material = materialMatched;
			displayText.GetComponent<Renderer> ().enabled = true;
		}
	}

	/*
	 * Getters and Setters
	 */
	public void setPair(Tile x){
		this.pair = x;
	}

	public void setID(int x){
		this.id = x;
		displayText.text = this.id.ToString ();
	}

	public void setParent(GridGen parent){
		this.parent = parent;
	}

	public void unSelect(){
		if (!matched) {
			//Set this to true to enable easy easy mode :)
			displayText.gameObject.GetComponent<Renderer> ().enabled = false;
			this.gameObject.GetComponent<Renderer> ().material = materialIdle;
			this.selected = false;
		}
	}


	/*
	 * Mouse over functions. Covers highligting
	 */
	void OnMouseOver(){
		if (!matched || !selected) {
			gameObject.GetComponent<Renderer> ().material = materialLightUp;
		}
	}
	 
	void OnMouseExit(){
		if (!matched || !selected) {
			gameObject.GetComponent<Renderer> ().material = materialIdle;
		}
	}

	/*
	 * Mouse down, covers selecting of a tile
	 */
	void OnMouseDown(){
		//Debug.Log ("Tile " + id + " has been clicked");
		if (!matched || !selected) {
			this.selected = true;
			Debug.Log ("Tile " + id + " has been selected correctly");
			parent.notify (this);
		}
	}


	/*
	 * Called once 2 tiles have been selected. Checks if a match has been made
	 */
	public bool checkPair(Tile other){
		return other.Equals (pair);
	}


	/*
	 * Handles the changing of colours once tile has been matched.
	 */ 
	public void match(){
		this.matched = true;
//		gameObject.GetComponent<Renderer> ().material = materialMatched;
//		displayText.GetComponent<Renderer> ().enabled = true;
	}

	public void select(){
		this.selected = true;
	}
}
