using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour {

	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool facingRight = true;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public Transform frontCheck;

	private bool grounded = false;
	private bool frontCollide = false;
	private Animator anim;
	private Rigidbody2D rb2D;
	// Movement Fields

	void Start () {		
		anim = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		frontCollide = Physics2D.Linecast(transform.position, frontCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		// TODO: Implement AI
		// Deciding where to move
	}

	void FixedUpdate() {
		// Apply Movement
	}
}
