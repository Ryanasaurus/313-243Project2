using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour {

	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool facingRight = true;

	public float moveForce = 365f;
	public float maxSpeed = 2f;
	public float jumpForce = 1000f;
	public float agroRange = 10f;
	public Transform groundCheck;
	public Transform frontCheck;
	public Transform playerTransform;

	private bool grounded = false;
	private bool frontCollide = false;
	// private Animator anim;
	private Rigidbody2D rb2D;
	// Movement Fields
	private float xMovement = 0;

	void Start () {		
		// anim = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		frontCollide = Physics2D.Linecast(transform.position, frontCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		// TODO: Implement AI
		if(playerTransform.position.x<transform.position.x && Mathf.Abs(playerTransform.position.x-transform.position.x) < agroRange) {
			xMovement = -1f;
		} else if (playerTransform.position.x>transform.position.x && Mathf.Abs(playerTransform.position.x-transform.position.x) < agroRange) {
			xMovement = 1f;
		}
		if(playerTransform.position.y>transform.position.y - 5 && grounded && Mathf.Abs(playerTransform.position.x-transform.position.x) < agroRange) {
			jump = true;
		}
	}

	void FixedUpdate() {
		// anim.SetFloat("Speed", Mathf.Abs(xMovement));

		if(!frontCollide) {
			if(xMovement*rb2D.velocity.x < maxSpeed) {
				rb2D.AddForce(Vector2.right * xMovement * moveForce);
			}
			if(Mathf.Abs(rb2D.velocity.x) > maxSpeed) {
				rb2D.velocity = new Vector2(Mathf.Sign(rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);
			}
		}

		if((xMovement>0 && !facingRight) || (xMovement<0 && facingRight)) {
			Flip();
		}

		if(jump) {
			// anim.SetTrigger("Jump");
			rb2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Bullet") || other.gameObject.layer == LayerMask.NameToLayer("Explosion")) {
			// gameObject.SetActive(false);
			Destroy(gameObject);
		} 
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
