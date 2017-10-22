using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indice_Holder : MonoBehaviour {

	//Script usado para segurar indice dentro do painel de Escolher Opção

	private int indice=0;

	
	public int getIndice(){
		return indice;
	}

	public void setIndice(int indice){
		this.indice = indice;
	}

	//Zera o indice após desabilitar janela
	void onDisable(){
		indice = 0;
	}
}
