using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	// 观察的物体
	public Transform target;

	private Vector3 dir;

	// Use this for initialization
	void Start () {
		dir = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = dir + target.position;
	}
}
