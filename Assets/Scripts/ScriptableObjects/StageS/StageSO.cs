using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "StageSO", menuName = "Stage/StageSO")]
public class StageSO : ScriptableObject
{
	[SerializeField] string stageName;
	public string getStageName => stageName;

	[SerializeField] List<ObstacleHolder> obstacleHolder = new List<ObstacleHolder>();
	[SerializeField] int minSpawnAmount;
	[SerializeField] int maxSpawnAmount;
	[SerializeField] float minSpawnTimer;
	[SerializeField] float maxSpawnTimer;
	[SerializeField] int stageMinTime, stageMaxTime;
	[SerializeField] float obstacleGravity;
	float spawnCounter;
	float stageTime;
	public float getStageTime => stageTime;
	public float getSpawnCounter => spawnCounter;
	[SerializeField] Difficulties difficult;
	public Difficulties getDifficult => difficult;

	bool isActive;
	public bool getIsActive => isActive;

	Coroutine StageSpawner;
	
	UICounter uiCounter;
	public int timeToShow;

	public void StartStage()
	{
	if(StageSpawner == null)
			StageSpawner = GameManager.getInstance.StartCoroutine(Spawn());
		isActive = true;
		ResetStageTime();
		RestartTimer();
	}
	public void StartSpawning()
	{
		isActive = true;
		if (StageSpawner != null)
			StageSpawner = GameManager.getInstance.StartCoroutine(Spawn());
		//GameManager.getInstance.StartCoroutine(StageSpawner);
	}
	void ResetStageTime()
	{
		stageTime = Random.Range(stageMinTime, stageMaxTime + 1);
	}
	IEnumerator Spawn()
	{
		float spawnTime = Random.Range(minSpawnTimer, maxSpawnTimer + 1);
		spawnCounter = spawnTime;
		List<ObstacleHolder> obstacleHolder = GetRandomObstacles();
		List<Obstacle> obstaclesToSpawn = new List<Obstacle>();
		for (int i = 0; i < obstacleHolder.Count; i++)
		{
			obstaclesToSpawn.Add(obstacleHolder[i].getObstacle);
		}
		
		List<Vector2> spawnPositions = GetRandomSpawnPositions(obstaclesToSpawn);

		yield return new WaitForSeconds(spawnCounter);
		if (this.isActive)
		{
			//InitObstacles();
			bool requirefix = true;
			bool canSpawn = false;
			while (requirefix)
			{
				Debug.Log("Amount to spawn: " + spawnPositions.Count);
				int amount = 0;
				if (spawnPositions.Count > 1)
				{
					for (int i = 0; i < spawnPositions.Count; i++)
					{
						float firstMinX = spawnPositions[i].x - obstaclesToSpawn[i].GetComponent<BoxCollider2D>().bounds.min.x - 0.5f;
						float firstMaxX = spawnPositions[i].x + obstaclesToSpawn[i].GetComponent<BoxCollider2D>().bounds.max.x + 0.5f;
						float secondMinX;
						float secondMaxX;
						if ((i + 1) == spawnPositions.Count)
						{
							canSpawn = true;
							requirefix = false;
							break;
						}
						else
						{
							secondMinX = spawnPositions[i + 1].x - obstaclesToSpawn[i + 1].GetComponent<BoxCollider2D>().bounds.min.x - 0.5f;
							secondMaxX = spawnPositions[i + 1].x + obstaclesToSpawn[i + 1].GetComponent<BoxCollider2D>().bounds.max.x + 0.5f;
						}
						
						if (firstMinX > secondMaxX && firstMaxX > secondMaxX)
						{
							amount++;
					
						}
						else if (firstMaxX < secondMinX && firstMinX < secondMinX)
						{
							amount++;
						
						}
						else
						{
					
							spawnPositions[i + 1] = GetRandomSpawnPos();
						}
					}
					if (amount == spawnPositions.Count)
					{
						canSpawn = true;
						requirefix = false;
						break;
					}
				}
				else
				{
				
					requirefix = false;
					canSpawn = true;
				}
				yield return null;
			}
			if (canSpawn)
			{
				for (int i = 0; i < obstaclesToSpawn.Count; i++)
				{

					ObstacleManager.getInstance.SpawnObstacle(obstaclesToSpawn[i], obstacleHolder[i].movementType, spawnPositions[i],obstacleGravity);
				}
			}

			GameManager.getInstance.StopCoroutine(StageSpawner);
			StageSpawner = GameManager.getInstance.StartCoroutine(Spawn());
		}
	}
	public void StageTimer()
	{
		if (isActive)
		{
		if(uiCounter == null)
				uiCounter = GameObject.Find("UIManager").GetComponent<UICounter>();
			if (stageTime > 0)
			{
				stageTime -= Time.deltaTime;

				if(stageTime < timeToShow)
				{
					uiCounter.ShowText(true);
				}
			}
			else
			{
				uiCounter.ShowText(false);
				ExitStage();
			}
		}
		else
		{
			StartStage();
		}
	}
	public void PauseStage()
	{
		isActive = false;
	}
	public void ExitStage()
	{
		GameManager.getInstance.EnterNewStage();
		isActive = false;
		GameManager.getInstance.StopCoroutine(StageSpawner);
	}

