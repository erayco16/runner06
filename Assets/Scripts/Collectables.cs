using UnityEngine;
using DG.Tweening;

public class Collectables : MonoBehaviour
{
    public CollectablesEnum collectablesEnum;
    public int toBeAddedHealth;
    public int toBeAddedScore;
    public int toBeAddedSpeed;
    public GameObject player;

    private void Start()
    {
        if (collectablesEnum == CollectablesEnum.Coin)
        {
            player = GameObject.FindFirstObjectByType<PlayerController>().gameObject;
        }
    }
    private void Update()
    {
        if (collectablesEnum == CollectablesEnum.Coin && player.GetComponent<PlayerController>().isMagnetActive)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < 8)
            {
                transform.DOMove(player.transform.position + new Vector3(0, 1, 0), 0.35f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }
}
