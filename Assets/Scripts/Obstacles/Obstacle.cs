using UnityEngine;

public class Obstacle : MonoBehaviour
{
	Rigidbody2D rb;
	public MovementTypes movementType;
	public ObstacleType obstacleType;
	public PlayerType targetType;
	public Color humanSafeColor, shadowSafeColor;
	public SpriteRenderer spriteRenderer;
	public float distanceToStayAlive;
	public float fallSpeed = 0.5f;

	float yStartPos;
	[Header("ZigZag")]
	public float _frequency = 1.0f;
	public float _amplitude = 5.0f;
	public float _cycleSpeed = 1.0f;

	private Vector3 pos;
	private Vector3 axis;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		pos = transform.position;
		axis = transform.right;
	}
	public virtual void FixedUpdate()
	{
		ObstacleFallBehaviour();

		float distance = (yStartPos - this.transform.position.y);
		//to prevent negative distance
		if (distance < 0)
		{
			distance *= -1;
		}
		if (distance >= distanceToStayAlive)
		{
			this.gameObject.SetActive(false);
		}
	}
	public void InitObstacle(PlayerType _targetType, MovementTypes _movementType)
	{
		pos = transform.position;
		movementType = _movementType;
		yStartPos = this.transform.position.y;
		targetType = _targetType;
		SetColorByType();
	}
	public virtual void ObstacleFallBehaviour()
	{
		switch (movementType)
		{
			case MovementTypes.Normal:
				rb.velocity = new Vector2(0, -fallSpeed);
				break;
			case MovementTypes.ZigZag:
				pos += Vector3.down * Time.deltaTime * _cycleSpeed;
				transform.position = pos + axis * Mathf.Sin(Time.time * _frequency) * _amplitude;
				break;

		}

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
		if (collision.gameObject.tag == "Player")
		{
			BasicPlayerMovement hitPlayer = collision.GetComponent<BasicPlayerMovement>();
			if (hitPlayer.playerType != targetType)
			{
				Debug.Log("You Lost");
				GameManager.getInstance.UponLosing();
				AudioSettings.ASInstance.ObstecleHit();
			}
		}
	}
}
public enum MovementTypes
{
	Normal,
	ZigZag
}
public enum PlayerType
{
	Human,
	Shadow
}
public enum ObstacleType
{
	normal,
	little,
	Collectible,
	ShadowDeadly,
	Star,
	zigzag
}