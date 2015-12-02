using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
	public float amplitude;
	public float forwardVel;
	public float bounceAngle;

	public AnimationCurve bounceCurve;

	protected Vector2 bounceNormal;
	protected Vector2 bounceFloor;
	protected float height;

	// Use this for initialization
	void Start () {
		bounceFloor = new Vector2 (transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos;

		// update height
		height = bounceCurve.Evaluate (Time.time);

		// update position
		bounceNormal = new Vector2 (Mathf.Cos (bounceAngle), Mathf.Sin (bounceAngle));
		pos.x = transform.position.x + bounceFloor.x + (bounceNormal.x * height);
		pos.y = transform.position.y + bounceFloor.y + (bounceNormal.y * height);
		pos.z = transform.position.z + (forwardVel * Time.deltaTime);
		transform.position = pos;
	}
	
	public void Bounce(float bounceFac) {
		height = amplitude * bounceFac;
	}
}
