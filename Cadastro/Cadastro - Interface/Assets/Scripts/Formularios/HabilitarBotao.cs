using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabilitarBotao : MonoBehaviour {

	public InputField[] listaInputfields;
	public Dropdown[] listaDropdowns;

	void FixedUpdate(){
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
}
