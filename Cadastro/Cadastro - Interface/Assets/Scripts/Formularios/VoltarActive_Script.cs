using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoltarActive_Script : MonoBehaviour {

	public GameObject ModificarRegistro;
	public bool inversao;
	public bool inversaoDeslogar;

	void FixedUpdate(){
		if (!inversao) {
			if (ModificarRegistro.activeInHierarchy) {
				gameObject.GetComponent<Button> ().interactable = true;
			} else {
				gameObject.GetComponent<Button> ().interactable = false;
			}
		} else {
			if (inversaoDeslogar) {
				if (ModificarRegistro.activeSelf) {
					gameObject.GetComponent<Button> ().interactable = false;
				} else {
					gameObject.GetComponent<Button> ().interactable = true;
				}
			} else {
				if (ModificarRegistro.activeSelf) {
					gameObject.SetActive (false);
				} else {
					gameObject.SetActive (true);
				}
			}
		}
	}
}
