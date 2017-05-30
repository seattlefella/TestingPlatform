using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PingPong : MonoBehaviour
{

	[SerializeField]
	private Vector3 minOffset;
	[SerializeField]
	private Vector3 maxOffset;

	[SerializeField]
	private Vector3 moveUnitsPerSecond;
	[SerializeField]
	private Vector3 rotateUnitsPerSecond;

	// All of the critical points that we need to calculate positions.
	private Vector3 currentPos = new Vector3();
	private Vector3 startPos = new Vector3();
	private Vector3 maxPos = new Vector3();
	private Vector3 minPos = new Vector3();

	// How far we have moved against how far we have to go.
	private Vector3 length = new Vector3();
	private Vector3 distanceMoved = new Vector3();

	// the parameter t [0,1] that we use to lerp positions with the ping-pong direction indicator
	private Vector3 t = new Vector3(0,0,0);
	private Vector3 direction = new Vector3(1,1,1);


	void Start()
	{
		startPos = transform.position;
		maxPos = startPos + maxOffset;
		minPos = startPos + minOffset;

		length = maxPos - minPos;

		distanceMoved.x = -minOffset.x;
		distanceMoved.y = -minOffset.y;
		distanceMoved.z = -minOffset.z;

		direction = Vector3.one;

	}
	// Update is called once per frame
	void Update ()
	{
		distanceMoved.x = distanceMoved.x + direction.x * (Time.deltaTime * moveUnitsPerSecond.x);
		distanceMoved.y = distanceMoved.y + direction.y * (Time.deltaTime * moveUnitsPerSecond.y);
		distanceMoved.z = distanceMoved.z + direction.z * (Time.deltaTime * moveUnitsPerSecond.z);

		// Every update we must calculate the parameter t [0,1]  this is used to lerp us between the points
		calculateT();
	   
		// Every time the parameter t [0,1] leaves this range we must flip the direction of movement
		calculateDirection();

		// Lets actualy change the position of our gameObject
		currentPos.x = Mathf.Lerp(minPos.x, maxPos.x, t.x);
		currentPos.y = Mathf.Lerp(minPos.y, maxPos.y, t.y);
		currentPos.z = Mathf.Lerp(minPos.z, maxPos.z, t.z);

		transform.position = currentPos;

		transform.Rotate(rotateUnitsPerSecond * Time.deltaTime, Space.World);

	}

	private void calculateT()
	{
		t.x = length.x <= 0.001f ? 0 : distanceMoved.x / length.x;
		t.y = length.y <= 0.001f ? 0 : distanceMoved.y / length.y;
		t.z = length.z <= 0.001f ? 0 : distanceMoved.z / length.z;

	}

	private void calculateDirection()
	{
		if (direction.x > 0  && t.x > 1)
		{
			direction.x = direction.x * (-1);
		}
		else if (direction.x <= 0 && t.x < 0)
		{
			direction.x = direction.x * (-1);
		}


		if (direction.y >= 0 && t.y > 1)
		{
			direction.y = direction.y * (-1);
		}
		else if (direction.y <= 0 && t.y < 0)
		{
			direction.y = direction.y * (-1);
		}

		if (direction.z >= 0 && t.z > 1)
		{
			direction.z = direction.z * (-1);
		}
		else if (direction.z <= 0 && t.z < 0)
		{
			direction.z = direction.z * (-1);
		}
	}
}