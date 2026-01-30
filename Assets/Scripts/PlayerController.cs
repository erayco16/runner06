using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] float speed;
    [SerializeField] float shift = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.A))
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
        */

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x>-0.5f)
        {
            transform.Translate(new Vector3(-shift, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x<0.5)
        {
            transform.Translate(new Vector3(shift, 0, 0));
        }
    }
}
