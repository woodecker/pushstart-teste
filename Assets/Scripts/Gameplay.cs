using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {

	private bool paused;

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
		Vector3 myPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		dragBuild = Instantiate (building, myPosition, Quaternion.identity);
		dragBuild.GetComponent<Building> ().Drag ();
	}

	public void ReleaseBuilding(){

		if (dragBuild == null)
			return;

		Debug.Log ("try to build");
		//if money and empty grid
		if (Player.instance.TakeMoney(dragBuild.GetComponent<Building> ().cost)) {
			dragBuild.GetComponent<Building> ().Stop ();
		} else {
			Destroy (dragBuild);
		}

		dragBuild = null;
	}
}
