using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBuilding : MonoBehaviour {

	public GameObject building;

	void Update(){
		GetComponent<Button> ().interactable = (building.GetComponent<Building> ().cost <= Player.instance.Money);
	}
}
