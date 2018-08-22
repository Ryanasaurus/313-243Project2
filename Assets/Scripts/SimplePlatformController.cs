using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	// [HideInInspector] public static Transform location;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public Transform frontCheck;
	public float bulletSpeed = 30f;
	public float rocketSpeed = 40f;
	public float rocketCooldown = 5f;

	private bool grounded = false;
	private bool frontCollide = false;
	private Animator anim;
	private Rigidbody2D rb2D;
	private bool shoot = false;
	private bool shootRocket = false;
	private bool pickUpRocket = false;
	private float rocketCooldownCounter = 0f;

	public Transform firePoint;
	public GameObject simpleBullet;
	public GameObject simpleRocket;


	void Awake () {
		anim = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
		// location = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		frontCollide = Physics2D.Linecast(transform.position, frontCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if(Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}

		if(Input.GetButtonDown("Fire1")) {
			shoot = true;
		}

		if(Input.GetButtonDown("Fire2") && rocketCooldownCounter<=0f && pickUpRocket) { 
			shootRocket = true;
		}

		if(rocketCooldownCounter>0){
			rocketCooldownCounter-=Time.deltaTime;
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(h));

		if(!frontCollide){
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

		if(shoot) {
			shoot = false;
			GameObject bullet = (GameObject)Instantiate(simpleBullet, firePoint.position, firePoint.rotation);
			if(facingRight) {
				bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
			} else {
				bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
			}
		}

		if(shootRocket) {
			rocketCooldownCounter = rocketCooldown;
			shootRocket = false;
			GameObject bullet = (GameObject)Instantiate(simpleRocket, firePoint.position, firePoint.rotation);
			if(facingRight) {
				bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rocketSpeed, 0);
			} else {
				bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-rocketSpeed, 0);
			}
		}
	}

    void OnTriggerEnter2D(Collider2D other) {
    	if (other.gameObject.CompareTag("Pickup")) {
    		other.gameObject.SetActive (false);
			pickUpRocket = true;
		}
    }

	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
