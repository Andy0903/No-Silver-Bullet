using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	#region Member variables

	private enum Direction
	{
		Up,
		Left,
		Right,
		Down
	}

	public float mySpeed;
	public float myRange;
	private float myHorizontal;
	private float myVertical;
	private Animator myAnimator;
	private Rigidbody2D myRidigBody;
	private Vector3 myMovement;
	private Direction myDirection;
	private GameObject myTarget;
	private bool myIsMoving = true;




	#endregion

	#region Private methods

	private void Awake ()
	{
		myAnimator = GetComponent<Animator> ();
		myRidigBody = GetComponent<Rigidbody2D> ();
		myDirection = Direction.Down;
		myTarget = GameObject.FindGameObjectWithTag ("Player"); //Sets the enemies target to the Player GameObject
	}

	private void Update()
	{
		myMovement = myTarget.transform.position - transform.position;
		myMovement.Normalize ();
		myHorizontal = myMovement.x;
		myVertical = myMovement.y;
		UpdateWalkAnimation ();
		Movement ();
	}

	private void Movement()
	{
		if (myIsMoving == true)
		{
			float distance = Vector3.Distance (transform.position, myTarget.transform.position);
			if (distance < myRange)
			{

				myMovement = myMovement * mySpeed * Time.deltaTime;
				myRidigBody.MovePosition (transform.position + myMovement);
			}
		}

	}

	private void UpdateWalkAnimation ()
	{
		//"Cheap" way to see which direction is greater than the other TODO: Find a better way of doing it
		if (myHorizontal * myHorizontal > myVertical * myVertical) 
		{
			if (myHorizontal > 0)
			{
				myAnimator.SetTrigger ("PressedRight");
				myDirection = Direction.Right;

			}
			else if (myHorizontal < 0)
				{
					myAnimator.SetTrigger ("PressedLeft");
					myDirection = Direction.Left;
				}
		}
		else
		{
			if (myVertical > 0)
			{
				myAnimator.SetTrigger ("PressedUp");
				myDirection = Direction.Up;
			}
			else if (myVertical < 0)
				{
					myAnimator.SetTrigger ("PressedDown");
					myDirection = Direction.Down;
				}
		}



	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.gameObject.CompareTag("Player")) //The GO stops moving when it enters a collision with a player
		{
			myIsMoving = false; 	
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player")) //When the GO and Player no longer collides the GO starts moving again
		{
			myIsMoving = true;	
		}
	}


	#endregion


}
