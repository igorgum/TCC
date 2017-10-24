using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FuncoesRegistradas : MonoBehaviour {

	public GameObject controllerOPC;

	//esse script deve pesquisar no banco quais as funções registradas
	//e então devolver ao dropdown Funções que o chamar com um OnClick
	//(é OnClick, não OnValueChange, se liga!)

	void OnEnable(){
		RetornarFuncoes ();
	}

	public void RetornarFuncoes(){
		StartCoroutine ("RetornarFuncao");
	}
	IEnumerator RetornarFuncao(){
		WWW txtConsulta = new WWW (controllerOPC.GetComponent<OPC_Controller>().endereco
			+ "/tcc/consultas/funcoes/retornarTodas.php");
		yield return txtConsulta;

		//Debug.Log ("retornei isso em funcao:"+txtConsulta.text); ///////////////////////DEBUG
		String[] substrings = txtConsulta.text.Split(';');
		Array.Resize(ref substrings, substrings.Length - 1); //Tirando duplicata gerada pelo splitter

		gameObject.GetComponent<Dropdown> ().ClearOptions();
		List<string> novaLista = new List<string>();
		novaLista.Add("Selecione uma funcao...");
		foreach (var substring in substrings){
			//print(substring); //DEBUG
			novaLista.Add(substring);
		}
		//gameObject.GetComponent<Dropdown> ().options = novaLista;
		gameObject.GetComponent<Dropdown> ().AddOptions(novaLista);

	}
}
