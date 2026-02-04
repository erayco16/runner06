using UnityEngine;

public class GameManager : MonoBehaviour
{
    float roadLength = 20;
    [SerializeField] GameObject[] road;
    [SerializeField] Transform player;
    [SerializeField] Transform roadParent;

    int startRoadCount = 2;

    private void Start()
    {
        Instantiate(road[0], transform.position, Quaternion.identity, roadParent);
        for (int i = 0; i < startRoadCount; i++)
        {
            Instantiate(road[Random.Range(0, road.Length)], transform.position + new Vector3(0,0,roadLength), Quaternion.identity, roadParent);
            roadLength += 20;
        }
    }
}
