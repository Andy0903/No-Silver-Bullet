using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {


	#region Member Variables

	private enum Direction
	{
		Up,
		Left,
		Right,
		Down
	}

	private enum State
	{
		Idle, //TODO: Change the name?
		Talking
	}

	private Direction myCurrentDirection;
	private State myCurrentState;

	public float mySpeed;
	public int myWalkingLength;
	private Vector3 myLastDirectionChangePos;
	private Vector3 myMovement;
	private Animator myAnimator;
	private Rigidbody2D myRigidBody;

	#endregion

	#region Private Methods

	private void Awake () 
	{
		myAnimator = GetComponent<Animator> ();
		myRigidBody = GetComponent<Rigidbody2D> ();
		myCurrentState = State.Idle;
	}

	private void Update () 
	{
		Movement ();
		UpdateWalkingAnimation ();
	}

	private void Movement()
	{
		if (myCurrentState == State.Idle)
		{
			if (myCurrentDirection == Direction.Down)
			{
				myMovement = Vector3.down * mySpeed * Time.deltaTime;
				myRigidBody.MovePosition (transform.position + myMovement);
			}
			else if (myCurrentDirection == Direction.Up)
			{
				myMovement = Vector3.up * mySpeed * Time.deltaTime;
				myRigidBody.MovePosition (transform.position + myMovement);
			}

			float walked = Vector3.Distance (myLastDirectionChangePos, transform.position);

			if (walked > myWalkingLength)
			{
				ChangeDirection ();
			}
		}
	}

	private void UpdateWalkingAnimation()
	{
		if (myCurrentState == State.Idle) //Changes the animation based on direction
		{
			if (myCurrentDirection == Direction.Down)
			{
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedDown");
			}
			if (myCurrentDirection == Direction.Left)
			{
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedLeft");
			}
			if (myCurrentDirection == Direction.Up)
			{
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedUp");
			}
			if (myCurrentDirection == Direction.Right)
			{
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedRight");
			}
		}

	}

	private void ChangeDirection()
	{
		myLastDirectionChangePos = transform.position;
		if (myCurrentDirection == Direction.Up)
		{
			myCurrentDirection = Direction.Down;
		}
		else if (myCurrentDirection == Direction.Down)
		{
			myCurrentDirection = Direction.Up;
		}
	}

	#endregion
}
