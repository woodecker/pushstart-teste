using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour {


	public float zPosition = -10f;

	void Update() {

		if (Gameplay.instance.creating)
			return;

		if (Input.GetMouseButtonDown (0)) {
		}

		if (Input.GetMouseButton (0)) {
		}
	}
}
