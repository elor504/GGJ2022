using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KeepDistanceSO", menuName = "Stage/KeepDistanceSO")]
public class KeepDistanceStageSO : StageSO
{
	[SerializeField] private float extraRadiusAllowed;
	public override void StageBehaviour()
	{

		if (getIsActive)
		{
			StageTimer();
			float playerOneMinX = GameManager.getInstance.playerOne.GetComponent<BoxCollider2D>().bounds.min.x;
			float playerOneMaxX = GameManager.getInstance.playerOne.GetComponent<BoxCollider2D>().bounds.max.x;

			float playerTwoMinX = GameManager.getInstance.playerTwo.GetComponent<BoxCollider2D>().bounds.min.x;
			float playerTwoMaxX = GameManager.getInstance.playerTwo.GetComponent<BoxCollider2D>().bounds.max.x;


			if (playerOneMinX - extraRadiusAllowed > playerTwoMaxX)
			{
				GameManager.getInstance.UponLosing();
				Debug.Log("Out of distance!!!");
			}
			else if (playerOneMaxX + extraRadiusAllowed < playerTwoMinX)
			{
				GameManager.getInstance.UponLosing();
				Debug.Log("Out of distance!!!");
			}
		}
	}
}
