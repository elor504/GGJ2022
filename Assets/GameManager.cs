using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager getInstance => _instance;

	private ObstacleManager obstacleManager;


	public Transform playerOne, playerTwo;



	public bool secondStage;

	[Header("Stage Test")]
	[SerializeField] List<StageSO> stages = new List<StageSO>();
	[SerializeField] int stageIndex = 0;

	public StageUI stageUI;

	private float points;
	public float getPoints => points;




	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);
		obstacleManager = GetComponent<ObstacleManager>();
		stageUI.SetStageText();
		ResetAllStages();
		ResetState();
	}
	// Update is called once per frame
	void Update()
	{
		points += Time.deltaTime;
		CurrentStageBehaviour();
	}


	#region Stages
	void ResetState()
	{
		stages[0].StartStage();
		stageIndex = 0;
		stages[0].StartSpawning();
	}
	void ResetAllStages()
	{
		for (int i = 0; i < stages.Count; i++)
		{
			//stages[i].StartStage();
		}
	}
	public void EnterNewStage()
	{
		if (stageIndex + 1 < stages.Count - 1)
		{
			stages[stageIndex + 1].StartStage();
			//stages[stageIndex + 1].StartSpawning();
		}

		stageIndex++;

		if (stageIndex > stages.Count - 1)
		{
			stageIndex = 0;
			GetCurrentStage().StartStage();
			GetCurrentStage().StartSpawning();
		}
		stageUI.SetStageText();
	}
	public void CurrentStageBehaviour()
	{
		GetCurrentStage().StageBehaviour();
	}
	StageSO GetCurrentStage()
	{
		return stages[stageIndex];
	}
	public float getStageCounter()
	{
		return GetCurrentStage().getStageTime;
	}
	public string GetStageName(){
		return GetCurrentStage().getStageName;
	}

	#endregion

	public void StartCourentine(IEnumerator courentine)
	{
		StartCoroutine(courentine);
	}

	public void RestartGame()
	{
		//reset the players position
		playerOne.GetComponent<BasicPlayerMovement>().RestartPlayer();
		playerTwo.GetComponent<BasicPlayerMovement>().RestartPlayer();

		//reset the obstacles
		obstacleManager.ResetObstacles();

		//reset Stages
		ResetState();

		//reset the distance bool
		stageUI.SetStageText();

		//reset the points
		points = 0;

		//reset the stage
		//stageType = StageTypes.FreeRoam;
	}







}

