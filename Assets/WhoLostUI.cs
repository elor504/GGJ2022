using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WhoLostUI : MonoBehaviour
{
	public TextMeshProUGUI wholost;
	private void Awake()
	{
		wholost.text = "PlayerX lost the round";
	}


	public void UpdateLoseText(string _text)
	{
		wholost.text = _text;
	}

}
