﻿using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
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
		Idle,
		//TODO: Change the name?
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
	private Renderer myRenderer;
	private DialogueReader myDialogueReader;
	private bool myIsTalking;
	private bool myIsFirstTalk;
	private int myDialogueChoice;

	#endregion

	#region Private Methods

	private void Awake ()
	{
		myAnimator = GetComponent<Animator> ();
		myRigidBody = GetComponent<Rigidbody2D> ();
		myCurrentState = State.Idle;
		myRenderer = GetComponent<Renderer> ();
		myDialogueReader = DialogueReader.Load ();
		myIsTalking = false;
	}

	private void Update ()
	{
		if (myRenderer.isVisible)
		{
			Movement ();
			UpdateWalkingAnimation ();
		}
	}

	private void Movement ()
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

			float walked = Vector3.Distance (myLastDirectionChangePos, transform.position);

			if (walked > myWalkingLength)
			{
				ChangeDirection ();
			}
		}
	}

	private void UpdateWalkingAnimation ()
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

	private void ChangeDirection ()
	{			

		myLastDirectionChangePos = transform.position;

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

	/*	private void ReadMyDialogue ()
	{
		int offset = 50;
		CharacterData tempData = myDialogueReader.FindCharacter ("QuestGiver1");	//Tag used to be instead of test val QuestGiver1
		//To be used for testing in the future
		//Needs to be redone
		GUI.TextField (new Rect (Screen.currentResolution.width / 2 - offset, Screen.currentResolution.height - offset, 50, 50), 
			tempData.dialogue [0]); //TODO: Write out each dialogue line
	}*/

	private void OnGUI ()
	{
		if (myIsTalking == true)
		{
			
			GUIStyle style = new GUIStyle ();
			style.fontSize = 24;
			style.alignment = TextAnchor.UpperCenter;
			int offset = 50;
			CharacterData tempData = myDialogueReader.FindCharacter ("NPC");	//Tag used to be instead of test val QuestGiver1
			if (myIsFirstTalk == true)
			{
				myDialogueChoice = Random.Range (0, tempData.dialogue.Length);
				myIsFirstTalk = false;
			}
			//To be used for testing in the future
			//Needs to be redone
			//GUI.Label (new Rect (Screen.currentResolution.width / 2 - offset, Screen.currentResolution.height - offset, 50, 50), 
			//	tempData.dialogue [0]); //TODO: Write out each dialogue line
			GUI.enabled = true;
			GUI.Label (new Rect (Camera.main.pixelWidth / 2 - offset, Camera.main.pixelHeight - offset, 200, 200), 
				tempData.dialogue [myDialogueChoice], style);
		}
	}

	private void OnTriggerStay2D (Collider2D aOther)
	{
		if (aOther.gameObject.CompareTag ("Player"))
		{
			if (Input.GetKeyDown (KeyCode.E))
			{
				myIsTalking = true;
				myIsFirstTalk = true;	
			}
		}
	}

	private void OnTriggerExit2D (Collider2D aOther)
	{
		if (myIsTalking == true && aOther.gameObject.CompareTag ("Player"))
		{
			myIsTalking = false;
			GUI.enabled = false;
			myIsFirstTalk = true;	
		}
	}

	#endregion
}
