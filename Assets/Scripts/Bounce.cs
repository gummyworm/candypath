using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
	public float amplitude;	// amplitude of bounce
	public float radius;	// radius of tunnel
	public float forwardVel = 1.0f;
	public float lateralVel = 1.0f;
	public float bounceSpeed = 1.0f;
	public float deathHeight = -1.0f;
	public float bounceAngle;
	public AnimationCurve bounceCurve;
	public KeyCode moveLeft;
	public KeyCode moveRight;

	protected float height;
	protected float hangTime;
	protected float repeatTime;
	
	protected bool canBounce;
	protected bool dead;

	protected Animator anim;
	protected int bounceAnimID;
	protected Scorer scorer;

	// Use this for initialization
	void Start () {
		hangTime = 0.0f;
		repeatTime = bounceCurve.keys [bounceCurve.keys.Length - 1].time;
		canBounce = true;
		anim = GetComponent<Animator> ();
		scorer = GetComponent<Scorer> ();	
		bounceAnimID = Animator.StringToHash ("bounce");
	}
	
	// Update is called once per frame
	void Update () {
		if (dead || anim.GetBool(bounceAnimID)) {
			return;
		}

		// player motion
		if (Input.GetKey (moveLeft)) {
			bounceAngle -= lateralVel * Time.deltaTime;
		}
		if (Input.GetKey (moveRight)) {
			bounceAngle += lateralVel * Time.deltaTime;
		}

		if (height < deathHeight) {

		}

		// update height
		float t = hangTime * bounceSpeed;
		height = amplitude * bounceCurve.Evaluate (t);
		if (t >= repeatTime / 2.0f) {
			canBounce = true;
		}

		// if height is < 0, are we dead?
		if (height <= (transform.lossyScale.y / 2.0f)) {
			RaycastHit hit;
			if (Physics.Raycast (new Ray (transform.position, transform.TransformDirection (Vector3.down)), out hit)) {
				TunnelTile tile = hit.transform.gameObject.GetComponent<TunnelTile>();
				if (tile) {
					tile.OnBounce(this);
					height = transform.lossyScale.y / 4.0f;
					Jump (1.0f);
				}
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
		if (!canBounce || anim.GetBool(bounceAnimID)) {
			return;
		}
		scorer.Score (1);
		anim.SetBool (bounceAnimID, true);
		hangTime = 0.0f;
		canBounce = false;
	}

	public void Die() {
		dead = true;
	}
}
