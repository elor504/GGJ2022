using UnityEngine;
[CreateAssetMenu(fileName = "FreeRoamSO", menuName = "Stage/FreeRoamSO")]
public class FreeRoamStageSO : StageSO
{



	public override void StageBehaviour()
	{
		if (getIsActive)
			StageTimer();
	}
}
