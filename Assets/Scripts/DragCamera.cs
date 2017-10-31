using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour {

	private Vector3 startPos;

	public float zPosition = -10f;

	void Update() {

		if (Gameplay.instance.creating)
			return;

		if (Input.GetMouseButtonDown (0)) {
			startPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			startPos.z = zPosition;
		}

		if (Input.GetMouseButton (0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos.z = zPosition;
			transform.position += startPos - mousePos;
		}
	}
}