	public void DeactivateStage()
	{
		isActive = false;
		GameManager.getInstance.StopCoroutine(StageSpawner);
	}
	void RestartTimer()
	{
		float spawnTime = Random.Range(minSpawnTimer, maxSpawnTimer + 1);
		spawnCounter = spawnTime;
	}
	public void InitObstacles()
	{
		
	}

	IEnumerator Test(List<Obstacle> obstaclesToSpawn, List<Vector2> spawnPositions)
	{
		bool requirefix = true;
		bool canSpawn = false;
		while (requirefix)
		{

			int amount = 0;
			for (int i = 1; i < spawnPositions.Count; i++)
			{
				float firstMinX = spawnPositions[i - 1].x - obstaclesToSpawn[i - 1].GetComponent<BoxCollider2D>().bounds.min.x;
				float firstMaxX = spawnPositions[i - 1].x + obstaclesToSpawn[i - 1].GetComponent<BoxCollider2D>().bounds.max.x;

				float secondMinX = spawnPositions[i].x - obstaclesToSpawn[i].GetComponent<BoxCollider2D>().bounds.min.x;
				float secondMaxX = spawnPositions[i].x + obstaclesToSpawn[i].GetComponent<BoxCollider2D>().bounds.max.x;

				//if (firstMinX > secondMaxX || firstMaxX > secondMinX)
				//{
				//	Debug.Log("Fixing Position");
				//	spawnPositions[i] = GetRandomSpawnPos();
				//}
				Debug.Log("First position Min and Max: " + firstMinX + " " + firstMaxX + "Second position Min and Max: " + secondMinX + " " + secondMaxX);
				if (firstMinX < secondMaxX || firstMaxX > secondMinX)
				{
					Debug.Log("Fixing Position");
					spawnPositions[i] = GetRandomSpawnPos();
				}
				else
				{
					amount++;
					Debug.Log("correctedAmount: " + amount);
				}
			}
			if (amount == spawnPositions.Count)
			{
				canSpawn = true;
				requirefix = false;
			}
			yield return new WaitForSeconds(0.001f);
		}
		if (canSpawn)
		{
			Debug.Log("Spawn");
			for (int i = 0; i < obstaclesToSpawn.Count; i++)
			{
				//ObstacleManager.getInstance.SpawnObstacle(obstaclesToSpawn[i], spawnPositions[i], obstacleGravity,);
			}
		}

	}
	Vector2 GetRandomSpawnPos()
	{
		float spawnXPos = Random.Range(ObstacleManager.getInstance.minX, ObstacleManager.getInstance.maxX + 1);
		float spawnYPos = Random.Range(ObstacleManager.getInstance.minY, ObstacleManager.getInstance.maxY + 1);
		Vector2 spawnPos = new Vector2(spawnXPos, spawnYPos);
		return spawnPos;
	}
	List<Vector2> GetRandomSpawnPositions(List<Obstacle> obstaclesToSpawn)
	{
		List<Vector2> spawnPositions = new List<Vector2>();
		for (int i = 0; i < obstaclesToSpawn.Count; i++)
		{
			spawnPositions.Add(GetRandomSpawnPos());
		}



		return spawnPositions;
	}
	List<ObstacleHolder> GetRandomObstacles()
	{
		List<ObstacleHolder> obstaclesToSpawn = new List<ObstacleHolder>();
		int amountToSpawn = Random.Range(minSpawnAmount, maxSpawnAmount + 1);
		for (int i = 0; i < amountToSpawn; i++)
		{
			int spawnIndex = Random.Range(0, obstacleHolder.Count);
			obstaclesToSpawn.Add(obstacleHolder[spawnIndex]);
		}

		return obstaclesToSpawn;
	}
	public virtual void StageBehaviour()
	{
		Debug.Log("Not Implemented Stage");
	}
}

[Serializable]
public class ObstacleHolder
{
	[SerializeField] Obstacle obstacle;
	public Obstacle getObstacle => obstacle;
	public MovementTypes movementType;
	[SerializeField] float obstacleSpawnChange;
}
public enum StageTypes
{
	FreeRoam,
	KeepDistance
}

public enum Difficulties
{
	Easy,
	Normal,
	Hard
}