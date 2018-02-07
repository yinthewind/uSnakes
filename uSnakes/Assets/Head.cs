using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	public delegate void OnEvent(Collider2D e);
	public OnEvent OnTriggerEnterEvent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D e) {
		OnTriggerEnterEvent (e);
	}
}
