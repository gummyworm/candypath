using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelGen : MonoBehaviour {
	public int tilesPerStep;
	public int length = 25;
	public float stepSize;
	public float diameter;
	
	// put tiles used by your generator here
	public GameObject wallTile;
	public GameObject wallTile2;
	
	protected class Segment {
		private GameObject[] tiles;
		private GameObject[] items;

		private float diameter;
		private int numTiles;

		public Segment(float d, int numTiles) {
			this.diameter = d;
			this.numTiles = numTiles;

			this.tiles = new GameObject[numTiles];
			this.items = new GameObject[numTiles];
		}

		public void SetTile(int i, GameObject tile) {
			if (i < numTiles) {
				tiles [i] = tile;
			}
		}

		public GameObject GetTile(int i) {
			if (i < numTiles) {
				return tiles [i];
			}
			return null;
		}

		public void SetItem(int i, GameObject item) {
			if (i < numTiles) {
				items [i] = item;
			}
		}
		
		public GameObject GetItem(int i) {
			if (i < numTiles) {
				return items [i];
			}
			return null;
		}

		public int NumTiles() {
			return numTiles;
		}
	}

	protected Dictionary<int, GameObject> tileMap;
	protected List<Segment> tiles; // tiles is the array of tunnel segments
	protected float angleStep; // radians between each tile in a segment

	// Use this for initialization
	void Start () {
		tileMap = new Dictionary<int, GameObject> ();
		tiles = new List<Segment> ();
		angleStep = (Mathf.PI*2.0f)/(float)tilesPerStep;

		MakeTunnel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// MapTo maps the integer i to the given tile (for generation).
	public void MapTo(int i, GameObject tile) {
		tileMap.Add (i, tile);
	}

	// AddSegment adds a segment of tiles from the given array of tile ID's.
	public void AddSegment(int[] seg) {
		Segment segment = new Segment (diameter, tilesPerStep);
		for(int i = 0; i < seg.Length; ++i) {
			if(tileMap.ContainsKey(seg[i])) {
				segment.SetTile(i, (GameObject)Instantiate(tileMap[seg[i]]));
			} else {
				Debug.Log ("key " + seg[i] + " not found");
				segment.SetTile(i, null);
			}
		}
		tiles.Add (segment);
	}

	// Generate places each tile in tiles according to the diameter and step parameters
	public void Generate() {
		float d = 0.0f;	//distance

		foreach (Segment segment in tiles) {
			int i;
			float theta;
			for(i = 0, theta = 0.0f; i < segment.NumTiles(); ++i, theta += angleStep) {
				GameObject go = segment.GetTile(i);
				go.transform.position = new Vector3(Mathf.Cos(theta) * diameter, Mathf.Sin(theta) * diameter, d);
				go.transform.rotation = Quaternion.LookRotation(go.transform.position - new Vector3(0.0f, 0.0f, d));
			}
			d += stepSize;
		}
	}

	virtual public void MakeTunnel() {
		string s0 = "10101010";
		string s1 = "01010101";

		// map tiles
		MapTo ('0', wallTile);
		MapTo ('1', wallTile2);

		int[] seg0 = new int[s0.Length];
		int[] seg1 = new int[s1.Length];
		for(int i = 0; i < s0.Length; ++i) {
			seg0[i] = s0[i];
		}
		for(int i = 0; i < s1.Length; ++i) {
			seg1[i] = s1[i];
		}
		for(int i = 0; i < length; ++i) {
			AddSegment(seg0);
			AddSegment(seg1);
		}
		Generate ();
	}
}
