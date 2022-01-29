using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDeadlyObject : Obstacle
{
	public override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	public override void ObstacleFallBehaviour()
	{
		base.ObstacleFallBehaviour();
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			if (collision.GetComponent<BasicPlayerMovement>().playerType == PlayerType.Human)
			{
				

			}
			else if (collision.GetComponent<BasicPlayerMovement>().playerType == PlayerType.Shadow)
			{
				Debug.Log("You Lost");
				GameManager.getInstance.UponLosing("shadow");
				AudioSettings.ASInstance.ObstecleHit();
			}
		}
	}
}
