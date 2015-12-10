using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	public int scoreVal = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter (Collision col) {
		foreach (ContactPoint c in col.contacts) {
			Bounce b = c.otherCollider.gameObject.GetComponent<Bounce>();
			Scorer s = c.otherCollider.gameObject.GetComponent<Scorer>();
			if (s != null) {
				s.Score(scoreVal);
			}
			if (b != null) {
				OnPick(b);
			}
		}
	}

	public virtual void OnPick (Bounce player) {

	}
}
