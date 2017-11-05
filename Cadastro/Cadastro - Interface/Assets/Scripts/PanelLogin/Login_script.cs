using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;

public class Login_script : MonoBehaviour {

	public GameObject painelPrincipal;
	public GameObject painelLogin;
	public Text statusLogin, conectadoComo;
	public InputField isLoginOk; //campo login, se tiver vazio dá erro
	public InputField isSenhaOk; //campo senha, se tiver vazio dá erro

	public GameObject controllerOPC;
	public GameObject Loading;

	void Start(){
		statusLogin.text = "";
	}

	public void Logar() {
		Loading.SetActive (true);
		if (isLoginOk.text != "") {
			if (isSenhaOk.text != "") {
				StartCoroutine ("RotinaLogar");
			} else {
				statusLogin.text = "Erro: O campo senha está vazio";
				//ou:
				//statusLogin.text = "Erro: a Senha foi digitada incorretamente";
				Loading.SetActive (false);
			}
		} else {
			statusLogin.text = "Erro: O campo login está vazio";
			//ou:
			//statusLogin.text = "Erro: o Login foi digitada incorretamente";
			Loading.SetActive (false);
		}

	}

	IEnumerator RotinaLogar(){
		Loading.SetActive (true);

		string login = isLoginOk.text;
		string senha = Md5Sum(isSenhaOk.text);
		//print ("minha senha eh: " + senha);

		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/login/Logar.php"
			+ "?login=" + login
			+ "&senha=" + senha);
		yield return txtConsulta;

		if (txtConsulta.text != "|") {
			conectadoComo.text = "Usuário conectado como\n" + txtConsulta.text;
			//limpando/ativando telas
			statusLogin.text = "";
			painelLogin.SetActive (false);
			painelPrincipal.SetActive (true);
		} else {
			statusLogin.text = "Erro: Falha na autenticação";
		}

		Loading.SetActive (false);
	}

	//Função de MD5 retirada do site http://wiki.unity3d.com/index.php?title=MD5
	public  string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}
}
