using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = true;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public Transform rightCheck;

	private bool grounded = false;
	private bool collideRight = false;
	private Animator anim;
	private Rigidbody2D rb2D;


	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		collideRight = Physics2D.Linecast(transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if(Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(h));

		if(!collideRight){
			if(h*rb2D.velocity.x < maxSpeed) {
				rb2D.AddForce(Vector2.right * h * moveForce);
			}
			if(Mathf.Abs(rb2D.velocity.x) > maxSpeed) {
				rb2D.velocity = new Vector2(Mathf.Sign(rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);
			}
		}

		if((h>0 && !facingRight) || (h<0 && facingRight)) {
			Flip();
		}

		if(jump) {
			anim.SetTrigger("Jump");
			rb2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
