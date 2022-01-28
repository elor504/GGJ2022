using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
	public Transform obstacleParent;

	public float minX, maxX;
	public float minY, maxY;
	public Obstacle obstaclePF;
	public Obstacle littleObstaclePF;

	public float spawnMinCounter, spawnMaxCounter;
	public float spawnCounter;

	private bool isActive;
	public bool getIsActive => isActive;

	List<Obstacle> obstaclePool = new List<Obstacle>();
	List<Obstacle> littleObstaclePool = new List<Obstacle>();

	private void Awake()
	{
		RestartCounter();
		isActive = true;
	}
	private void Update()
	{
		if (isActive)
		{
			if (spawnCounter > 0)
			{
				spawnCounter -= Time.deltaTime;
			}
			else
			{
				if (GameManager.getInstance.stageType == StageTypes.FreeRoam)
					SpawnLittleObstacle();
				else
					SpawnObstacle();
				RestartCounter();
			}
		}
	}
	public void SetObstacleManagerActive(bool _isActive)
	{
		isActive = _isActive;
		RestartCounter();
	}
	void RestartCounter()
	{
		float newTime = Random.Range(spawnMinCounter, spawnMaxCounter);
		spawnCounter = newTime;
	}
	void SpawnObstacle()
	{
		float spawnXPos = Random.Range(minX, maxX + 1);
		float spawnYPos = Random.Range(minY, maxY + 1);

		if (obstaclePool.Count == 0)
		{
			Obstacle spawnedObstacle = Instantiate(obstaclePF, new Vector2(spawnXPos, spawnYPos), Quaternion.identity, obstacleParent);
			InitSpawnedObstacle(spawnedObstacle);
			obstaclePool.Add(spawnedObstacle);
		}
		else
		{
			bool canUseExisted = false;
			for (int i = 0; i < obstaclePool.Count; i++)
			{
				if (!obstaclePool[i].gameObject.activeInHierarchy)
				{
					obstaclePool[i].transform.position = new Vector2(spawnXPos, spawnYPos);
					InitSpawnedObstacle(obstaclePool[i]);
					obstaclePool[i].gameObject.SetActive(true);
					canUseExisted = true;
					break;
				}
			}
			if (!canUseExisted)
			{
				Obstacle spawnedObstacle = Instantiate(obstaclePF, new Vector2(spawnXPos, spawnYPos), Quaternion.identity, obstacleParent);
				InitSpawnedObstacle(spawnedObstacle);
				obstaclePool.Add(spawnedObstacle);
			}
		}
	}
	void SpawnLittleObstacle()
	{
		float spawnXPos = Random.Range(minX, maxX + 1);
		float spawnYPos = Random.Range(minY, maxY + 1);

		if (littleObstaclePool.Count == 0)
		{
			Obstacle spawnedObstacle = Instantiate(littleObstaclePF, new Vector2(spawnXPos, spawnYPos), Quaternion.identity, obstacleParent);
			InitSpawnedObstacle(spawnedObstacle);
			littleObstaclePool.Add(spawnedObstacle);
		}
		else
		{
			bool canUseExisted = false;
			for (int i = 0; i < littleObstaclePool.Count; i++)
			{
				if (!littleObstaclePool[i].gameObject.activeInHierarchy)
				{
					littleObstaclePool[i].transform.position = new Vector2(spawnXPos, spawnYPos);
					InitSpawnedObstacle(littleObstaclePool[i]);
					littleObstaclePool[i].gameObject.SetActive(true);
					canUseExisted = true;
					break;
				}
			}
			if (!canUseExisted)
			{
				Obstacle spawnedObstacle = Instantiate(littleObstaclePF, new Vector2(spawnXPos, spawnYPos), Quaternion.identity, obstacleParent);
				InitSpawnedObstacle(spawnedObstacle);
				littleObstaclePool.Add(spawnedObstacle);
			}
		}




	}
	public void InitSpawnedObstacle(Obstacle spawnedObstacle)
	{
		int typeRandom = Random.Range(0, 2);
		if (typeRandom == 0)
		{
			spawnedObstacle.InitObstacle(PlayerType.Human, GameManager.getInstance.stageType);
		}
		else if (typeRandom == 1)
		{
			spawnedObstacle.InitObstacle(PlayerType.Shadow, GameManager.getInstance.stageType);
		}
	}

	public void ResetObstacles()
	{
		for (int i = 0; i < obstaclePool.Count; i++)
		{
			obstaclePool[i].gameObject.SetActive(false);
		}
	}
}
