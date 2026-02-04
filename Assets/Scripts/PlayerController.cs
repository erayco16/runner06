using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [Header("Settings")]
    [Tooltip("Bu deðiþken oyuncunun hýzýný belirler")]
    [SerializeField] float speed;
    [Tooltip("Bu deðiþken oyuncunun saða sola kaç metre gideceðini ayarlar")]
    [SerializeField] float shift = 2;
    bool isDead;
    [SerializeField] int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
            
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        #region Karakter Sýnýrlama
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


        if (Input.GetKeyDown(KeyCode.A) && transform.position.x>-0.5f)
        {
            //transform.Translate(new Vector3(-shift, 0, 0));
            transform.DOMoveX(transform.position.x - shift, 0.2f);
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x<0.5)
        {
            //transform.Translate(new Vector3(shift, 0, 0));
            transform.DOMoveX(transform.position.x + shift, 0.2f);
        }
        #endregion


    }
    /// <summary>
    /// ilk çarpýþtýgýmýz an
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            anim.SetBool("Death", true);
            isDead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score += 10;
            Destroy(other.gameObject);
        }
    }
}
