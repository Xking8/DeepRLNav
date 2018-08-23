using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour {

	public Rigidbody rb;
	//public Vector3 StartVec;

	// Use this for initialization
	void Start () {
		//StartVec = transform.position;
		//transform.position = new Vector3 (0, 0, 0);
		rb = GetComponent<Rigidbody> ();

		rb.velocity = new Vector3 (1, 10, 10);
		//InvokeRepeating("ChangeSpeed", 1.0f, 100.0f);
	}

	// Update is called once per frame
	void ChangeSpeed () {
		rb.velocity = new Vector3 (1, 0, 0);

		//rb.velocity = new Vector3 (0, 0, 0);
	}
}
