using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PushStart_API : MonoBehaviour {

	public InputField inputUsername;
	public InputField inputPassword;
	public Button submitButton;
	
	public void SubmitForm () {
		Debug.Log (inputUsername.text);
		Debug.Log (inputPassword.text);

		submitButton.interactable = false;

		LoginForm.instance.LoginUser (inputUsername.text, inputPassword.text, ()=>SceneManager.LoadScene("Gameplay"));


	}
}
