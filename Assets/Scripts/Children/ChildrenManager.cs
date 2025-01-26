using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class ChildrenManager : MonoBehaviour
{
	[SerializeField]
	private Transform playerTransform;
	private float spawnRadius;

	[SerializeField]
	private float spawnTime;
	[SerializeField]
	private Transform spawnPointLimitUpLeft;
	[SerializeField]
	private Transform spawnPointLimitDownRight;
	[SerializeField]
	private GameObject spawnPointPrefab;
	[SerializeField]
	[Range(0.2f, 2f)]
	private float spawnPointSpacing;
	[SerializeField]
	[Range(1, 10)]
	private int maxSpawnQuantity;

	[SerializeField]
	private int nbChildren;

	[SerializeField]
	private List<Transform> childrenSpawns = new List<Transform>();
	[SerializeField]
	private List<Transform> enabledChildrenSpawns = new List<Transform>();
	[SerializeField]
	private GameObject childPrefab;

	[SerializeField]
	private GameObject spawnPointsContainer;


	public void Awake()
	{
		GenerateSpawnPoints();
	}

	private void Start()
	{
		spawnTime = RoomManager.instance.GetSpawnTimeChildren();
		maxSpawnQuantity = RoomManager.instance.GetSpawnQuantityChildren();
		nbChildren = RoomManager.instance.GetNbChildren();

		spawnRadius = 2f;
		UpdateEnableSpawnPoints();
		StartSpawnChild();
	}

	public void Update()
	{
		UpdateEnableSpawnPoints();
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(playerTransform.position, spawnRadius);
	}

	private void GenerateSpawnPoints()
	{
		float x = spawnPointLimitUpLeft.position.x;
		float y = spawnPointLimitUpLeft.position.y;

		while (x <= spawnPointLimitDownRight.position.x)
		{
			while (y >= spawnPointLimitDownRight.position.y)
			{
				Transform spawnPoint = Instantiate(spawnPointPrefab).transform;
				childrenSpawns.Add(spawnPoint);
				spawnPoint.position = new Vector3(x, y, 0);
				spawnPoint.parent = transform;
				y -= spawnPointSpacing;
			}
			y = spawnPointLimitUpLeft.position.y;
			x += spawnPointSpacing;
		}
	}

	private void UpdateEnableSpawnPoints()
	{
		enabledChildrenSpawns = childrenSpawns.Where(x => Vector2.Distance(x.position, playerTransform.position) > spawnRadius).ToList();
	}

	public void StartSpawnChild()
	{
		InvokeRepeating("SpawnChild", 2, spawnTime);
	}

	private void SpawnChild()
	{
		int nbSpawn = UnityEngine.Random.Range(1, maxSpawnQuantity);
		if (nbChildren < nbSpawn)
			nbSpawn = nbChildren;

		for (int i = 0; i < nbSpawn; i++)
		{
			SpawnOneChild();
		}
	}

	private void SpawnOneChild()
	{
		nbChildren--;
		Vector2 spawnPos = UnityEngine.Random.insideUnitCircle * spawnRadius;
		int randomIndex = UnityEngine.Random.Range(1, enabledChildrenSpawns.Count - 1);
		Debug.Log("randomIndex: " + randomIndex);
		Debug.Log("enabledChildrenSpawns.Count: " + enabledChildrenSpawns.Count);

		Transform spawnPoint = enabledChildrenSpawns[randomIndex];
		Instantiate(childPrefab, spawnPoint.position, Quaternion.identity);
	}
}
