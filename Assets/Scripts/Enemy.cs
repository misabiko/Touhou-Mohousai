using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int health;

	void OnCollisionEnter2D(Collision2D other) {
		if (!other.gameObject.GetComponent<Bullet>()) return;
		
		health--;
		if (health == 0)
			Destroy(gameObject);
	}
}