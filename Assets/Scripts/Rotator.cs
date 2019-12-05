using UnityEngine;

public class Rotator : MonoBehaviour {
	public float speed;
	
	void FixedUpdate() {
		transform.Rotate(0f, 0f, speed);
	}
}