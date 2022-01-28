using TMPro;
using UnityEngine;
public class StageUI : MonoBehaviour
{
	public TextMeshProUGUI stageText;


	
	public void SetStageText()
	{
		stageText.text = GameManager.getInstance.GetStageName();
	}
}
