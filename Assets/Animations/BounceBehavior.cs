using UnityEngine;
using System.Collections;

public class BounceBehavior : StateMachineBehaviour
{
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetComponent<Bounce> ().bouncing = true;
	}
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetComponent<Bounce> ().bouncing = false;
		animator.SetBool ("bounce", false);

	}
}