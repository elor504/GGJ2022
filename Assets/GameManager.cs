using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager getInstance => _instance;

	private ObstacleManager obstacleManager;


	public Transform playerOne, playerTwo;

	public GameObject ResultScreen;
	public WhoLostUI whoLost;


	public bool secondStage;

	[Header("Stage Test")]
	[SerializeField] List<StageSO> stages = new List<StageSO>();
	[SerializeField] int stageIndex = 0;

	public StageUI stageUI;

	private float points;
	public float getPoints => points;

	bool lastInput; //false = first player, true = second player


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
		if (stageIndex + 1 < stages.Count)
		{
			stages[stageIndex + 1].StartStage();
			stages[stageIndex + 1].StartSpawning();
		}

		stageIndex++;

		if (stageIndex > stages.Count - 1)
		{
			stageIndex = 0;
			GetCurrentStage().StartStage();
			GetCurrentStage().StartSpawning();
		}
		AudioSettings.ASInstance.StateChangeSound();
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


	public void UponLosing(string _description)
	{
		whoLost.UpdateLoseText(_description);
		// Freeze time
		Time.timeScale = 0f;
		ResultScreen.SetActive(true);

		GetCurrentStage().DeactivateStage();
		//ResetGame();
	}
	public void ResetGame()
	{
		//reset the players position
		playerOne.GetComponent<BasicPlayerMovement>().RestartPlayer();
		playerTwo.GetComponent<BasicPlayerMovement>().RestartPlayer();

		GetCurrentStage().DeactivateStage();

		//reset the obstacles
		obstacleManager.ResetObstacles();

		//reset Stages
		ResetState();

		//reset the distance bool
		stageUI.SetStageText();

		//reset the points
		points = 0;
	}
	public void AddPoints(int _amount)
	{
		points += _amount;
	}
	public void WhohadLastInput(bool _who)
	{
		lastInput = _who;
	}
}

