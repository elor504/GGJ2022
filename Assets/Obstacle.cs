using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	Rigidbody2D rb;


	public PlayerType targetType;
	public Color humanSafeColor, shadowSafeColor;
	public SpriteRenderer spriteRenderer;
	public float distanceToStayAlive;

	public float normalGravity = 0.5f;
	public float freeRoamGravity = 0.7f;
	float yStartPos;
	
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		//InitObstacle(PlayerType.Shadow,);
	}
	private void FixedUpdate()
	{
		float distance = (yStartPos - this.transform.position.y);
		//to prevent negative distance
		if(distance < 0){
			distance *= -1;
		}
		if(distance >= distanceToStayAlive){
			this.gameObject.SetActive(false);
		}
	}
	public void InitObstacle(PlayerType _targetType,StageTypes _stageType)
	{
		switch (_stageType)
		{
			case StageTypes.FreeRoam:
				rb.gravityScale = freeRoamGravity;
				break;
			case StageTypes.KeepDistance:
				rb.gravityScale = normalGravity;
				break;
		}

		yStartPos = this.transform.position.y;
		targetType = _targetType;
		SetColorByType();
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

	private void OnTriggerEnter2D(Collider2D collision)
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
