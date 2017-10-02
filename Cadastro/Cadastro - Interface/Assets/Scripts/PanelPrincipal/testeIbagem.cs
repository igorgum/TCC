using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testeIbagem : MonoBehaviour {

	private string url = "http://i0.kym-cdn.com/photos/images/facebook/001/090/170/192.png";

	public void Start () {
		StartCoroutine ("carregarImagem");
	}

	IEnumerator carregarImagem(){
		WWW wwwimg = new WWW (url);
		yield return wwwimg;

		gameObject.GetComponent<RawImage> ().texture = wwwimg.texture;
	}
}
