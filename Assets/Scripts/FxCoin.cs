using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxCoin : MonoBehaviour {

	Vector3 myTarget;
	bool move = false;

	public void MoveCoin (Vector3 target) {
		myTarget = target;
		move = true;
	}
	
	void Update () {
		if (move) {
			transform.position = Vector2.MoveTowards (transform.position, myTarget, Time.deltaTime * 10f);
		}

		if (Vector3.Distance (transform.position, myTarget) < 0.1f) {
			//wait particles to arrive
			Invoke ("DestroyMe", 1f);
		}
	}

	void DestroyMe(){
		Destroy (gameObject);
	}
}
