using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {

	private bool paused = false;
	public bool creating = false;

	public static Gameplay instance;

	public GameObject dragBuild;

	public float zLayer = 0f;

	void Awake () {
		instance = this.GetComponent<Gameplay> ();
	}
	
	void Update () {

	}

	void PauseGame(){
		paused = true;
	}

	void UnpauseGame(){
		paused = false;
	}

	bool isPaused(){
		return paused;
	}

	public void NewBuilding(GameObject building){

		if (building.GetComponent<Building> ().cost > Player.instance.Money)
			return;

		Debug.Log ("new building");

		//follow mouse or touch
		Vector3 myPosition;
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			myPosition = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 0f));
		} else {
			myPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}

		dragBuild = Instantiate (building, myPosition, Quaternion.identity);
		dragBuild.GetComponent<Building> ().Drag ();

		creating = true;
	}

	public void ReleaseBuilding(){

		if (dragBuild == null)
			return;

		Debug.Log ("try to build");

		//if money and empty grid
		Building b = dragBuild.GetComponent<Building> ();

		if (!b.isColliding() && Player.instance.TakeMoney(b.cost)) {
			b.Stop ();
		} else {
			Destroy (dragBuild);
		}

		dragBuild = null;

		creating = false;
	}
}
