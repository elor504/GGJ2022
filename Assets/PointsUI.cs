using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsUI : MonoBehaviour
{
	public TextMeshProUGUI pointText;
	private void FixedUpdate()
	{
		pointText.text = "Points: " + Mathf.RoundToInt(GameManager.getInstance.getPoints).ToString();
	}
}
