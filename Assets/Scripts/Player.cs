﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public string nickname;
	public string Nickname {
		get {return nickname;}
		set {nickname = value; UpdateGui ();}
	}

	public int money = 999;
	public int Money {
		get {return money;}
		set {money = value; UpdateGui ();}
	}

	public Text nicknameText;
	public Text moneyText;

	public static Player instance;

	void Awake(){
		if (instance == null)
			instance = this.GetComponent<Player> ();
	}

	void Start () {

		LoginForm.instance.GetUser ();

		//nicknameText.text = nickname;
		//moneyText.text = money.ToString ();
	}
	
	void UpdateGui () {
		nicknameText.text = nickname;
		moneyText.text = money.ToString ();
	}

	public bool TakeMoney(int quant){
		if (money - quant >= 0) {
			money -= quant;
			UpdateGui ();
			return true;
		} else {
			return false;
		}
	}
}
