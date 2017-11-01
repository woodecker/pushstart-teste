using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	string login_url = "http://dev.pushstart.com.br/desafio/public/api/auth/login";
	string username = "pusher";
	string password = "b7e94be513e96e8c45cd23d162275e5a12ebde9100a425c4ebcdd7fa4dcd897c";	//senha sha256

	string status_url = "http://dev.pushstart.com.br/desafio/public/api/status";

	IEnumerator Start () {

		WWWForm form = new WWWForm();

		form.AddField( "username", username );
		form.AddField( "password", password );

		WWW download = new WWW( login_url, form );

		yield return download;

		if(!string.IsNullOrEmpty(download.error)) {
			print( "Error downloading: " + download.error );
		} else {
			Debug.Log(download.text);

			LoginInfo lg = new LoginInfo ();
			lg = LoginInfo.CreateFromJSON (download.text);
			Debug.Log (lg.token);

			StartCoroutine(GetUserData (lg.token));
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
}