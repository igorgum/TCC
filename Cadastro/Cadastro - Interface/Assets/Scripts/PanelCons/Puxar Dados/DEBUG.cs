using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DEBUG : MonoBehaviour {

	String[] substrings;

	// Use this for initialization
	void Start () {
		StartCoroutine ("ConsultaPorNome");
	}

	/*
	IEnumerator ConsultaPorNome(){
		WWW txtConsulta = new WWW ("localhost/testemultiplo.php");
		yield return txtConsulta;

		print ("Resultado da consulta: " + txtConsulta.text);
		//////////////////////////////////////////////////////////
		/////////////////////////////////////////arrumando o vetor
		String[] listaDeSubstrings = txtConsulta.text.Split('|');
		Array.Resize(ref listaDeSubstrings, listaDeSubstrings.Length - 1); //Tirando duplicata gerada pelo splitter
		////////////////////////////////contando numero de pessoas
		int cont = 0;
		foreach (var substring in listaDeSubstrings) {
			cont++;
		}
		if (cont == 0) {
			print ("sem retorno");
		} else {
			int qnts = cont / 5;
			print("retornei "+qnts+" pessoas");
		}
		//////////////////////////////////////Instanciando botões
		int i = 0;
		foreach (var substring in listaDeSubstrings) {
			if (i<qnts){
			GameObject instancia = (GameObject)Instantiate (prefab, Content);
			instancia.SetActive (true);
			instancia.name = "novobotao";
			instancia.GetComponentInChildren<Text> ().text = substrings [2+(5*i)]; //[valor+campos*i]
			instancia.GetComponent<btnFuncio_Script> ().codigoFuncionario = substrings [0+(5*i)];
			instancia.GetComponent<btnFuncio_Script> ().agoraPegaFoto = true;
			i++;

		}
	}*/
}
