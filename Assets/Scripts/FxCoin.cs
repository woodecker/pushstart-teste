using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxCoin : MonoBehaviour {

	Vector3 myTarget;
	bool move = false;
	float timeLife = 3f;

	public void MoveCoin (Vector3 target) {
		myTarget = target;
		move = true;
	}
	
	void Update () {
		if (move) {
			transform.position = Vector2.MoveTowards (transform.position, myTarget, Time.deltaTime * 10f);
		}

		timeLife -= Time.deltaTime;
		if (timeLife <= 0f)
			Destroy (gameObject);
	}
}
