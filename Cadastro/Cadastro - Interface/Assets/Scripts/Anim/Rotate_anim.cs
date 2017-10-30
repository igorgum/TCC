using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_anim : MonoBehaviour {
	void FixedUpdate () {
			transform.Rotate (transform.rotation.x + 4, transform.rotation.y + 4, transform.rotation.z + 4);
	}
}
