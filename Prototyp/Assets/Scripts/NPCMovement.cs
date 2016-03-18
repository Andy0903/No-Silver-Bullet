using UnityEngine;
using System.Collections;

public class NPCMovement : MonoBehaviour
{

	public float myMaxTravelDistance;
	public float mySpeed;

	private float myHorizontal;
	private float myVertical;

	private Vector3 myStartPosition;
	private Animator myAnimator;
	private Vector3 myMovement;
	private Rigidbody2D myRigidBody;

	void Start ()
	{
		myAnimator = GetComponent<Animator> ();
		myRigidBody = GetComponent<Rigidbody2D> ();
		myStartPosition = gameObject.transform.position;
		InvokeRepeating ("ChangeSpeeds", 1, 1.0f);
	}

	private void FixedUpdate ()
	{	
		Move ();
		UpdateAnimation ();
	}

	private bool WithinDistanceLimit ()
	{
		//Debug.Log (Vector3.Distance (transform.position, myStartPosition));
		if (Vector3.Distance (transform.position, myStartPosition) < myMaxTravelDistance)
		{	
			return true;
		}
		else
		{
			return false;
		}
	}

	private void ChangeSpeeds ()
	{
		if (WithinDistanceLimit () == true)
		{
			myAnimator.enabled = true;
			myHorizontal = Random.Range (-mySpeed, mySpeed);
			myVertical = Random.Range (-mySpeed, mySpeed);

//			if (Random.Range (1, 3) % 2 == 0)
//			{
//				myHorizontal = 0;
//				myVertical = 0;
//				myAnimator.enabled = false;
//			}
		}
		else
		{
			if (transform.position.x > myStartPosition.x)
			{
				myHorizontal = -mySpeed;//(int)Random.Range (-mySpeed, 0);
			}
			else
			{
				myHorizontal = mySpeed;//(int)Random.Range (0, mySpeed);
			}


			if (transform.position.y > myStartPosition.y)
			{
				myVertical = -mySpeed;//(int)Random.Range (-mySpeed, 0);
			}
			else
			{
				myVertical = mySpeed;//(int)Random.Range (0, mySpeed);
			}
		}
	}

	private void Move ()
	{
		myMovement.Set (myHorizontal, myVertical, 0);

		myMovement = myMovement.normalized * mySpeed * Time.deltaTime;

		myRigidBody.MovePosition (transform.position + myMovement);
	}

	private void UpdateAnimation ()
	{
		if (myHorizontal > 0)
		{
			myAnimator.SetTrigger ("PressedRight");
		}
		else if (myHorizontal < 0)
			{
				myAnimator.SetTrigger ("PressedLeft");
			}
			else if (myVertical > 0)
				{
					myAnimator.SetTrigger ("PressedUp");
				}
				else if (myVertical < 0)
					{
						myAnimator.SetTrigger ("PressedDown");
					}
	}
}
