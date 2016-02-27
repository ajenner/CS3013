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
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.selected) {
			gameObject.GetComponent<Renderer> ().material = materialMatched;
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
		gameObject.GetComponent<Renderer> ().material = materialIdle;
		this.selected = false;
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
		this.selected = true;
		parent.notify (this);
		Debug.Log ("Tile " + id + " has been clicked");
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
		gameObject.GetComponent<Renderer> ().material = materialMatched;
	}
}
