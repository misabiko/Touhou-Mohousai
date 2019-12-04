using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour {
	PlayerInput playerInput;
	Vector2 moveInput;
	bool focused;
	Coroutine shootCoroutine;
	
	public GameObject hitboxSprite;
	public GameObject bulletPrefab;

	public float speed = 5f;
	public float focusedSpeed = 3f;

	public Vector3 bulletSpawn = Vector3.up;
	public float shotRate = 0.1f;
	public float bulletSpeed = 1f;

	public VideoPlayer video;
	public float timestamp;
	
	void Awake() {
		playerInput = GetComponent<PlayerInput>();

		playerInput.actions["Move"].started += OnMove;
		playerInput.actions["Move"].performed += OnMove;
		playerInput.actions["Move"].canceled += OnMove;

		playerInput.actions["Shoot"].started += _ => shootCoroutine = StartCoroutine(Shoot());
		playerInput.actions["Shoot"].canceled += _ => StopCoroutine(shootCoroutine);
		
		playerInput.actions["Bomb"].performed += _ => Bomb();
		
		playerInput.actions["Focus"].started += _ => SetFocus(true);
		playerInput.actions["Focus"].canceled += _ => SetFocus(false);

		video.time = timestamp;
	}

	void OnMove(InputAction.CallbackContext context) {
		moveInput = context.ReadValue<Vector2>();
		moveInput.Normalize();
	}

	IEnumerator Shoot() {
		while (true) {
			SpawnBullet();
			yield return new WaitForSeconds(shotRate);
		}
	}

	void SpawnBullet() {
		GameObject bullet = Instantiate(bulletPrefab, transform.position + bulletSpawn, Quaternion.identity);
		bullet.GetComponent<Bullet>().Init(true, bulletSpeed * Vector2.up);
	}

	void Bomb() {
		Debug.Log("Bomb");
	}

	void SetFocus(bool focus) {
		focused = focus;
		hitboxSprite.SetActive(focus);
	}

	void Update() {
		transform.position += (focused ? focusedSpeed : speed) * Time.deltaTime * (Vector3) moveInput;
	}
}