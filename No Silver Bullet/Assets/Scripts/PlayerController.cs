using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	#region Member variables

	private enum Direction
	{
		Up,
		Left,
		Right,
		Down
	}
	
	public AudioClip mySlashClip;
	public float mySpeed;
	private Animator myAnimator;
	private Rigidbody2D myRidigBody;
	private Vector3 myMovement;
	private Direction myDirection;
	private int myDamage;
	private float myRange;
	private bool myIsAttacking;
	private float myTimeSinceAttacking;
	private const float AttackDuration = 0.2f;
	private int myHorizontalInput;
	private int myVerticalInput;
	public ProgressTracker myProgressTracker;

	#endregion

	#region Private methods

	private void Awake ()
	{
		myProgressTracker = new ProgressTracker ();
		myAnimator = GetComponent<Animator> ();
		myRidigBody = GetComponent<Rigidbody2D> ();
		myDirection = Direction.Down;
		myIsAttacking = false;
		myTimeSinceAttacking = 0;
		myDamage = 50;
		myRange = 0.5f;
	}

	private void Update ()
	{
		Movement ();

		if (myIsAttacking == true)
		{
			Attack ();
			UpdateAttackDuration ();
		}
	
		if (Input.GetKeyDown (KeyCode.R))	//TODO change for other option? -Andy
		{
			SavedGame.SaveGame ();
		}		
	}
	
	private void UpdateAttackDuration ()
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
	
	private Vector2 SetAttackDirection ()
	{
		switch (myDirection)
		{
			case Direction.Down:
				return Vector2.down;
			case Direction.Left:
				return Vector2.left;
			case Direction.Right:
				return Vector2.right;
			case Direction.Up:
				return Vector2.up;
		}

		return Vector2.zero;
	}
	
	private RaycastHit2D[] EnemiesHit ()
	{
		Vector2 direction = SetAttackDirection ();
		RaycastHit2D[] hits = Physics2D.RaycastAll (new Vector2 (transform.position.x, transform.position.y), direction, myRange);

		return hits;
	}
	
	private void DamageHitEnemies (RaycastHit2D[] aHitArray)
	{
		for (int i = 0; i < aHitArray.Length; i++)
		{
			if (aHitArray [i].collider.tag == "Enemy")
			{
				GameObject enemy = aHitArray [i].collider.gameObject;
				EnemyController enemyController = enemy.GetComponent<EnemyController> ();
				enemyController.TakeDamage (myDamage);
			}
		}
	}
	
	private void Attack ()
	{
		if (myTimeSinceAttacking == 0)
		{
			RaycastHit2D[] hits = EnemiesHit ();
			DamageHitEnemies (hits);
			SoundManager.instance.PlaySingle (mySlashClip);
		}
	}
	
	private void Movement ()
	{
		myAnimator.enabled = true;
		myHorizontalInput = 0;
		myVerticalInput = 0;

		myHorizontalInput = (int)(Input.GetAxisRaw ("Horizontal"));
		myVerticalInput = (int)(Input.GetAxisRaw ("Vertical"));
	
		Move ();
		UpdateAnimation ();
	}
	
	private void Move ()
	{
		if (myIsAttacking == false)
		{
			myMovement.Set (myHorizontalInput, myVerticalInput, 0);
			myMovement = myMovement.normalized * mySpeed * Time.deltaTime;
			myRidigBody.MovePosition (transform.position + myMovement);
		}
	}
	
	private void UpdateAnimation ()
	{
		UpdateWalkAnimation ();
		UpdateAttackAnimation ();
	}
	
	private void UpdateWalkAnimation ()
	{
		bool pressedNothing = false;
	
		if (myHorizontalInput > 0)
		{
			myAnimator.SetTrigger ("PressedRight");
			myDirection = Direction.Right;
		}
		else if (myHorizontalInput < 0)
		{
			myAnimator.SetTrigger ("PressedLeft");
			myDirection = Direction.Left;
		}
		else if (myVerticalInput > 0)
		{
			myAnimator.SetTrigger ("PressedUp");
			myDirection = Direction.Up;
		}
		else if (myVerticalInput < 0)
		{
			myAnimator.SetTrigger ("PressedDown");
			myDirection = Direction.Down;
		}
		else
		{
			pressedNothing = true;
		}
		

		myAnimator.SetBool ("PressedNothing", pressedNothing);

	}
	
	private void UpdateAttackAnimation ()
	{
		if (Input.GetButtonDown ("Attack"))
		{
			myAnimator.SetBool ("PressedNothing", true);
			myIsAttacking = true;
					
			switch (myDirection)
			{
				case Direction.Down:
					myAnimator.SetTrigger ("AttackDown");
					break;
				case Direction.Left:
					myAnimator.SetTrigger ("AttackLeft");
					break;
				case Direction.Right:
					myAnimator.SetTrigger ("AttackRight");
					break;
				case Direction.Up:
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
