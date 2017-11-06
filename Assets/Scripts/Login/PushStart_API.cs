using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PushStart_API : MonoBehaviour {

	public InputField inputUsername;
	public InputField inputPassword;
	public Button submitButton;

	public GameObject panelTryAgain;
	public Text panelMessage;

	void Start(){
		panelTryAgain.SetActive(false);
	}

	public void SubmitForm () {
		Debug.Log (inputUsername.text);
		Debug.Log (inputPassword.text);

		submitButton.interactable = false;

		Action<string> act = (s) => {
			TryAgain(s);
		};

		LoginForm.instance.LoginUser (inputUsername.text, 
			inputPassword.text, 
			() => SceneManager.LoadScene ("Gameplay"),
			act
		);
	}

	public void TryAgain(string errorMessage){
		panelMessage.text = errorMessage;
		submitButton.interactable = true;
		panelTryAgain.SetActive(true);
	}
}
