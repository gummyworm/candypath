using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelGen : MonoBehaviour {
	public int tilesPerStep;
	public float stepSize;
	public float diameter;

	public GameObject[] tunnel;

	protected Dictionary<int, TunnelTile> tileMap;
	protected List<TunnelTile[]> tiles; // tiles is the array of tunnel segments
	protected float angleStep; // radians between each tile in a segment

	// Use this for initialization
	void Start () {
		tileMap = new Dictionary<int, TunnelTile> ();
		tiles = new List<TunnelTile[]> ();
		angleStep = (Mathf.PI*2.0f)/tilesPerStep;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// MapTo maps the integer i to the given tile (for generation).
	public void MapTo(int i, TunnelTile tile) {
		tileMap.Add (i, tile);
	}

	// AddSegment adds a segment of tiles from the given array of tile ID's.
	public void AddSegment(int[] tiles) {
		TunnelTile segment = new TunnelTile[tilesPerStep];
		for(int i = 0; i < tiles.Length; ++i) {
			if(tileMap.ContainsKey(tiles[i])) {
				segment[i] = tileMap[tiles[i]];
			}
			else {
				segment[i] = null;
			}
		}
	}

	// Generates a tunnel from all the segments that have been added.
	public void Generate() {
		Vector3 spawnAt = Vector3.zero;

		foreach (TunnelTile[] segment in tiles) {
			int i;
			float theta;
			for(int i = 0, theta = 0.0f; i < segment.Length; ++i, theta += angleStep) {
				spawnAt.x += Mathf.Cos(theta) * diameter;
				spawnAt.y += Mathf.Sin(theta) * diameter;
				GameObject.Instantiate(segment[i], spawnAt, Quaternion.identity);
			}
			spawnAt.z += stepSize;
		}
	}
}
