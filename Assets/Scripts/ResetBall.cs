using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		other.gameObject.transform.position = new Vector3 (0, 0.5f, 0);
		if (other.gameObject.GetComponent<Rigidbody> () != null)
			other.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
	}
}
