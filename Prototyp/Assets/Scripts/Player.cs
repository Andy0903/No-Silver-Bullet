using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

	public float mySpeed = 5;

	private Animator myAnimator;
	private Vector3 myMovement;
	private Rigidbody2D myRigidBody;

	int myHorizontal;
	int myVertical;

	void Awake()
	{
		myAnimator = GetComponent<Animator> ();
		myRigidBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		Movement ();
	}

	void LateUpdate()
	{
		if (myHorizontal == 0 && myVertical == 0 && myAnimator.isActiveAndEnabled == true)
		{
			myAnimator.enabled = false;
		}
	}

	private void Movement()
	{
		myAnimator.enabled = true;
		myHorizontal = 0;
		myVertical = 0;

		myHorizontal = (int)(Input.GetAxisRaw("Horizontal"));
		myVertical = (int)(Input.GetAxisRaw ("Vertical"));

		Move ();
		UpdateAnimation ();

	}

	public void SetPosition(Transform aTransform)
	{
		gameObject.transform.position = aTransform.position;
	}

	private void Move()
	{
		myMovement.Set(myHorizontal, myVertical, 0);

		myMovement = myMovement.normalized * mySpeed * Time.deltaTime;

		myRigidBody.MovePosition(transform.position + myMovement);
	}

	private void UpdateAnimation()
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
