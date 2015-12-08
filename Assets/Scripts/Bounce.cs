using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
	public float amplitude;	// amplitude of bounce
	public float radius;	// radius of tunnel
	public float forwardVel = 1.0f;
	public float lateralVel = 1.0f;
	public float bounceSpeed = 1.0f;
	public float bounceAngle;
	public AnimationCurve bounceCurve;
	public KeyCode moveLeft;
	public KeyCode moveRight;

	public string bounceAnim;

	protected Vector2 bounceNormal;
	protected Vector2 bounceFloor;

	protected float height;
	protected float hangTime;
	protected float repeatTime;
	
	public bool bouncing;
	protected bool canBounce;
	protected bool dead;

	protected Animator anim;
	protected Scorer scorer;

	// Use this for initialization
	void Start () {
		bounceNormal = new Vector2(Vector3.up.x, Vector3.up.y);
		hangTime = 0f;
		repeatTime = bounceCurve.keys [bounceCurve.keys.Length - 1].time;
		canBounce = true;
		anim = GetComponent<Animator> ();
		scorer = GetComponent<Scorer> ();
	}
	
	// Update is called once per frame
	void Update () {
		float t;

		if (bouncing || dead) {
			return;
		}

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
		t = hangTime * bounceSpeed;
		height = bounceCurve.Evaluate (t);
		if (t >= repeatTime / 2.0f) {
			canBounce = true;
		}

		// if height is < 0, are we dead?
		if (height <= (transform.lossyScale.y / 2.0f)) {
			if (Physics.Raycast (new Ray (transform.position, transform.TransformDirection (Vector3.down)))) {
				height = transform.lossyScale.y / 4.0f;
				Jump (1.0f);
			}
		}

		// update position & rotation
		transform.position = new Vector3((radius-height) * 2.0f * Mathf.Cos (bounceAngle),
		                                 (radius-height) * 2.0f * Mathf.Sin (bounceAngle),
		                                 transform.position.z + forwardVel * Time.deltaTime);
		transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(0.0f, 0.0f, transform.position.z));
		hangTime += Time.deltaTime;
	}

	public void Jump(float bounceFac) {
		if (bouncing || !canBounce) {
			return;
		}
		scorer.Score (1);
		anim.SetBool ("bounce", true);
		hangTime = 0.0f;
		canBounce = false;
	}

	public void Die() {
		Debug.Log ("DIE");
		dead = true;
	}
}
