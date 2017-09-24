using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController_script : MonoBehaviour {

	//Se um OU dois dropdowns estiverem com um valor X, ativa um conteudo

	public GameObject obj1, obj2;
	public int valorObj1, valorObj2;
	public GameObject conteudo;
	private bool qtd = false; //false é 1 obj, true é 2 obj

	void Start(){

		//vendo quantos objetos estão referenciados
		if (obj2 != null) {
			qtd = true;
		}
	
	}


	void Update () {

		//activate "conteudo" se "obj1" e "obj2" forem um valor "valorObj1" e "valorObj2"
		if (!qtd){
			//SE FOR APENAS 1 OBJETO FAÇA
			if (obj1.activeInHierarchy)
			{
				if (obj1.GetComponent<Dropdown>().value == valorObj1) {
					conteudo.SetActive (true);
				} else {
					conteudo.SetActive (false);
				}
			}
		}else{
			//SE FOREM 2 OBJETOS FAÇA
			if (obj1.activeInHierarchy && obj2.activeInHierarchy)
			{
				if (obj1.GetComponent<Dropdown>().value == valorObj1 && obj2.GetComponent<Dropdown>().value == valorObj2) {
					conteudo.SetActive (true);
				} else {
					conteudo.SetActive (false);
				}
			}
		}
			
	}
}
