using UnityEngine;

public class CollectibleObstacle : Obstacle
{
	public int collectiblePoint;
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
		//base.OnTriggerEnter2D(collision);
		if (collision.gameObject.tag == "Player")
		{
			if (collision.GetComponent<BasicPlayerMovement>().playerType == PlayerType.Human)
			{
				GameManager.getInstance.AddPoints(collectiblePoint);
				this.gameObject.SetActive(false);
			}
			else if (collision.GetComponent<BasicPlayerMovement>().playerType == PlayerType.Shadow)
			{
				Debug.Log("You Lost");
				GameManager.getInstance.UponLosing("Demonna got hit");
				AudioSettings.ASInstance.ObstecleHit();
			}
		}
	}
}
