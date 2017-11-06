using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour {

	private Vector3 startPos;

	public float zPosition = -10f;

	void Update() {

		if (Gameplay.instance.creating)
			return;

		if (Input.touchCount > 0) {

			/*** TOUCH INPUT ***/

			Touch touch = Input.GetTouch (0);

			switch (touch.phase) {
			case TouchPhase.Began:
				startPos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 0f));
				startPos.z = zPosition;
				break;

			case TouchPhase.Moved:
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 0f));
				mousePos.z = zPosition;
				transform.position += startPos - mousePos;
				break;

			case TouchPhase.Ended:
				break;
			}
		} else {

			/*** MOUSE INPUT ***/

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

}
