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
	public float firstStageTime;
	private float stageCounter;




	private void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy(this.gameObject);
		obstacleManager = GetComponent<ObstacleManager>();
		stageCounter = firstStageTime;
	}
	// Update is called once per frame
	void Update()
	{
		if(stageCounter > 0)
		{
			stageCounter -= Time.deltaTime;
		}else
		{
			if(obstacleManager.getIsActive)
			{
				ActivateSecondStage();
			}
		}

		if (secondStage)
			CheckDistanceByCollider();
	}

	public void ActivateSecondStage()
	{
		obstacleManager.SetObstacleManagerActive(false);
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
			Debug.Log("Out of distance!!!");
		}
		else if (playerOneMaxX + extraRadiusAllowed < playerTwoMinX)
		{
			Debug.Log("Out of distance!!!");
		}


	}


}
