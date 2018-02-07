using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	public delegate void OnEvent(Collider2D e);
	public OnEvent OnTriggerEnterEvent;

	void OnTriggerEnter2D(Collider2D e) {
		OnTriggerEnterEvent (e);
	}
}
