using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 2f);
    }

    void Spawn()
    {
        // 1.18 stop moving left when the game is over
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            return;
        }

        int randomType = Random.Range(0, 3);

        GameObject obstacle = ObstacleObjectPool.staticObstacle.Acquire(randomType);
        obstacle.transform.position = spawnPoint.position;
    }
}
