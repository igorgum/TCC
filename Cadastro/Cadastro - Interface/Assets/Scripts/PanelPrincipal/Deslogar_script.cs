using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deslogar_script : MonoBehaviour {
	
	public GameObject painelPrincipal;
	public GameObject painelLogin;
	public GameObject painelPergunta;
	public InputField login, senha;

	//lista pública de Dropdowns para zerar
	public Dropdown[] lista;

	public void Sair(){
		//zerando Dropdowns
		foreach (Dropdown i in lista)
		{
			i.value = 0;
		}

		//Ativando Painel de login p/ referencias os input fields
		painelLogin.SetActive (true);

		//Procurar zerar todas as variaveis e campos aqui
		login.text = string.Empty;
		senha.text = string.Empty;

		//Desligando painéis
		painelPergunta.SetActive (false);
		painelPrincipal.SetActive(false);
	}

}
