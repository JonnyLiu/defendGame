using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5f);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyControl> ().Die ();
		}
	}
}