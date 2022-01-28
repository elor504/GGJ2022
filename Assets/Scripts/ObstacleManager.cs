using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
	private static ObstacleManager _instance;
	public static ObstacleManager getInstance => _instance;

	public Transform obstacleParent;

	public float minX, maxX;
	public float minY, maxY;


	private bool isActive;
	public bool getIsActive => isActive;

	List<Obstacle> obstaclePool = new List<Obstacle>();

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else if (_instance != this)
		{
			Destroy(this.gameObject);
		}
	}
	private void Update()
	{

	}

	public void SpawnObstacle(Obstacle obstaclePF,Vector2 spawnPos)
	{

		if (obstaclePool.Count == 0)
		{
			Obstacle spawnedObstacle = Instantiate(obstaclePF, spawnPos, Quaternion.identity, obstacleParent);
			InitSpawnedObstacle(spawnedObstacle);
			obstaclePool.Add(spawnedObstacle);
		}
		else
		{
			bool canUseExisted = false;
			for (int i = 0; i < obstaclePool.Count; i++)
			{
				if (!obstaclePool[i].gameObject.activeInHierarchy && obstaclePF.obstacleType == obstaclePool[i].obstacleType)
				{
					Recycle(obstaclePool[i], spawnPos);
					canUseExisted = true;
					break;
				}
			}
			if (!canUseExisted)
			{
				Obstacle spawnedObstacle = Instantiate(obstaclePF, spawnPos, Quaternion.identity, obstacleParent);
				InitSpawnedObstacle(spawnedObstacle);
				obstaclePool.Add(spawnedObstacle);
			}
		}
	}
	void Recycle(Obstacle spawnedObstacle, Vector2 _pos)
	{
		spawnedObstacle.transform.position = new Vector2(_pos.x, _pos.y);
		InitSpawnedObstacle(spawnedObstacle);
		spawnedObstacle.gameObject.SetActive(true);
	}
	public void InitSpawnedObstacle(Obstacle spawnedObstacle)
	{
		int typeRandom = Random.Range(0, 2);
		if (typeRandom == 0)
		{
			spawnedObstacle.InitObstacle(PlayerType.Human);
		}
		else if (typeRandom == 1)
		{
			spawnedObstacle.InitObstacle(PlayerType.Shadow);
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
