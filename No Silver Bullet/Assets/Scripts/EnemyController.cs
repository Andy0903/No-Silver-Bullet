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

	private enum State
	{
		Attacking,
		Idle,
		Running,
		Walking,
		Dead
	}

	private Direction myDirection;
	private State myCurrentState;

	public int myHealth;
	public float mySpeed;
	public float myRange;
	private float myHorizontal;
	private float myVertical;
	private Animator myAnimator;
	private Rigidbody2D myRidigBody;
	private CircleCollider2D myCircleCollider;
	private Vector3 myMovement;
	private Vector3 myIdleStartPosition;
	private GameObject myTarget;
	private bool myIsMoving;
	private float myTimeSinceIdle;

	#endregion

	#region Public methods

	public void TakeDamage (int aDamage)
	{
		myHealth -= aDamage;
	}

	#endregion

	#region Private methods

	private void Awake ()
	{
		myAnimator = GetComponent<Animator> ();
		myRidigBody = GetComponent<Rigidbody2D> ();
		myCircleCollider = GetComponent<CircleCollider2D> ();
		myDirection = Direction.Down;
		myCurrentState = State.Idle;
		myTarget = GameObject.FindGameObjectWithTag ("Player"); //Sets the enemies target to the Player GameObject
		myIsMoving = true;
	}

	private void Update ()
	{
		myMovement = myTarget.transform.position - transform.position;
		myMovement.Normalize ();
		myHorizontal = myMovement.x;
		myVertical = myMovement.y;
		myTimeSinceIdle += Time.deltaTime;
		UpdateWalkAnimation ();
		Movement ();
	}

	private void LateUpdate ()
	{
		if (myHealth <= 0)
		{
			myCurrentState = State.Dead;
			Destroy (gameObject); //TODO: Remove/Change so we can add dead animation and/or loot drop
		}
	}

	private void Movement ()
	{
		if (myIsMoving == true)
		{
			float distance = Vector3.Distance (transform.position, myTarget.transform.position);
			if (distance < myRange)
			{
				myCurrentState = State.Attacking;
				myMovement = myMovement * mySpeed * Time.deltaTime;
				myRidigBody.MovePosition (transform.position + myMovement);
			}
			else
			{
				myCurrentState = State.Idle;
				myIdleStartPosition = transform.position;
			}

			if (myCurrentState == State.Idle)
			{
				if (myDirection == Direction.Down)
				{
					myMovement = Vector3.down * mySpeed * Time.deltaTime;
					myRidigBody.MovePosition (transform.position + myMovement);
				}
				else if (myDirection == Direction.Up)
				{
					myMovement = Vector3.up * mySpeed * Time.deltaTime;
					myRidigBody.MovePosition (transform.position + myMovement);

				}
				else if (myDirection == Direction.Left)
				{
					myMovement = Vector3.left * mySpeed * Time.deltaTime;
					myRidigBody.MovePosition (transform.position + myMovement);

				}
				else if (myDirection == Direction.Right)
				{
					myMovement = Vector3.right * mySpeed * Time.deltaTime;
					myRidigBody.MovePosition (transform.position + myMovement);

				}

				//Whenever the enemy has walked far away from it's starting idle position
				/*if (transform.position.y - myIdleStartPosition.y > 1)
				{
					myDirection = Direction.Left;
				}
				else if (transform.position.x - myIdleStartPosition.x < 1)
				{
					myDirection = Direction.Up;
				}
				else if (transform.position.y - myIdleStartPosition.y < 1)
				{
					myDirection = Direction.Right;
				}
				else if (transform.position.x - myIdleStartPosition.x > 1)
				{
					myDirection = Direction.Down;
				} */

			}
		}
	}

	private void UpdateWalkAnimation ()
	{
		if (myCurrentState == State.Attacking)
		{
			//"Cheap" way to see which direction is greater than the other TODO: Find a better way of doing it
			if (myHorizontal * myHorizontal > myVertical * myVertical)
			{
				if (myHorizontal > 0)
				{
					myAnimator.SetTrigger ("PressedRight");
					myDirection = Direction.Right;
					myAnimator.SetBool ("PressedNothing", false);
				}
				else if (myHorizontal < 0)
				{
					myAnimator.SetTrigger ("PressedLeft");
					myDirection = Direction.Left;
					myAnimator.SetBool ("PressedNothing", false);
				}
				else
				{
					myAnimator.SetBool ("PressedNothing", true);
				}
			}
			else
			{
				if (myVertical > 0)
				{
					myAnimator.SetTrigger ("PressedUp");
					myDirection = Direction.Up;
					myAnimator.SetBool ("PressedNothing", false);
				}
				else if (myVertical < 0)
				{
					myAnimator.SetTrigger ("PressedDown");
					myDirection = Direction.Down;
					myAnimator.SetBool ("PressedNothing", false);
				}
				else
				{
					myAnimator.SetBool ("PressedNothing", true);
				}
			}
		}

		if (myCurrentState == State.Idle) //Changes the animation based on direction
		{
			if (myDirection == Direction.Down)
			{
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedDown");
			}
			if (myDirection == Direction.Left)
			{
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedLeft");
			}
			if (myDirection == Direction.Up)
			{
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedUp");
			}
			if (myDirection == Direction.Right)
			{
				myAnimator.SetBool ("PressedNothing", false);
				myAnimator.SetTrigger ("PressedRight");
			}
		}


	}


	private void OnTriggerEnter2D (Collider2D other)
	{
		
		if (other.gameObject.CompareTag ("Player")) //The GO stops moving when it enters a collision with a player
		{
			myIsMoving = false; 	
		}

		if (other.CompareTag ("Untagged"))
		{
			//myHealth = 0; //Used for testing health system
			if (myCurrentState == State.Idle)
			{
				if (myDirection == Direction.Down)
				{
					myDirection = Direction.Left;
				}
				else if (myDirection == Direction.Left)
				{
					myDirection = Direction.Up;
				}
				else if (myDirection == Direction.Up)
				{
					myDirection = Direction.Right;
				}
				else if (myDirection == Direction.Right)
				{
					myDirection = Direction.Down;
				}	
			}	
		} 
	}

	private void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) //When the GO and Player no longer collides the GO starts moving again
		{
			myIsMoving = true;	
		}
	}

	/*private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.IsTouching(myCircleCollider))
		{
			if (myDirection == Direction.Down)
			{
				myDirection = Direction.Left;
			}
			if (myDirection == Direction.Left)
			{
				myDirection = Direction.Up;
			}
			if (myDirection == Direction.Up)
			{
				myDirection = Direction.Right;
			}
			if (myDirection == Direction.Right)
			{
				myDirection = Direction.Down;
			}	
		}	
	}*/

	#endregion


}
