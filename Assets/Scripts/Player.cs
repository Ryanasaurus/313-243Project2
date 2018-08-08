using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingCharacter {

	static protected Player staticInstance;
	static public Player PlayerInstance { get { return staticInstance; } }

	private Rigidbody2D rb2D;

	public SpriteRenderer spriteRenderer;

	public float maxSpeed = 10f;
	public float groundAcceleration = 100f;
	public float groundDeceleration = 100f;
	[Range(0f, 1f)] public float pushingSpeedProportion;
	[Range(0f, 1f)] public float airborneAccelProportion;
	[Range(0f, 1f)] public float airborneDecelProportion;
	public float gravity = 50f;
	public float jumpSpeed = 20f;
	public float jumpAbortSpeedReduction = 100f;

	protected Vector2 moveVector;
	protected float currentVerticalSpeed = 0.0f;
	protected float currentHorizontalSpeed = 0.0f;


	// Use this for initialization
	void Awake () {
		staticInstance = this;
		rb2D = GetComponent<Rigidbody2D>();
	}

	void Start() {
		print("hello world");
	}

	void FixedUpdate() {
		this.Move();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalMovement = Input.GetAxisRaw("Horizontal");
		print(horizontalMovement);
		SetHorizontalMovement(horizontalMovement);
		Move();
	}

	private void Move() {
		rb2D.MovePosition(rb2D.position + moveVector);
	}

	public void SetMoveVector(Vector2 newMoveVector) {
		moveVector = newMoveVector;
	}

	public void SetHorizontalMovement(float newHorizontalMovement) {
		moveVector.x = newHorizontalMovement;
	}

	public void SetVerticalMovement(float newVerticalMovement) {
		moveVector.y = newVerticalMovement;
	}

	public void IncrementMovement(Vector2 additionalMovement) {
        moveVector += additionalMovement;
    }

    public void IncrementHorizontalMovement(float additionalHorizontalMovement) {
        moveVector.x += additionalHorizontalMovement;
    }

    public void IncrementVerticalMovement(float additionalVerticalMovement) {
        moveVector.y += additionalVerticalMovement;
    }

    public Vector2 GetMoveVector() {
    	return moveVector;
    }
}
