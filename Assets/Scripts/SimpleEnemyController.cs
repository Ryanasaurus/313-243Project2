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
	public Transform playerTransform;

	private bool grounded = false;
	private bool frontCollide = false;
	private Animator anim;
	private Rigidbody2D rb2D;
	// Movement Fields
	private float xMovement = 0;

	// so for future, implement acceleration/deceleration
	// have a velocity vector and apply it to the movement in update method
	// in FixedUpdate, alter the velocity vector by a set amount 
	// aim is to make enemy movement smoother, and less janky

	void Start () {		
		anim = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		frontCollide = Physics2D.Linecast(transform.position, frontCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		// TODO: Implement AI
		if(playerTransform.position.x<transform.position.x) {
			xMovement = -1f;
		} else {
			xMovement = 1f;
		}
		if(playerTransform.position.y>transform.position.y && grounded) {
			jump = true;
		}
	}

	void FixedUpdate() {
		anim.SetFloat("Speed", Mathf.Abs(xMovement));

		if(xMovement*rb2D.velocity.x < maxSpeed) {
			rb2D.AddForce(Vector2.right * xMovement * moveForce);
		}
		if(Mathf.Abs(rb2D.velocity.x) > maxSpeed) {
			rb2D.velocity = new Vector2(Mathf.Sign(rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);
		}

		// if((h>0 && !facingRight) || (h<0 && facingRight)) {
		// 	Flip();
		// }

		if(jump) {
			anim.SetTrigger("Jump");
			rb2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}
}
