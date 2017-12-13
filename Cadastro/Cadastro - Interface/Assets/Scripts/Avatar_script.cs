using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Avatar_script : MonoBehaviour {

	public Texture2D padrao;
	public InputField inputfieldLogin;
	public RawImage imagem, imagem2;
	public string url;

	public GameObject controllerOPC;
	public GameObject Loading;//painel LOADING

	// Use this for initialization
	public void atualizar () {
		//GameObject inputfieldLoginaso = GameObject.Find ("InputField_Login");
		//inputfieldLogin = inputfieldLoginaso.GetComponent<InputField> ();

		if (inputfieldLogin.text != string.Empty) {
			StartCoroutine ("PuxarLogin");
		} else {
			imagem.texture = padrao;
			imagem2.texture = padrao;
		}
	}
		
	IEnumerator PuxarLogin(){
		Loading.SetActive (true);
		string login = inputfieldLogin.text;

		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/login/consultaLogin.php"
			+ "?login=" + login);
		yield return txtConsulta;

		if (txtConsulta.text == null || txtConsulta.text == "" || txtConsulta.text.Contains ("<title>404 Not Found</title>") || txtConsulta.text.Contains ("class='xdebug-error xe-warning'")) {
			Debug.Log ("nao consegui alcancar o ip");
			url = null;
			imagem.texture = padrao;
			imagem2.texture = padrao;
			Loading.SetActive (false);
			yield break;
		}

		String[] listaDeSubstrings = txtConsulta.text.Split('|');

		Debug.Log ("conteudo de consulta="+txtConsulta.text);
		if (inputfieldLogin.text.Equals (listaDeSubstrings[1])) {
			url = listaDeSubstrings [0];
			imagem = this.gameObject.GetComponent<RawImage> ();
			StartCoroutine ("carregarImagem");
		} else {
			url = null;
			imagem.texture = padrao;
			imagem2.texture = padrao;
		}
		Loading.SetActive (false);
	}

	IEnumerator carregarImagem(){
		Loading.SetActive (true);
		WWW wwwimg = new WWW (controllerOPC.GetComponent<OPC_Controller> ().endereco
			+ "/tcc/uploads/" + url + ".png");
		yield return wwwimg;

		if (wwwimg.error == null) {
			imagem.texture = wwwimg.texture;
			imagem2.texture = imagem.texture;
		} else {
			imagem.texture = padrao;
			imagem2.texture = padrao;
		}

		Loading.SetActive (false);
	}
}
