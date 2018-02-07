using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

	private GameObject head;
	private LinkedList<GameObject> body = new LinkedList<GameObject>();

	public const int DIR_UP = 0;
	public const int DIR_RIGHT = 1;
	public const int DIR_DOWN = 2;
	public const int DIR_LEFT = 3;

	private int inc = 6;

	private int dir = 0;
	private int nextDir = 0;

	void Start () {
		dir = DIR_UP;

		head = newHead ();
		head.transform.position = this.transform.position;
	}

	void OnDestroy() {
		Destroy (head);
		foreach (var node in body) {
			Destroy (node);
		}
	}

	void OnGUI() {

		bool up = Input.GetKeyDown (KeyCode.UpArrow);
		bool down = Input.GetKeyDown (KeyCode.DownArrow);
		bool left = Input.GetKeyDown (KeyCode.LeftArrow);
		bool right = Input.GetKeyDown (KeyCode.RightArrow);

		if (up && dir != DIR_DOWN) {
			nextDir = DIR_UP;
		} else if (down && dir != DIR_UP) {
			nextDir = DIR_DOWN;
		} else if (right && dir != DIR_LEFT) {
			nextDir = DIR_RIGHT;
		} else if (left && dir != DIR_RIGHT) {
			nextDir = DIR_LEFT;
		}
	}

	void FixedUpdate () {
		dir = nextDir;

		moveHead ();
		moveBody ();
	}

	void moveHead() {

		Vector3 delta = new Vector3 (0, 0, 0);

		switch(dir)
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

		head.transform.Translate (delta);
	}

	void moveBody() {
		var first = newNode ();
		first.transform.position = transform.position;
		body.AddFirst (first);
		transform.position = head.transform.position;

		if (inc > 0) {
			inc--;
			return;
		}
		var last = body.Last;
		body.RemoveLast ();
		Destroy (last.Value);
	}

	void OnTriggerEnterEvent(Collider2D e) {

		switch (e.tag) {
		case "snakeBodyNode":
			Destroy (this);
			break;
		}

		Debug.Log ("From snake" + e);
	}

	GameObject newHead() {

		var head = new GameObject ();
		head.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Circle");
		head.AddComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;

		var collider = head.AddComponent<CircleCollider2D> ();
		collider.isTrigger = true;
		collider.radius = 0.4f;

		var script = head.AddComponent<Head> ();
		script.OnTriggerEnterEvent += OnTriggerEnterEvent;

		return head;
	}

	GameObject newNode() {

		var node = new GameObject ();
		node.tag = "snakeBodyNode";

		var renderer = node.AddComponent<SpriteRenderer> ();
		renderer.sprite = Resources.Load<Sprite> ("Circle");
		renderer.color = Color.gray;

		var collider = node.AddComponent<CircleCollider2D> ();
		collider.isTrigger = true;
		collider.radius = 0.4f;
		return node;
	}
}