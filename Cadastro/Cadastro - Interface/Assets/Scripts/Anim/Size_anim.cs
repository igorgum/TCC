using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size_anim : MonoBehaviour {
	private Vector3 tamanhoOriginal;
	private int cont=0;

	void Start(){
		tamanhoOriginal = transform.localScale;
	}

	void OnDisable(){
		transform.localScale = tamanhoOriginal;
		cont = 0;
	}

	void FixedUpdate () {
		if (cont < 10) {
			transform.localScale = new Vector3 (transform.localScale.x + 0.01f, transform.localScale.y + 0.01f, transform.localScale.z + 0.01f);
			cont++;
		} else if (cont < 20) {
			transform.localScale = new Vector3 (transform.localScale.x - 0.01f, transform.localScale.y - 0.01f, transform.localScale.z - 0.01f);
			cont++;
		} else {
			OnDisable ();
		}
	}
}
