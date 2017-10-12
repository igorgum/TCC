using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login_script : MonoBehaviour {

	public GameObject painelPrincipal;
	public GameObject painelLogin;
	public Text statusLogin, conectadoComo;
	public InputField isLoginOk; //campo login, se tiver vazio dá erro
	public InputField isSenhaOk; //campo senha, se tiver vazio dá erro

	void Start(){
		statusLogin.text = "";
	}

	public void Logar() {
		if (isLoginOk.text != "") {
			if (isSenhaOk.text != "") {

				//usuario conectado como x ("isLoginOk" é p/ debug, dps vc pega o NOME no banco de dados)
				conectadoComo.text = "Usuário conectado como " + isLoginOk.text;

				//limpando/ativando telas
				statusLogin.text = "";
				painelLogin.SetActive (false);
				painelPrincipal.SetActive(true);

			} else {
				statusLogin.text = "Erro: O campo senha está vazio";
				//ou:
				//statusLogin.text = "Erro: a Senha foi digitada incorretamente";
			}
		} else {
			statusLogin.text = "Erro: O campo login está vazio";
			//ou:
			//statusLogin.text = "Erro: o Login foi digitada incorretamente";
		}

	}
		
}
