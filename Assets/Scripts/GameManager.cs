using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GUIStyle scoreScreenStyle;
	public GUIStyle scoreStyle;

	public PlayerController player;
	public EnemyGrid enemies;
	//GameObject player;
	//GameObject enemies;

	public int currentLevel;

	bool gameEnd = false;
	int gameScore;
	int highscore;

	bool showScore = false;
	bool showEndScreen = false;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		currentLevel = 1;
	}

	void OnLevelWasLoaded(int level)
	{
		player = player.GetComponent<PlayerController> ();
		enemies = enemies.GetComponent<EnemyGrid> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!gameEnd)
		{
			CalculateScore ();
			
			// This is more of a MVC way to check for game end condition than a function call from Player or EnemyGrid.
			// However, the DontDestroyOnLoad statement makes this troublesome because in the main menu the Game Manager 
			// in the background doesn't find the references to Player  and EnemyGrid.
			if (enemies.enemiesDead == true)
			{
				gameEnd = true;
			}
			
			if (player.playerDead == true)
			{
				gameEnd = true;
			}
		}
	}

	void Reinitialize()
	{
		if (gameEnd)
		{
			//player = Object.FindObjectOfType(typeof(PlayerController)) as GameObject;
			//enemies = Object.FindObjectOfType (typeof(EnemyGrid)) as GameObject;
			gameEnd = false;
			gameScore = 0;
			showScore = false;
			showEndScreen = false;
		}
	}

	IEnumerator EndGame()
	{
		yield return new WaitForSeconds(1);
		if (player.playerDead == false)
		{
			if (currentLevel < 1)
			{
				if (gameEnd == true)
				{
					currentLevel++;
				}

				Application.LoadLevel(currentLevel);
				
				Reinitialize();
			} else {
				showScore = true;
				yield return new WaitForSeconds(1);
				showEndScreen = true;
			}
		}

		if (player.playerDead == true)
		{
			showScore = true;
			yield return new WaitForSeconds(1);
			Application.LoadLevel (0);

			Reinitialize();
		}
	}

	void CalculateScore()
	{
		gameScore = ((42 - enemies.gameObject.transform.childCount) * 200);

		if (highscore < gameScore)
		{
			highscore = gameScore;
		}
	}

	void OnGUI()
	{
		if (gameEnd == true && player.playerDead == false)
		{
			GUI.Label (new Rect(100, 100, 200, 200), "Level complete!", scoreScreenStyle);
			StartCoroutine(EndGame ());
		}

		if (gameEnd == true && player.playerDead == true)
		{
			highscore = gameScore;
			GUI.Label (new Rect(100, 100, 200, 200), "You died!", scoreScreenStyle);
			StartCoroutine(EndGame ());
		}

		if (showScore == true && gameEnd == true)
		{
			GUI.Label (new Rect(100, 150, 200, 200), "Your score was: " + gameScore + " / 8400", scoreScreenStyle);
		}

		if (showEndScreen == true && gameEnd == true)
		{
			GUI.Label (new Rect(100, 200, 200, 200), "Thanks for playing!", scoreScreenStyle);
		}
	}
}
