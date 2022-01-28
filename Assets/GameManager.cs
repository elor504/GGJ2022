using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager getInstance => _instance;

	private ObstacleManager obstacleManager;


	public Transform playerOne, playerTwo;

	[SerializeField] private float extraRadiusAllowed;

	public bool secondStage;

	[Header("Stage Test")]
	public StageUI stageUI;
	public float firstStageTime;
	private float stageCounter;
	public float getStageCounter => stageCounter;

	private float points;
	public float getPoints => points;

	public StageTypes stageType;



	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);
		obstacleManager = GetComponent<ObstacleManager>();
		stageCounter = firstStageTime;
		stageUI.SetStageText(secondStage);
	}
	// Update is called once per frame
	void Update()
	{
		points += Time.deltaTime;
		if (stageCounter > 0)
		{
			stageCounter -= Time.deltaTime;
		}
		else
		{
			secondStage = !secondStage;
			if (secondStage)
				stageType = StageTypes.KeepDistance;
				else
				stageType = StageTypes.FreeRoam;
			stageUI.SetStageText(secondStage);
			stageCounter = firstStageTime;
		}

		if (secondStage)
			CheckDistanceByCollider();
	}

	public void ActivateSecondStage()
	{
		//obstacleManager.SetObstacleManagerActive(false);
		secondStage = true;
	}
	void CheckDistanceByCollider()
	{
		float playerOneMinX = playerOne.GetComponent<BoxCollider2D>().bounds.min.x;
		float playerOneMaxX = playerOne.GetComponent<BoxCollider2D>().bounds.max.x;

		float playerTwoMinX = playerTwo.GetComponent<BoxCollider2D>().bounds.min.x;
		float playerTwoMaxX = playerTwo.GetComponent<BoxCollider2D>().bounds.max.x;


		if (playerOneMinX - extraRadiusAllowed > playerTwoMaxX)
		{
			GameManager.getInstance.RestartGame();
			Debug.Log("Out of distance!!!");
		}
		else if (playerOneMaxX + extraRadiusAllowed < playerTwoMinX)
		{
			GameManager.getInstance.RestartGame();
			Debug.Log("Out of distance!!!");
		}


	}
	public void RestartGame()
	{
		//reset the players position
		playerOne.GetComponent<BasicPlayerMovement>().RestartPlayer();
		playerTwo.GetComponent<BasicPlayerMovement>().RestartPlayer();

		//reset the obstacles
		obstacleManager.ResetObstacles();

		//reset counter
		stageCounter = firstStageTime;

		//reset the distance bool
		secondStage = false;
		stageUI.SetStageText(secondStage);

		//reset the points
		points = 0;

		//reset the stage
		stageType = StageTypes.FreeRoam;
	}

}

public enum StageTypes
{
FreeRoam,
KeepDistance
}
