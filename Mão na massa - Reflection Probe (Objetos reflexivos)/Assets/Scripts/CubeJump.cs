using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeJump : MonoBehaviour {

	private Rigidbody rb;
	public float speed = 5;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		float jump = Input.GetAxis ("Jump");

		Vector3 movement = new Vector3 (0, jump, 0);

		rb.AddForce (movement * speed);
	}
}
