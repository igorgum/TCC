using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dropdowns_script : MonoBehaviour {

	//SE DROPDOWN OPCAO FOR 0, DESABILITA ALVO

	public Dropdown opcao, alvo;

	public void AtualizaDropdown () {
		if (opcao.value == 0) {
			alvo.interactable = false;
		} else {
			alvo.interactable = true;
		}
	}
		
}
