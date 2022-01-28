using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
	public float minX, maxX;
	public float minY, maxY;
	public Obstacle obstaclePF;


	public float spawnMinCounter, spawnMaxCounter;
	public float spawnCounter;

	private bool isActive;
	public bool getIsActive => isActive;
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
				SpawnObstacle();
				RestartCounter();
			}
		}
	}
	public void SetObstacleManagerActive(bool _isActive){
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
		Obstacle spawnedObstacle = Instantiate(obstaclePF, new Vector2(spawnXPos, spawnYPos), Quaternion.identity);
		int typeRandom = Random.Range(0, 2);
		if(typeRandom == 0){
			spawnedObstacle.InitObstacle(PlayerType.Human);
		}
		else if(typeRandom == 1){
			spawnedObstacle.InitObstacle(PlayerType.Shadow);
		}
		
	}
}
