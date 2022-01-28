using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICounter : MonoBehaviour
{
	public TextMeshProUGUI counterText;
	private void FixedUpdate()
	{
		counterText.text = Mathf.RoundToInt(GameManager.getInstance.getStageCounter).ToString();
	}
}
