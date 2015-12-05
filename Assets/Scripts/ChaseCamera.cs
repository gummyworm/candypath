using UnityEngine;
using System.Collections;

public class ChaseCamera : MonoBehaviour {
	public GameObject target;
	public float followDist;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x,
		                                 transform.position.y,
		                                 target.transform.position.z - followDist);
	}
}
