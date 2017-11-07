﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTurn : MonoBehaviour
{
	public Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	private void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		playerRigidbody = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void FixedUpdate()
	{
		TurningTurret();
	}
	
	private void TurningTurret()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation(newRotation);
		}
	}
}
