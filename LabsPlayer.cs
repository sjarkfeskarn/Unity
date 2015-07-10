using UnityEngine;
using System.Collections;

public class LabsPlayer : MonoBehaviour {

	public float moveSpeed = 5.0f;
	public float gravity = 8.0f;
	public GameObject player;
	public LayerMask rayTerrain;
	Vector3 lookPosition;
	Vector3 destPosition;
	Vector3 destDirection;
	float destDistance;
	float downforce;
	Ray ray;
	RaycastHit hit;
	CharacterController controller;
	Animator animator;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		}
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, rayTerrain)) {
			destPosition = hit.point;
			destDistance = Vector3.Distance(destPosition, player.transform.position);
			player.transform.LookAt (new Vector3 (destPosition.x, player.transform.position.y, destPosition.z));
			destDirection = destPosition - player.transform.position;
			destDirection.y = 0;
			destDirection = destDirection.normalized * moveSpeed;
			destDirection.y -= gravity * 100 * Time.deltaTime;
		}
		if (destDistance > .2f) {
			controller.Move (destDirection * Time.deltaTime);
			animator.SetFloat ("Speed", 1f);
		}
		else {
			animator.SetFloat ("Speed", 0f);
		}
	}
}
