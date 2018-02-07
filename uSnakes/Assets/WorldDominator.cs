using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDominator : MonoBehaviour {

	public GameObject player;

	private int foodFlush = 5;
	private int foodIndex = 3;

	public int Width = 10;
	public int Height = 10;

	void tryGenerateFood() {
		Debug.Log (foodIndex);
		if (foodFlush > 0) {
			foodFlush--;
		} else if (foodIndex > 0) {
			foodIndex--;
			return;
		} else {
			foodIndex = (int)(Random.value * 25);
		}

		var x = (Random.value - 0.5f) * Width * 2;
		var y = (Random.value - 0.5f) * Height * 2;
		newFood (x, y);
	}

	GameObject newFood(float x, float y) {
		var food = new GameObject ();
		food.name = "snakeFood";
		food.tag = "snakeFood";

		var renderer = food.AddComponent<SpriteRenderer> ();
		renderer.sprite = Resources.Load<Sprite> ("Circle");
		renderer.material.color = Color.yellow;

		food.AddComponent<CircleCollider2D> ();

		food.transform.position = new Vector3 (x, y);
		return food;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		tryGenerateFood ();

		if (player == null) {
			player = new GameObject ();
			player.AddComponent<Snake> ();
		}
	}
}
