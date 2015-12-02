using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelGen : MonoBehaviour {
	public int tilesPerStep;


	protected Dictionary<int, TunnelTile> tileMap;
	protected List<TunnelTile[]> tiles; // tiles is the array of tunnel segments

	// Use this for initialization
	void Start () {
		tileMap = new Dictionary<int, TunnelTile> ();
		tiles = new List<TunnelTile[]> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// MapTo maps the integer i to the given tile (for generation).
	public void MapTo(int i, TunnelTile tile) {
		tileMap.Add (i, tile);
	}

	// AddSegment adds a segment of tiles from the given array of tile ID's.
	public void AddSegment(int []tiles) {
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
}
