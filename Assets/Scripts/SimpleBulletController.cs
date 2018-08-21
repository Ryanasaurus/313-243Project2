using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBulletController : MonoBehaviour {

	public float speed = 5f;

	private Rigidbody2D rb2D;

	void Start () {
		rb2D = GetComponent<Rigidbody2D>();		
	}
	
	void Update () {
		rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			Destroy(gameObject);
		} else if (other.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
			Destroy(gameObject);
		}
	}
}
