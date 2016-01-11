using UnityEngine;
using System.Collections;

public class EnemyGrid : MonoBehaviour {

	public GameObject prefab;
	public int numberOfEnemies;
	public int enemiesPerRow;
	public float distanceBetweenEnemies;

	public bool enemiesDead = false;

	private int currentPosition;
	public Transform[] patrolPoints;
	public float patrolSpeed;

	// Use this for initialization
	void Start () {
		CreateTiles ();
		currentPosition = 0;
	}

	void Update()
	{
		Patrol ();

		if (gameObject.transform.childCount == 0)
		{
			enemiesDead = true;
		}
	}

	void CreateTiles()
	{
		float xOffset = 0f;
		float yOffset = 0f;

		for (int enemiesCreated = 0; enemiesCreated < numberOfEnemies; enemiesCreated++)
		{
			xOffset += distanceBetweenEnemies;

			if (enemiesCreated % enemiesPerRow == 0)
			{
				yOffset -= distanceBetweenEnemies;
				xOffset = 0;
			}

			// See the cast at the end? Take notes... without that there is an error about converting Object to GameObject
			GameObject clone = Instantiate(prefab, new Vector3(prefab.transform.position.x + xOffset, prefab.transform.position.y + yOffset, prefab.transform.position.z), transform.rotation) as GameObject;
			clone.transform.parent = this.transform;
		}
	}

	void Patrol()
	{		
		if (transform.position == patrolPoints[currentPosition].position)
		{
			currentPosition++;
		}
		
		if (currentPosition >= patrolPoints.Length)
		{
			for (int i = 0; i < patrolPoints.Length; i++)
			{
				patrolPoints[i].position = new Vector2(patrolPoints[i].position.x, patrolPoints[i].position.y - 2);
			}

			currentPosition = 0;
		}

		transform.position = Vector2.MoveTowards (transform.position,  patrolPoints [currentPosition].position, patrolSpeed * Time.deltaTime);
	}
}
