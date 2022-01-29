using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagMovement : Obstacle
{

	public float _frequency = 1.0f;
	public float _amplitude = 5.0f;
	public float _cycleSpeed = 1.0f;

	private Vector3 pos;
	private Vector3 axis;
	private void Awake()
	{
		pos = transform.position;
		axis = transform.right;
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	public override void ObstacleFallBehaviour()
	{
		pos += Vector3.down * Time.deltaTime * _cycleSpeed;
		transform.position = pos + axis * Mathf.Sin( _frequency) * _amplitude;
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		//base.OnTriggerEnter2D(collision);
	}
}
