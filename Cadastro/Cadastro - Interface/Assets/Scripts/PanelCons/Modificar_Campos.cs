using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modificar_Campos : MonoBehaviour {
	//Procurar onde
	public GameObject[] objetoOnde; //Começa em Zero
	private int indice=0; //Indice de "objetoOnde" para mexer
	public GameObject nomeMenu; //nome do menu pra setar o indice

	//Campos (privados)
	private InputField[] listaInputfields;
	private Dropdown[] listaDropdowns;

	//O "Main" do script, seta os campos.interactables
	//O parametro que ele recebe indica se irá ativar ou desativar o atributo "interactable" objetos
	public void Executar (bool isInterativo) {

		setIndice ();
		Procurar();

		if (isInterativo == true) {
			foreach (Dropdown i in listaDropdowns) {
				i.interactable = true;
			}
			foreach (InputField i in listaInputfields) {
				i.interactable = true;
			}
		} else {
			foreach (Dropdown i in listaDropdowns) {
				i.interactable = false;
			}
			foreach (InputField i in listaInputfields) {
				i.interactable = false;
			}
		}
	}

	void Procurar(){
		/////////////////////////////PROCURAR DROPDOWN
		//ve o tamanho de dropdown
		int tamanhoDropdown=0;
		if (objetoOnde[indice].activeInHierarchy) {
			foreach (Dropdown i in objetoOnde[indice].GetComponentsInChildren<Dropdown>()) {
				tamanhoDropdown++;
			}
		}
		//Inicializa uma lista de dropdowns para um tamanho X
		listaDropdowns = new Dropdown[tamanhoDropdown];
		//agora adiciona a Dropdowns os filhos de Onde
		if (objetoOnde[indice].activeInHierarchy) {
			foreach (Dropdown i in objetoOnde[indice].GetComponentsInChildren<Dropdown>()) {
				tamanhoDropdown--;
				listaDropdowns[tamanhoDropdown]=i;
			}
		}
		/////////////////////////////PROCURAR INPUTFIELD
		//ve o tamanho de inputfield
		int tamanhoInputfield=0;
		if (objetoOnde[indice].activeInHierarchy) {
			foreach (InputField i in objetoOnde[indice].GetComponentsInChildren<InputField>()) {
				tamanhoInputfield++;
			}
		}
		//Inicializa uma lista de dropdowns para um tamanho X
		listaInputfields = new InputField[tamanhoInputfield];
		//agora adiciona a Dropdowns os filhos de Onde
		if (objetoOnde[indice].activeInHierarchy) {
			foreach (InputField i in objetoOnde[indice].GetComponentsInChildren<InputField>()) {
				tamanhoInputfield--;
				listaInputfields[tamanhoInputfield]=i;
			}
		}
	}






	//Lembrando que um botão da lista de consulta (btnProfessor por exemplo) irá setar o nome do menu de consulta
	//setIndice olha o nome do menu de consulta e associa à um indice da lista ObjetoOnde
	void setIndice(){
		switch (nomeMenu.GetComponent<Text>().text) {
		case "Funcionário":
			indice = 0;
			break;
		default:
			Debug.Log ("Indice caiu no default, revise o código");
			indice = 0;
			break;
		}
	}

}
