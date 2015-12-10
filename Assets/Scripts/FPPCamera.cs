using UnityEngine;
using System.Collections;

public class FPPCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 1, 1);
	}
}
