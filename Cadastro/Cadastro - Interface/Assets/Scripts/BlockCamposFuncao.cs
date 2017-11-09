using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockCamposFuncao : MonoBehaviour {

	public Dropdown funcao; //dropdown funcao do formulário de funcionário
	public GameObject salvarModificacao; //se ele tiver presente

	void FixedUpdate(){
		if (funcao.value == 1) {
			//verifica se jogou o script em um inputfield ou em um button
			if (gameObject.GetComponent<Button> () == null) {
				if (salvarModificacao == null || (salvarModificacao != null && salvarModificacao.activeInHierarchy)) {
					gameObject.GetComponent<InputField> ().interactable = true;
				}
			}
		} else { //else do funcao.value == 1
			//verifica se jogou o script em um inputfield ou em um button
			if (gameObject.GetComponent<Button> () == null) {
				gameObject.GetComponent<InputField> ().interactable = false;
				gameObject.GetComponent<InputField> ().text = null;
			} else { //else do button == null
				gameObject.GetComponent<Button>().interactable = false;
			}
		}
	}
}
