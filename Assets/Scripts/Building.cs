using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	public int cost = 10;
	public int prize = 5;
	public float timeToCreate = 10f;	//seconds
	public float timeToPrize = 60f;		//seconds

	public enum State {
		Idle,
		Dragging,
		Creating,
		Completed
	}

	public State state;

	void Awake () {
		state = State.Idle;

		//z-order
		float zPosition = Gameplay.instance.zLayer + transform.position.y * 0.1f;
		transform.position = new Vector3 (transform.position.x, transform.position.y, zPosition);
	}

	public void Drag(){
		state = State.Dragging;
	}

	public void Stop(){
		state = State.Creating;
	}

	void Update () {
		if (state == State.Dragging) {
			//follow mouse
			Vector3 myPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//z-order
			float zPosition = Gameplay.instance.zLayer + myPosition.y * 0.1f;
			transform.position = new Vector3 (myPosition.x, myPosition.y, zPosition);
		}
	}
}
