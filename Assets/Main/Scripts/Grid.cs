using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

	public bool onlyDisplayPathGizmos;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	[Header("Debris Spawning")]
	public List<GameObject>DebrisPrefabs = new List<GameObject>();
	public int maxDebrisInScene = 1;
	public bool spawnDebris = true;
	public float maxTorque = 1f;
	public float maxForce = 1f;
	public Vector3 debrisSpawnOffset = new Vector3();

	void Start ()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
	}
	public int MaxSize
	{
		get
		{
			return gridSizeX * gridSizeY;
		}
	}
    private void Update ()
    {
		CreateGrid();
	}
	public Vector3 GetBottomLeft ()
    {
		return transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
	}
	public void CreateGrid ()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = GetBottomLeft();

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
				//bool walkable = !(Physics.SphereCast(worldPoint,nodeRadius,unwalkableMask));
				bool walkable = !(Physics2D.CircleCast(worldPoint,nodeRadius,Vector2.zero,0,unwalkableMask));
				grid[x, y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}
	public List<Node> GetNeighbours (Node node)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}
	public Node NodeFromWorldPoint (Vector3 worldPosition)
	{
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x, y];
	}
	public List<Node> path;
	public void SpawnDebris ()
    {
        if (spawnDebris)
        {
            for (int D = 0; D < maxDebrisInScene; D++)
            {
				Vector3 bottomleft = GetBottomLeft();
				Vector3 position = new Vector3(Random.Range(bottomleft.x,bottomleft.x+gridWorldSize.x),Random.Range(bottomleft.y,bottomleft.y + gridWorldSize.y));
				position += debrisSpawnOffset;
				float torque = Random.Range(0,maxTorque);
				Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value);

				GameObject debris = Instantiate(DebrisPrefabs[Random.Range(0, DebrisPrefabs.Count)],position, Quaternion.identity,GameManager.instance.activeRoom.transform);
				Rigidbody2D rbDebris = debris.GetComponent<Rigidbody2D>();
				rbDebris.AddForce(randomDirection.normalized * Random.Range(0, maxForce));
				rbDebris.AddTorque(torque);
			}
		}
    }
	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y,1));

		if (onlyDisplayPathGizmos)
		{
			if (path != null)
			{
				foreach (Node n in path)
				{
					Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
				}
			}
		}
		else
		{
			if (grid != null)
			{
				foreach (Node n in grid)
				{
					Gizmos.color = (n.walkable) ? Color.white : Color.red;
					if (path != null)
						if (path.Contains(n))
							Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
				}
			}
		}
	}
}