using UnityEngine;
using System.Collections;
using UnityEditor;

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

	private Direction myCurrentDirection;
	private State myCurrentState;

	public int myHealth;
	public int myWalkingLength;
	public int myDamage;
	public int myParticlesOnDeath;
	public float myTimeBetweenAttacks;
	public float mySpeed;
	public float myDetectionRange;
	public float myAttackRange;


	private float myHorizontal;
	private float myVertical;
	private float myTimeSinceLastAttack;
	private float myTimeSinceDeath;
	private float myTimeSinceIdle;
	private bool myIsMoving;

	private ParticleSystem myParticleSystem;
	private Animator myAnimator;
	private Rigidbody2D myRigidBody;
	private Vector3 myMovement;
	private Vector3 myIdleStartPosition;
	//Might rename this to myLastDirectionChangePos? Or similar
	private GameObject myTarget;


	#endregion

	#region Public methods

	public void TakeDamage (int aDamage)
	{
		myHealth -= aDamage;

		//Test code for knockback, check into this later
		//myRidigBody.AddForce isn't working
		/*Vector3 difference = myTarget.transform.position - transform.position;
		difference.Normalize ();
		myRidigBody.MovePosition (transform.position + (-difference));*/

	}

	#endregion

	#region Private methods

	private void Awake ()
	{
		myParticleSystem = GetComponent<ParticleSystem> ();
		myAnimator = GetComponent<Animator> ();
		myRigidBody = GetComponent<Rigidbody2D> ();
		myCurrentDirection = Direction.Down;
		myCurrentState = State.Idle;
		myTarget = GameObject.FindGameObjectWithTag ("Player"); //Sets the enemies target to the Player GameObject
		myIsMoving = true;
		myIdleStartPosition = myRigidBody.transform.position;
	}

	private void Update ()
	{
		myMovement = myTarget.transform.position - transform.position;
		myMovement.Normalize ();
		myHorizontal = myMovement.x;
		myVertical = myMovement.y;
		myTimeSinceIdle += Time.deltaTime;
		myTimeSinceLastAttack += Time.deltaTime;
		UpdateWalkAnimation ();
		UpdateAttack ();
		Movement ();
	}

	private void LateUpdate ()
	{
		if (myHealth <= 0)
		{
			myTimeSinceDeath += Time.deltaTime;
			myCurrentState = State.Dead;
			if (myParticleSystem.isStopped == true)
			{
				//TODO: Add death animation or something similar
				myParticleSystem.Emit (myParticlesOnDeath);
			}
			else if (myTimeSinceDeath > myParticleSystem.duration)
			{
				Destroy (gameObject);
			}

			//Code that might be needed in the future for changing values of the component inside the code
			/*SerializedObject so = new SerializedObject(GetComponent<ParticleSystem>());
			so.FindProperty ("ShapeModule.arc").floatValue = angleBetweenTargetDegrees;
			so.ApplyModifiedProperties ();*/


		}
	}

	private void Movement ()
	{
		if (myIsMoving == true)
		{
			float distance = Vector3.Distance (transform.position, myTarget.transform.position);
			if (distance < myDetectionRange)
			{
				myCurrentState = State.Attacking;
				myMovement = myMovement * mySpeed * Time.deltaTime;
				myRigidBody.MovePosition (transform.position + myMovement);
			}
			else if (myCurrentState == State.Attacking)
			{
				myCurrentState = State.Idle;
				myIdleStartPosition = transform.position;
			}

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
				else if (myCurrentDirection == Direction.Left)
				{
					myMovement = Vector3.left * mySpeed * Time.deltaTime;
					myRigidBody.MovePosition (transform.position + myMovement);

				}
				else if (myCurrentDirection == Direction.Right)
				{
					myMovement = Vector3.right * mySpeed * Time.deltaTime;
					myRigidBody.MovePosition (transform.position + myMovement);

				}

				//When the GO has walked a set amount of units from where it last changed direction it'll change direction again while idle

				float distanceFromIdleStart = Vector3.Distance (transform.position, myIdleStartPosition); 

				if (distanceFromIdleStart > myWalkingLength)
				{
					ChangeDirection ();
					myIdleStartPosition = myRigidBody.transform.position;
				}


			}
		}
	}

	private void UpdateAttack ()
	{
		float distanceFromTarget = Vector3.Distance (transform.position, myTarget.transform.position);

		if (myAttackRange > distanceFromTarget && myTimeSinceLastAttack > myTimeBetweenAttacks)
		{
			PlayerHealth targetHealth = myTarget.GetComponent<PlayerHealth> ();
			targetHealth.TakeDamage (myDamage);
			myTimeSinceLastAttack = 0;
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
					myCurrentDirection = Direction.Right;
					myAnimator.SetBool ("PressedNothing", false);
				}
				else if (myHorizontal < 0)
				{
					myAnimator.SetTrigger ("PressedLeft");
					myCurrentDirection = Direction.Left;
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
					myCurrentDirection = Direction.Up;
					myAnimator.SetBool ("PressedNothing", false);
				}
				else if (myVertical < 0)
				{
					myAnimator.SetTrigger ("PressedDown");
					myCurrentDirection = Direction.Down;
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
				ChangeDirection ();
				myIdleStartPosition = myRigidBody.transform.position;
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

	private void ChangeDirection ()
	{
		if (myCurrentDirection == Direction.Down)
		{
			myCurrentDirection = Direction.Left;
		}
		else if (myCurrentDirection == Direction.Left)
		{
			myCurrentDirection = Direction.Up;
		}
		else if (myCurrentDirection == Direction.Up)
		{
			myCurrentDirection = Direction.Right;
		}
		else if (myCurrentDirection == Direction.Right)
		{
			myCurrentDirection = Direction.Down;
		}
	}

	private void InitiateParticleSystem ()
	{

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
