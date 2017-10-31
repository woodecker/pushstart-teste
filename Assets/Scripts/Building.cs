using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	public int cost = 10;
	public int prize = 5;
	public float timeToCreate = 10f;	//seconds
	public float timeToPrize = 60f;		//seconds

	Transform clock;
	private bool colliding = false;
	SpriteRenderer sp;

	public enum State {
		Idle,
		Dragging,
		Creating,
		Completed
	}

	public State state;

	void Awake () {
		state = State.Idle;
		sp = GetComponent<SpriteRenderer> ();
	}

	void Start(){
		//z-order
		float zPosition = Gameplay.instance.zLayer + transform.position.y * 0.1f;
		transform.position = new Vector3 (transform.position.x, transform.position.y, zPosition);

		clock = transform.Find ("Clock");
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
			Vector3 myPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			//z-order
			float zPosition = Gameplay.instance.zLayer + myPosition.y * 0.1f;
			transform.position = new Vector3 (myPosition.x, myPosition.y, zPosition);

			CheckColliding ();
		}
		else if (state == State.Creating) {

			timeToCreate -= Time.deltaTime;
			if (timeToCreate <= 0f) {
				state = State.Completed;
			}
		}

		//show clock if Creating
		clock.gameObject.SetActive(state == State.Creating);
	}

	void CheckColliding(){
		if (colliding) {
			GetComponent<SpriteRenderer> ().color = Color.red;
		} else {
			GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.CompareTag ("Building")) {
			colliding = true;
		}
	}

	void OnTriggerStay2D(Collider2D coll){
		if (coll.CompareTag ("Building")) {
			colliding = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.CompareTag ("Building")) {
			colliding = false;
		}
	}

	public bool isColliding(){
		return colliding;
	}
}
