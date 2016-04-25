using UnityEngine;
using System.Collections;

namespace TestGame1
{
	using System.Collections.Generic;       

	public class GameManager : MonoBehaviour
	{

		public static GameManager instance = null;              
		private BoardManager boardScript;                      
		private int level = 3;                                 


		void Awake()
		{
			if (instance == null)

				instance = this;

			else if (instance != this)

				Destroy(gameObject);    
			

			DontDestroyOnLoad(gameObject);

			boardScript = GetComponent<BoardManager>();

			InitGame();
		}
			
		void InitGame()
		{
			boardScript.SetupScene(level);

		}


		void Update()
		{

		}
	}
}