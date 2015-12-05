using UnityEngine;
using System.Collections;

public class TunnelTile : MonoBehaviour {
	public float bounceFactor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		Debug.Log ("bounce");
		foreach (ContactPoint c in col.contacts) {
			GameObject go = c.otherCollider.gameObject;
			Bounce bouncer = go.GetComponent<Bounce>();
			if(bouncer != null) {
				bouncer.Jump(bounceFactor);
			}
		}
	}
}
