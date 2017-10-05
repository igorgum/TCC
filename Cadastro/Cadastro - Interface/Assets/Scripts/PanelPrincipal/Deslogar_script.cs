using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deslogar_script : MonoBehaviour {
	
	public GameObject painelPrincipal;
	public GameObject painelLogin;
	public GameObject painelPergunta;

	//lista pública de Inputfields para zerar,
	//incluindo LOGIN e SENHA
	public InputField[] listaInputfields;


	//lista pública de Dropdowns para zerar
	public Dropdown[] listaDropdowns;


	public void Sair(){


		//zerando Dropdowns
		foreach (Dropdown i in listaDropdowns)
		{
				i.value = 0;
		}

		//Ativando Painel de login p/ referencias os input fields LOGIN e SENHA
		painelLogin.SetActive (true);

		//Procurar zerar todas as variaveis e campos aqui
		//incluindo LOGIN e SENHA
		foreach (InputField i in listaInputfields)
		{
			i.text = string.Empty;
		}

		//Desligando painéis
		painelPergunta.SetActive (false);
		painelPrincipal.SetActive(false);
	}

}
