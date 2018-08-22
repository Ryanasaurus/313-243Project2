using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRocketController : MonoBehaviour {

	private bool exploded;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {	
	}

	void FixedUpdate() {
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log(col.gameObject.layer);
		if(col.gameObject.layer == LayerMask.NameToLayer("Ground") || col.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}


