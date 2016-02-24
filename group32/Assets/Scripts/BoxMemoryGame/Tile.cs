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
	
	}

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

	void OnMouseOver(){
		if (!matched) {
			gameObject.GetComponent<Renderer> ().material = materialLightUp;
		}
	}
	 
	void OnMouseExit(){
		if (!matched) {
			gameObject.GetComponent<Renderer> ().material = materialIdle;
		}
	}

	void OnMouseDown(){
		parent.notify (this);
		Debug.Log ("Tile " + id + " has been clicked");
	}

	public bool checkPair(Tile other){
		return other.Equals (pair);
	}

	public void match(){
		this.matched = true;
		gameObject.GetComponent<Renderer> ().material = materialMatched;
	}
}
