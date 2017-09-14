using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar_script : MonoBehaviour {

	public Texture2D padrao;
	public InputField inputfieldLogin;
	public RawImage imagem, imagem2;
	private string url = "https://avatars1.githubusercontent.com/u/31558711";

	// Use this for initialization
	public void atualizar () {
		//GameObject inputfieldLoginaso = GameObject.Find ("InputField_Login");
		//inputfieldLogin = inputfieldLoginaso.GetComponent<InputField> ();

		if (inputfieldLogin.text != string.Empty) {
				//agora irei verificar se login está como "igor", somente para debugar
				//depois mude para procurar no server o nome do usuario
			if (inputfieldLogin.text.Equals ("igor")) {
				imagem = this.gameObject.GetComponent<RawImage> ();
				StartCoroutine ("carregarImagem");
			} else {
				imagem.texture = padrao;
				imagem2.texture = padrao;
			}
		} else {
			imagem.texture = padrao;
			imagem2.texture = padrao;
		}
	}
		
	IEnumerator carregarImagem(){
		WWW wwwimg = new WWW (url);
		yield return wwwimg;

		imagem.texture = wwwimg.texture;
		imagem2.texture = imagem.texture;
	}
}
