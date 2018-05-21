using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointControl : MonoBehaviour {
	public GameObject EnemyPre;
	private float timer = 0;
	private PlayerControl playerControl;

	// Use this for initialization
	void Start () {
		playerControl = GameObject.FindWithTag ("Player").GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerControl.Hp <= 0) {
			return;
		}

		timer += Time.deltaTime;
		if (timer > 2f) {
			timer = 0;
			// 创建敌人
			Instantiate(EnemyPre, transform.position, transform.rotation);
		}
	}
}
