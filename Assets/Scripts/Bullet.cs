using UnityEngine;

public class Bullet : MonoBehaviour {
	public bool fromPlayer {get; private set;}
	Vector2 velocity;

	public void Init(bool fromPlayer, Vector3 velocity) {
		this.fromPlayer = fromPlayer;
		this.velocity = velocity;
	}

	void Update() {
		transform.position += (Vector3) velocity;
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other) {
		Destroy(gameObject);
	}
}