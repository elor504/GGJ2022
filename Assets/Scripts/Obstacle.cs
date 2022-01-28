using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	Rigidbody2D rb;

	public ObstacleType obstacleType;
	public PlayerType targetType;
	public Color humanSafeColor, shadowSafeColor;
	public SpriteRenderer spriteRenderer;
	public float distanceToStayAlive;

	public float fallSpeed = 0.5f;

	float yStartPos;
	
	

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	public virtual void FixedUpdate()
	{
		ObstacleFallBehaviour();

		float distance = (yStartPos - this.transform.position.y);
		//to prevent negative distance
		if(distance < 0){
			distance *= -1;
		}
		if(distance >= distanceToStayAlive){
			this.gameObject.SetActive(false);
		}
	}
	public void InitObstacle(PlayerType _targetType)
	{
		yStartPos = this.transform.position.y;
		targetType = _targetType;
		SetColorByType();
	}

	public virtual void ObstacleFallBehaviour()
	{
		rb.velocity = new Vector2(0, -fallSpeed);
	}


	void SetColorByType()
	{
		switch (targetType)
		{
			case PlayerType.Human:
				spriteRenderer.color = humanSafeColor;
				break;
			case PlayerType.Shadow:
				spriteRenderer.color = shadowSafeColor;
				break;
		}
	}

	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			BasicPlayerMovement hitPlayer = collision.GetComponent<BasicPlayerMovement>();
			if(hitPlayer.playerType != targetType){
				Debug.Log("You Lost");
				GameManager.getInstance.RestartGame();
			}
		}
	}
}
public enum PlayerType
{
Human,
Shadow
}
public enum ObstacleType
{
normal,
little
}