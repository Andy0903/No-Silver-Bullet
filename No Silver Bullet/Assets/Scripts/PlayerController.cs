using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	#region Member variables

	enum Direction
	{
		Up,
		Left,
		Right,
		Down
	}

	public float mySpeed;
	private Animator myAnimator;
	private Rigidbody2D myRidigBody;
	private Vector3 myMovement;
	private Direction myDirection;

	bool myIsAttacking;
	float myTimeSinceAttacking;
	const float AttackDuration = 0.2f;

	int myHorizontal;
	int myVertical;

	#endregion

	#region Private methods

	private void Awake ()
	{
		myAnimator = GetComponent<Animator> ();
		myRidigBody = GetComponent<Rigidbody2D> ();
		myDirection = Direction.Down;
		myIsAttacking = false;
		myTimeSinceAttacking = 0;
	}
	
	private void FixedUpdate ()
	{
		Movement ();

		if (myIsAttacking == true) 
		{
			if (myTimeSinceAttacking >= AttackDuration)
			{
				myIsAttacking = false;
				myTimeSinceAttacking = 0;
			}
			else
			{
				myTimeSinceAttacking += Time.deltaTime;
			}
		}
	}

	private void Movement ()
	{
		myAnimator.enabled = true;
		myHorizontal = 0;
		myVertical = 0;

		myHorizontal = (int)(Input.GetAxisRaw ("Horizontal"));
		myVertical = (int)(Input.GetAxisRaw ("Vertical"));

		Move ();
		UpdateAnimation ();
	}

	private void Move ()
	{
		if (myIsAttacking == false)
		{
			myMovement.Set (myHorizontal, myVertical, 0);
			myMovement = myMovement.normalized * mySpeed * Time.deltaTime;
			myRidigBody.MovePosition (transform.position + myMovement);
		}
	}

	private void UpdateAnimation ()
	{
		if (myHorizontal > 0) {
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedRight");
				myDirection = Direction.Right;
			} else if (myHorizontal < 0) {
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedLeft");
				myDirection = Direction.Left;
			} else if (myVertical > 0) {
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedUp");
				myDirection = Direction.Up;
			} else if (myVertical < 0) {
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedDown");
				myDirection = Direction.Down;
			} else {
				myAnimator.SetBool ("PressedNothing", true);
			}


			if (Input.GetButtonDown ("Attack")) {
				switch (myDirection) {
			case Direction.Down:
				myAnimator.SetBool ("PressedNothing", true);
					myIsAttacking = true;
					myAnimator.SetTrigger ("AttackDown");
					break;
			case Direction.Left:
				myAnimator.SetBool ("PressedNothing", true);
					myIsAttacking = true;
					myAnimator.SetTrigger ("AttackLeft");
					break;
			case Direction.Right:
				myAnimator.SetBool ("PressedNothing", true);
					myIsAttacking = true;
					myAnimator.SetTrigger ("AttackRight");
					break;
			case Direction.Up:
				myAnimator.SetBool ("PressedNothing", true);
					myIsAttacking = true;
					myAnimator.SetTrigger ("AttackUp");
					break;
				default:
					break;
				}
			}
		myAnimator.SetBool ("IsAttacking", myIsAttacking);
	}

	#endregion
}