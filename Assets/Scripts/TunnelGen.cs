using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelGen : MonoBehaviour {
	public int tilesPerStep;
	public float stepSize;
	public float diameter;
	
	protected Dictionary<int, GameObject> tileMap;
	protected List<GameObject[]> tiles; // tiles is the array of tunnel segments
	protected float angleStep; // radians between each tile in a segment


	// put tiles used by your generator here
	public GameObject wallTile;
	public GameObject wallTile2;


	// Use this for initialization
	void Start () {
		tileMap = new Dictionary<int, GameObject> ();
		tiles = new List<GameObject[]> ();
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
		GameObject[] segment = new GameObject[tilesPerStep];
		for(int i = 0; i < seg.Length; ++i) {
			if(tileMap.ContainsKey(seg[i])) {
				segment[i] = (GameObject)Instantiate(tileMap[seg[i]]);
			} else {
				Debug.Log ("key " + seg[i] + " not found");
				segment[i] = null;
			}
		}
		tiles.Add (segment);
	}

	// Generate places each tile in tiles according to the diameter and step parameters
	public void Generate() {
		float d = 0.0f;	//distance

		foreach (GameObject[] segment in tiles) {
			int i;
			float theta;
			for(i = 0, theta = 0.0f; i < segment.Length; ++i, theta += angleStep) {
				segment[i].transform.position = new Vector3(
					Mathf.Cos(theta) * diameter,
					Mathf.Sin(theta) * diameter,
					d);
				segment[i].transform.rotation = Quaternion.LookRotation(segment[i].transform.position - new Vector3(0.0f, 0.0f, d));
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
		for(int i = 0; i < 10; ++i) {
			AddSegment(seg0);
			AddSegment(seg1);
		}
		Generate ();
	}
}
