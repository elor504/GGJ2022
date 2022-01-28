using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FreeRoamSO", menuName = "Stage/FreeRoamSO")]
public class FreeRoamStageSO : StageSO
{



	public override void StageBehaviour()
	{
		StageTimer();
	}
}
