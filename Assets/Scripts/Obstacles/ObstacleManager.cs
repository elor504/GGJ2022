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

	public void SpawnObstacle(Obstacle obstaclePF,MovementTypes movementTypes,Vector2 spawnPos,float _gravity)
	{

		if (obstaclePool.Count == 0)
		{
			Obstacle spawnedObstacle = Instantiate(obstaclePF, spawnPos, Quaternion.identity, obstacleParent);
			spawnedObstacle.fallSpeed = _gravity;
			InitSpawnedObstacle(spawnedObstacle, movementTypes);
			obstaclePool.Add(spawnedObstacle);
		}
		else
		{
			bool canUseExisted = false;
			for (int i = 0; i < obstaclePool.Count; i++)
			{
				if (!obstaclePool[i].gameObject.activeInHierarchy && obstaclePF.obstacleType == obstaclePool[i].obstacleType)
				{
					obstaclePool[i].fallSpeed = _gravity;
					Recycle(obstaclePool[i], spawnPos, movementTypes);
					canUseExisted = true;
					break;
				}
			}
			if (!canUseExisted)
			{
				Obstacle spawnedObstacle = Instantiate(obstaclePF, spawnPos, Quaternion.identity, obstacleParent);
				spawnedObstacle.fallSpeed = _gravity;
				InitSpawnedObstacle(spawnedObstacle, movementTypes);
				obstaclePool.Add(spawnedObstacle);
			}
		}
	}
	void Recycle(Obstacle spawnedObstacle, Vector2 _pos, MovementTypes movementTypes)
	{
		spawnedObstacle.transform.position = new Vector2(_pos.x, _pos.y);
		InitSpawnedObstacle(spawnedObstacle, movementTypes);
		spawnedObstacle.gameObject.SetActive(true);
	}
	public void InitSpawnedObstacle(Obstacle spawnedObstacle,MovementTypes movementTypes)
	{
		int typeRandom = Random.Range(0, 2);
		if (typeRandom == 0)
		{
			spawnedObstacle.InitObstacle(PlayerType.Human, movementTypes);
		}
		else if (typeRandom == 1)
		{
			spawnedObstacle.InitObstacle(PlayerType.Shadow, movementTypes);
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
