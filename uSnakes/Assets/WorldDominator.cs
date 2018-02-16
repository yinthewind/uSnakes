using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDominator : MonoBehaviour {

	public GameObject player;

	private int foodFlush = 5;
	private int foodIndex = 3;

	public int Width = 5;
	public int Height = 5;

	void generatePlayground(int width, int height) {
		var playground = new GameObject ();
		playground.name = "playground";

		var sRenderer = playground.AddComponent<SpriteRenderer> ();
		sRenderer.sprite = Resources.Load<Sprite> ("Square");
		sRenderer.material.color = Color.black;
		sRenderer.drawMode = SpriteDrawMode.Tiled;
		sRenderer.size = new Vector2 (2 * Width + 1f, 2 * Height + 1f);
		sRenderer.sortingOrder = -1;

		var lRenderer = playground.AddComponent<LineRenderer> ();
		lRenderer.material.color = Color.white;
		lRenderer.endColor = Color.white;
		lRenderer.startColor = Color.white;

		lRenderer.endWidth = 0.5f;
		lRenderer.startWidth = 0.5f;
		lRenderer.positionCount = 4;
		lRenderer.loop = true;

		var w = width + 0.5f;
		var h = height + 0.5f;
		var points = new Vector3[] { 
			new Vector3 (w, h),
			new Vector3 (w, -h),
			new Vector3 (-w, -h),
			new Vector3 (-w, h)
		};
		lRenderer.SetPositions (points);

		var points2 = new Vector3[] {
			new Vector3 (1, 1),
			new Vector3 (1, -1)
		};
		lRenderer.SetPositions (points2);
	}

	void tryGenerateFood() {
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

	void Start() {
		generatePlayground (Width, Height);
	}

	void FixedUpdate () {

		tryGenerateFood ();

		if (player == null) {
			player = new GameObject ();
			player.AddComponent<Snake> ();
		}
	}
}
