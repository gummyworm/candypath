using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
	public float amplitude;	// amplitude of bounce
	public float radius;	// radius of tunnel
	public float forwardVel;
	public float lateralVel = 1.0f;
	public float bounceAngle;
	public AnimationCurve bounceCurve;
	public KeyCode moveLeft;
	public KeyCode moveRight;

	protected Vector2 basePos;	// position at angle=0
	protected Vector2 bounceNormal;
	protected Vector2 bounceFloor;

	protected float height;
	protected float hangTime;
	protected float repeatTime;

	protected bool canBounce;

	// Use this for initialization
	void Start () {
		bounceNormal = new Vector2(Vector3.up.x, Vector3.up.y);
		hangTime = 0.0f;
		canBounce = true;
		repeatTime = bounceCurve.keys [bounceCurve.keys.Length - 1].time;
		basePos = new Vector2 (transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		// player motion
		if (Input.GetKey (moveLeft)) {
			bounceAngle -= lateralVel * Time.deltaTime;
		}
		if (Input.GetKey (moveRight)) {
			bounceAngle += lateralVel * Time.deltaTime;
		}

		// if we've been up longer than bounce animation, die
		if (hangTime > repeatTime) {
			Die ();
		}

		// update height
		height = bounceCurve.Evaluate (hangTime);

		// update position
		transform.position = new Vector3((radius-height) * 2.0f * Mathf.Cos (bounceAngle), 
		                                 (radius-height) * 2.0f * Mathf.Sin (bounceAngle),
		                                 transform.position.z + forwardVel * Time.deltaTime);

		if (height > amplitude / 2.0f) {
			Debug.Log("RDY");
			canBounce = true;
		}
		hangTime += Time.deltaTime;
	}
	
	public void Jump(float bounceFac) {
		if (!canBounce) {
			return;
		}
		canBounce = false;
		height = amplitude * bounceFac;
		hangTime = 0.0f;
	}

	public void Die() {
		Debug.Log ("DIE");
	}
}
