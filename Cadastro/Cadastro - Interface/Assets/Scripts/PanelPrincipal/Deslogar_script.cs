using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deslogar_script : MonoBehaviour {
	
	public GameObject painelPrincipal;
	public GameObject painelLogin;
	public GameObject painelPergunta;

	public void Sair(){
		//Procurar zerar todas as variaveis e campos aqui

		//Desligando booleano de loginCerto (Debug)

		//Desligando painéis
		painelLogin.SetActive (true);
		painelPrincipal.SetActive(false);
		painelPergunta.SetActive (false);
	}

}
