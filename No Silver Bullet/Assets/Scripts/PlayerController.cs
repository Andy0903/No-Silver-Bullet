using UnityEngine;
using System.Collections;

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

	public float mySpeed;
	public int myHealth;
	private Animator myAnimator;
	private Rigidbody2D myRidigBody;
	private Vector3 myMovement;
	private Direction myDirection;
	private int myDamage;
	private float myRange;

	private bool myIsAttacking;
	private float myTimeSinceAttacking;
	private const float AttackDuration = 0.2f;

	private int myHorizontal;
	private int myVertical;

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
		myDirection = Direction.Down;
		myIsAttacking = false;
		myTimeSinceAttacking = 0;
		myDamage = 50;
		myRange = 0.5f;


		//transform.position = new Vector3 (15, -57, 0);
	}

	private void Update ()
	{
		Movement ();

		if (myIsAttacking == true)
		{
			Attack ();
		}

	}

	private void Attack ()
	{
		if (myIsAttacking == true && myTimeSinceAttacking == 0)
		{
			Vector2 direction = Vector2.zero;
			switch (myDirection)
			{
			case Direction.Down:
				direction = Vector2.down;
				break;
			case Direction.Left:
				direction = Vector2.left;
				break;
			case Direction.Right:
				direction = Vector2.right;
				break;
			case Direction.Up:
				direction = Vector2.up;
				break;
			}

			//Debug.DrawRay (transform.position, direction * 200f);
			RaycastHit2D[] hits = Physics2D.RaycastAll (new Vector2 (transform.position.x, transform.position.y), direction, myRange);

			for (int i = 0; i < hits.Length; i++)
			{
				if (hits [i].collider.tag == "Enemy")
				{
					GameObject enemy = hits [i].collider.gameObject;
					EnemyController enemyController = enemy.GetComponent<EnemyController> ();

					enemyController.TakeDamage (myDamage);
				}
			}
		}

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
		UpdateWalkAnimation ();
		UpdateAttackAnimation ();
	}

	private void UpdateWalkAnimation ()
	{
		bool pressedNothing = false;

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
		else if (myVertical > 0)
		{
			myAnimator.SetTrigger ("PressedUp");
			myDirection = Direction.Up;
		}
		else if (myVertical < 0)
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