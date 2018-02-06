using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

	private LinkedList<GameObject> body = new LinkedList<GameObject>();

	public const int DIR_UP = 0;
	public const int DIR_RIGHT = 1;
	public const int DIR_DOWN = 2;
	public const int DIR_LEFT = 3;

	private int inc = 6;

	public int state = 0;

	void Start () {
		state = DIR_UP;

		var head = new GameObject ();
		head.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Circle");
		head.transform.position = this.transform.position;
		body.AddFirst (head);
	}

	void OnGUI() {

		bool up = Input.GetKeyDown (KeyCode.UpArrow);
		bool down = Input.GetKeyDown (KeyCode.DownArrow);
		bool left = Input.GetKeyDown (KeyCode.LeftArrow);
		bool right = Input.GetKeyDown (KeyCode.RightArrow);

		if (up && state != DIR_DOWN) {
			state = DIR_UP;
		} else if (down && state != DIR_UP) {
			state = DIR_DOWN;
		} else if (right && state != DIR_LEFT) {
			state = DIR_RIGHT;
		} else if (left && state != DIR_RIGHT) {
			state = DIR_LEFT;
		}
	}

	void FixedUpdate () {
		Debug.Log (inc);
		moveHead ();
		moveTail ();
	}

	void moveHead() {

		Vector3 delta = new Vector3 (0, 0, 0);

		switch(state)
		{
		case DIR_UP:
			delta.y = 1;
			break;
		case DIR_DOWN:
			delta.y = -1;
			break;
		case DIR_LEFT:
			delta.x = -1;
			break;
		case DIR_RIGHT:
			delta.x = 1;
			break;
		}

		var head = new GameObject ();
		head.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Circle");
		head.transform.position = transform.position + delta;
		body.AddFirst (head);
		transform.Translate (delta);
	}

	void moveTail() {
		if (inc > 0) {
			inc--;
			return;
		}
		var last = body.Last;
		body.RemoveLast ();
		Destroy (last.Value);
	}
}
