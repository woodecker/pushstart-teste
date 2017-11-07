using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxCoin : MonoBehaviour {

	Vector3 myTarget;
	bool move = false;

	GameObject uiCoin;
	RectTransform myRect;
	private Vector3 coinWorldPosition;

	void Start(){
		uiCoin = GameObject.Find ("PlayerCoin");
		if (uiCoin != null) {
			myRect = uiCoin.GetComponent<RectTransform> ();
		}
		GetCoinPosition ();
		move = true;
	}
	
	void Update () {
		if (move) {
			GetCoinPosition ();
			transform.position = Vector2.MoveTowards (transform.position, coinWorldPosition, Time.deltaTime * 10f);
		}
		
		if (Vector3.Distance (transform.position, coinWorldPosition) < 0.1f) {
			//wait particles to arrive
			Invoke ("DestroyMe", 1f);
		}
	}

	void DestroyMe(){
		Destroy (gameObject);
	}

	void GetCoinPosition(){
		
		if (myRect != null) {
			RectTransformUtility.ScreenPointToWorldPointInRectangle (myRect,
				new Vector2 (myRect.position.x, myRect.position.y),
				Camera.main,
				out coinWorldPosition);
		}
	}
}
