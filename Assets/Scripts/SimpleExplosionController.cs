using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleExplosionController : MonoBehaviour {

	public float length = .5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		length -= Time.deltaTime;
		if(length<=0) { 
			Destroy(gameObject);
		}
	}
}
