using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    public static ObstacleObjectPool staticObstacle;

    void Awake()
    {
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();

        staticObstacle = this;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int randomType = Random.Range(0, 3);
            CreateObstacle(randomType);
            if (i % 20 == 0)
            {
                yield return null;
            }
        }
    }

    public void CreateObstacle(int obstacleType)
    {
        GameObject go = null;
        if (obstacleType == 0)
        {
            go = Instantiate(obstacleBarrelPrefab);
            obstacleBarrelPool.Add(go);
        }
        else if (obstacleType == 1)
        {
            go = Instantiate(obstacleBarrierPrefab);
            obstacleBarrierPool.Add(go);
        }
        else if (obstacleType == 2)
        {
            go = Instantiate(obstacleStoneWallPrefab);
            obstacleStoneWallPool.Add(go);
        }
        go.SetActive(false);
    }

    public GameObject Acquire(int obstacleType)
    {
        if (obstacleType < 0 || obstacleType > 2)
        {
            obstacleType = Random.Range(0, 3);
        }

        List<GameObject> pool = null;
        if (obstacleType == 0) pool = obstacleBarrelPool;
        else if (obstacleType == 1) pool = obstacleBarrierPool;
        else if (obstacleType == 2) pool = obstacleStoneWallPool;

        if (pool.Count == 0)
        {
            CreateObstacle(obstacleType);
        }

        GameObject go = pool[0];
        pool.RemoveAt(0);
        go.SetActive(true);
        return go;
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        obstacle.SetActive(false);

        if (obstacleType == 0) obstacleBarrelPool.Add(obstacle);
        else if (obstacleType == 1) obstacleBarrierPool.Add(obstacle);
        else if (obstacleType == 2) obstacleStoneWallPool.Add(obstacle);
    }
}
