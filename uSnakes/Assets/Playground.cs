using UnityEngine;
using System;
using System.Collections.Generic;

public class Playground
{
	public int Width = 5, Height = 5;
	public List<PlaygroundGrid> grids = new List<PlaygroundGrid> ();

	public void generatePlayground(int width, int height) {
		
		var playground = new GameObject ();
		playground.name = "playground";

		for (int x = -width; x <= width; x++) {
			for (int y = -height; y <= height; y++) {
				var grid = new PlaygroundGrid (x, y);
				grids.Add (grid);
			}
		}
	}
}

public class PlaygroundGrid : MonoBehaviour {

	public PlaygroundGrid(int x, int y) {
		var go = new GameObject ();
		go.name = "playgroundGrid";

		go.transform.position = new Vector3 (x, y);
		go.transform.localScale = new Vector3(0.9f, 0.9f, 1f);

		go.AddComponent<PlaygroundGrid> ();
		var sRenderer = go.AddComponent<SpriteRenderer> ();
		sRenderer.sprite = Resources.Load<Sprite> ("Square");
		sRenderer.material.color = Color.blue;
		sRenderer.sortingOrder = -2;
	}

	void Start() {
	}

	void OnTrigger() {
	}
}