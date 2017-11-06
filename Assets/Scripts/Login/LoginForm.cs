using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

[System.Serializable]
public class LoginInfo
{
	public string profile;
	public string token;

	public static LoginInfo CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<LoginInfo>(jsonString);
	}
}

[System.Serializable]
public class PlayerInfo
{
	public string nickname;
	public int money;

	public static PlayerInfo CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<PlayerInfo>(jsonString);
	}
}

public class LoginForm : MonoBehaviour {

	public static LoginForm instance;

	public string login_url = "http://dev.pushstart.com.br/desafio/public/api/auth/login";
	public string status_url = "http://dev.pushstart.com.br/desafio/public/api/status";

	//string username = "pusher";
	//string password = "b7e94be513e96e8c45cd23d162275e5a12ebde9100a425c4ebcdd7fa4dcd897c";	//senha sha256

	private string token;

	void Awake(){
		if (instance == null)
			instance = this;

		DontDestroyOnLoad (this.gameObject);
	}

	public void LoginUser(string username, string password, Action onFinished, Action<string> onError){
		StartCoroutine(LoginPushStart (username, password, onFinished, onError));
	}

	public void GetUser(){
		StartCoroutine(GetUserData (token));
	}

	IEnumerator LoginPushStart (string username, string password, Action onFinished, Action<string> onError) {

		WWWForm form = new WWWForm();

		form.AddField( "username", username );
		form.AddField( "password", PasswordEncryption(password) );

		WWW download = new WWW( login_url, form );

		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			print( "Error downloading: " + download.error );
			onError (download.error);
		} else {
			Debug.Log(download.text);

			LoginInfo lg = new LoginInfo ();
			lg = LoginInfo.CreateFromJSON (download.text);
			Debug.Log (lg.token);

			token = lg.token;
			onFinished ();
			//StartCoroutine(GetUserData (lg.token));
		}
	}

	IEnumerator GetUserData(string token){

		WWWForm form = new WWWForm();

		Hashtable headers = new Hashtable ();
		headers.Add( "X-Authorization", token );

		WWW download = new WWW( status_url, null, headers );

		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			print( "Error downloading: " + download.error );
		} else {
			Debug.Log(download.text);

			PlayerInfo pl = new PlayerInfo ();
			pl = PlayerInfo.CreateFromJSON (download.text);
			Debug.Log (pl.money);

			//set player info
			Player.instance.Money = pl.money;
			Player.instance.Nickname = pl.nickname;
		}
	}

	string PasswordEncryption(string passwordString){
		UTF8Encoding ue = new UTF8Encoding ();
		byte[] bytes = ue.GetBytes (passwordString);

		SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider ();
		byte[] hashBytes = sha256.ComputeHash(bytes);

		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++) {
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}
}