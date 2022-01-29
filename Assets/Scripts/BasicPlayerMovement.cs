using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
	public PlayerType playerType;
	Rigidbody2D rb;
	public float movementSpeed;
	public bool isSecondPlayer;

	Vector2 startPosition;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		startPosition = this.transform.position;
	}

	// Update is called once per frame
	void Update()
    {
		BasicMovement();
	}
	void BasicMovement()
	{
		if (isSecondPlayer)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				rb.velocity = new Vector2(-movementSpeed, 0);
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				rb.velocity = new Vector2(movementSpeed, 0);
			}
			else
			{
				//if (rb.velocity.x != 0)
				//	rb.velocity = Vector2.zero;
			}
		}
		else
		{
			if (Input.GetKey(KeyCode.A))
			{
				rb.velocity = new Vector2(-movementSpeed, 0);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				rb.velocity = new Vector2(movementSpeed, 0);
			}
			else
			{
				//if (rb.velocity.x != 0)
				//	rb.velocity = Vector2.zero;
			}
		}
	}
	public void RestartPlayer(){
		this.transform.position = startPosition;
	}

}
