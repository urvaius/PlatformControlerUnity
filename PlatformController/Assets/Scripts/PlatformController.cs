﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlatformController : RaycastController
{
	public LayerMask passengerMask;
	public Vector3 move;
	// Use this for initialization
	public override void Start ()
	{
		base.Start();

	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateRaycastOrigins();
		Vector3 velocity = move * Time.deltaTime;
		MovePassengers(velocity);
		transform.Translate(velocity);

	}

	void MovePassengers(Vector3 velocity)
	{
		HashSet<Transform> movedPassengers = new HashSet<Transform>();
		float directionX = Mathf.Sign(velocity.x);
		float directionY = Mathf.Sign(velocity.y);

		// Vertically moving platform
		if (velocity.y != 0)
		{
			float rayLength = Mathf.Abs(velocity.y) + skinWidth;

			for (int i = 0; i < verticalRayCount; i++)
			{
				Vector2 rayOrigin = (directionY == -1 ? raycastOrigins.bottomLeft : raycastOrigins.topLeft);
				rayOrigin += Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

				if(hit)
				{
					if(!movedPassengers.Contains(hit.transform))
					{
						movedPassengers.Add(hit.transform);
						float pushX = (directionY == 1) ? velocity.x : 0;
						float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

						hit.transform.Translate(new Vector3(pushX, pushY));
					}
					
				}

			}

		}

		}
}
