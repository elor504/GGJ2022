using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public PlayerType targetType;
	public Color humanSafeColor, shadowSafeColor;
	public SpriteRenderer spriteRenderer;

	private void Awake()
	{
		InitObstacle(PlayerType.Shadow);
	}
	public void InitObstacle(PlayerType _targetType)
	{
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
			}
		}
	}
}
public enum PlayerType
{
Human,
Shadow
}
