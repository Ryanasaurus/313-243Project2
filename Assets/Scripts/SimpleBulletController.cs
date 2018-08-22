using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBulletController : MonoBehaviour {

	void Start () {
	}
	
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			Destroy(gameObject);
		} else if (other.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
			Destroy(gameObject);
		}
	}
}
