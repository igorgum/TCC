using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForcarVolta_Script : MonoBehaviour {

	public Button btnVoltar;

	public void ForcarVolta(){
		btnVoltar.onClick.Invoke ();
	}
}
