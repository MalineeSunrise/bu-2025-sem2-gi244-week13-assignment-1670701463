using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int leftBound = -15;

    private void OnTriggerEnter(Collider other)
    {
        if (transform.position.x < leftBound)
        {
            if (gameObject.CompareTag("Barrel"))
            {
                Debug.Log("Barrel out of bounds!");
                ObstacleObjectPool.staticObstacle.Release(gameObject, 0);
            }
            else if (gameObject.CompareTag("Barrier"))
            {
                Debug.Log("Barrier out of bounds!");
                ObstacleObjectPool.staticObstacle.Release(gameObject, 1);
            }
            else if (gameObject.CompareTag("StoneWall"))
            {
                ObstacleObjectPool.staticObstacle.Release(gameObject, 2);
            }
        }
    }
}
