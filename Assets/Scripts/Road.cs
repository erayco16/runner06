using UnityEngine;

public class Road : MonoBehaviour
{
    GameObject Player;
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        if ((Player.transform.position.z - this.transform.position.z) > 25)
        {
            Destroy(this.gameObject);
        }
    }
}
