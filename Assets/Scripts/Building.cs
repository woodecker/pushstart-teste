using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	public int cost = 10;
	public int prize = 5;
	public float timeToCreate = 10f;	//seconds
	public float timeToPrize = 60f;		//seconds
	float t_nextPrize = 0f;

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

	private Vector3 coinWorldPosition;
	RectTransform myRect;
	public GameObject prefabCoin;
	float fxCoinForce = 0.1f;

	void Awake () {

		t_nextPrize = timeToPrize;

		state = State.Idle;
		sp = GetComponent<SpriteRenderer> ();
	}

	void Start(){
		//z-order
		float zPosition = Gameplay.instance.zLayer + transform.position.y * 0.1f;
		transform.position = new Vector3 (transform.position.x, transform.position.y, zPosition);

		clock = transform.Find ("Clock");

		GetCoinPosition ();
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
		} else if (state == State.Creating) {

			timeToCreate -= Time.deltaTime;
			if (timeToCreate <= 0f) {
				state = State.Completed;
			}
		} else if (state == State.Completed) {
			t_nextPrize -= Time.deltaTime;
			if (t_nextPrize <= 0) {
				Player.instance.Money += prize;
				t_nextPrize = timeToPrize;

				//fx
				GetCoinPosition();	//UI coin position can move
				GameObject fxCoin = Instantiate(prefabCoin, transform.position, Quaternion.identity);
				fxCoin.GetComponent<FxCoin>().MoveCoin(coinWorldPosition);
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

	void GetCoinPosition(){
		GameObject uiCoin = GameObject.Find ("PlayerCoin");
		if (uiCoin != null) {
			myRect = uiCoin.GetComponent<RectTransform> ();

			RectTransformUtility.ScreenPointToWorldPointInRectangle (myRect,
				new Vector2 (myRect.position.x, myRect.position.y),
				Camera.main,
				out coinWorldPosition);
		}
	}
}
