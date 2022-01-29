using TMPro;
using UnityEngine;

public class UICounter : MonoBehaviour
{
	private float alpha;
	public float fadeIn = 0.05f;
	public float fadeOut = 0.05f;

	public float maxAlpha = 1;
	public float minAlpha = 0;

	bool show;
	public Color alphaColor;
	public TextMeshProUGUI counterText;
	private void Awake()
	{
		ShowText(false);
	}
	private void Update()
	{
		counterText.text = Mathf.RoundToInt(GameManager.getInstance.getStageCounter()).ToString();

		if (show)
		{
			if (alphaColor.a < maxAlpha)
			{
				alphaColor.a += fadeOut;
				counterText.color = alphaColor;
			}
		}
		else
		{
			if (alphaColor.a > minAlpha)
			{
				alphaColor.a -= fadeIn;
				counterText.color = alphaColor;
			}else
			{
				
			}
		}

	}

	public void ShowText(bool _show)
	{
		show = _show;
	}

}
