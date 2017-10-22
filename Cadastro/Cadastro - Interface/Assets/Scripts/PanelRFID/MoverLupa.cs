using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverLupa : MonoBehaviour {
	float velocidade = 1.5f;
	int cont=0;
	bool voltar;
	Vector3 posicaoOriginal;

	void Start(){
		posicaoOriginal = transform.position;
	}

	void OnDisable(){
		transform.position = posicaoOriginal;
		cont = 0;
		voltar = false;
	}

	void FixedUpdate () {

			if (!voltar) {
				Debug.Log (cont);
				transform.Translate (velocidade * Time.deltaTime, 0, 0);
				cont++;
				if (cont == 120) {voltar = true;}
			} else {
				Debug.Log (cont);
			transform.Translate ((velocidade*-1) * Time.deltaTime, 0, 0);
				cont--;
				if (cont == 0) {voltar = false;}
			}

	}

}
