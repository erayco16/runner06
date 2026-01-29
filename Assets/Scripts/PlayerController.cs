using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Run", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("Run", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Jump", false);
        }
    }
}
