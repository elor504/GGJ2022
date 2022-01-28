using TMPro;
using UnityEngine;
public class StageUI : MonoBehaviour
{
	public TextMeshProUGUI stageText;

	public void SetStageText(bool _secondStage)
	{
		if (_secondStage)
			stageText.text = "Stay Near each other!!!";
		else
			stageText.text = "Free Roam";
	}
}
