using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCheck : MonoBehaviour
{


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "BigMuffin")
		{
			GameManager.getInstance.UponLosing(" didn't collected muffin");
		}
	}
}
