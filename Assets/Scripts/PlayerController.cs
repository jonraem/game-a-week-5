using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;

	public GameObject bullet;

	public bool playerDead = false;

	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxisRaw ("Horizontal");
		transform.Translate (new Vector2(speed*(move * Time.deltaTime), 0));
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(0);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate(bullet, (new Vector2(transform.position.x, transform.position.y + 0.35f)), Quaternion.identity);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Die ();
		}
	}

	void Die()
	{	
		Destroy (gameObject);
		playerDead = true;
	}
}
