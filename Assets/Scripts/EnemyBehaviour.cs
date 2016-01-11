using UnityEngine;
using System.Collections;

public class EnemyBehaviour: MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			anim.SetTrigger("Death");
			Destroy (this.gameObject, 0.35f);
		}
	}
}