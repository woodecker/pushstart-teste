using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBuilding : MonoBehaviour {

	public GameObject building;
	public Text costText;

	void Start(){
		costText.text = building.GetComponent<Building> ().cost.ToString();
	}

	void Update(){
		GetComponent<Button> ().interactable = (building.GetComponent<Building> ().cost <= Player.instance.Money);
	}
}
