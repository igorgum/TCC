using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabilitarBotao : MonoBehaviour {

	public InputField[] listaInputfields;
	public Dropdown[] listaDropdowns;

	public bool versaoFuncionario; // nessa versão, é verificada se funcionario precisa de login e email
	public Dropdown funcao; //usado em Versão Funcionario
	public int cont1Adicional; //usado em Versão Funcionario (qnts campos a mais)

	void FixedUpdate(){
		#region Versão Padrão
		if(!versaoFuncionario){
			int lenght1 = listaInputfields.Length;
			int lenght2 = listaDropdowns.Length;
			int cont1 = 0, cont2 = 0;

			for (int i = 0; i < listaInputfields.Length; i++) {
				if (listaInputfields [i].text != "") {
					cont1++;
				}
			}
			for (int i = 0; i < listaDropdowns.Length; i++) {
				if (listaDropdowns [i].value != 0) {
					cont2++;
				}
			}

			if (cont1 == lenght1 && cont2 == lenght2) {
				gameObject.GetComponent <Button> ().interactable = true;
			} else {
				gameObject.GetComponent <Button> ().interactable = false;
			}
		}
		#endregion

		#region Versão Funcionário
		if(versaoFuncionario){
			int lenght1 = listaInputfields.Length;
			int lenght2 = listaDropdowns.Length;
			int cont1 = 0, cont2 = 0;

			for (int i = 0; i < listaInputfields.Length; i++) {
				if (listaInputfields [i].text != "") {
					cont1++;
				}
			}
			for (int i = 0; i < listaDropdowns.Length; i++) {
				if (listaDropdowns [i].value != 0) {
					cont2++;
				}
			}

			if(funcao.value != 1){ 
				cont1 += cont1Adicional;
			}

			if (cont1 == lenght1 && cont2 == lenght2) {
				gameObject.GetComponent <Button> ().interactable = true;
			} else {
				gameObject.GetComponent <Button> ().interactable = false;
			}
		}
		#endregion
	}
}
