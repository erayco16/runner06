using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Rigidbody rb;
    [SerializeField] public Animator anim;
    [SerializeField] AudioClip bonusSound, CoinSound, DeathSound, MagnetCoinSound, ShieldSound;
    [SerializeField] AudioSource playerSounds;
    [SerializeField] GameObject coinCollectedVFX, DeathVFX, HealthDeclineVFX, MagnetVFX, WallBreakVFX, ShieldVFX;


    [Header("Settings")]
    [Tooltip("Bu deðiþken oyuncunun hýzýný belirler")]
    [SerializeField] float speed;
    [Tooltip("Bu deðiþken oyuncunun saða sola kaç metre gideceðini ayarlar")]
    [SerializeField] float shift = 2;
    [HideInInspector] public bool isDead;
    [SerializeField] public int score;
    [HideInInspector] public bool isStart;
    [HideInInspector] public float floatScore;
    [HideInInspector] public float passedTime;
    [SerializeField] int Health;
    float beforeSpeed;

    public bool is2XActive, isShieldActive, isMagnetActive;
    bool isMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if (passedTime > 10)
        {
            speed += 0.3f;
            passedTime = 0;
        }

        if (!isStart) return;

        if (isDead) return;

        if (is2XActive)
        {
            floatScore += Time.deltaTime;
        }
        
        floatScore += Time.deltaTime;
        if (floatScore > 1)
        {
            score += 1;
            floatScore = 0;
        }

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


        if (Input.GetKeyDown(KeyCode.A) && transform.position.x>-0.5f && !isMove)
        {
            //transform.Translate(new Vector3(-shift, 0, 0));
            transform.DOMoveX(transform.position.x - shift, 0.2f).OnComplete(isMoveToFalse);
            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x<0.5 && !isMove)
        {
            //transform.Translate(new Vector3(shift, 0, 0));
            transform.DOMoveX(transform.position.x + shift, 0.2f).OnComplete(isMoveToFalse);
            isMove = true;
        }
        #endregion

    }

    void isMoveToFalse()
    {
        isMove = false;
    }


    /// <summary>
    /// ilk çarpýþtýgýmýz an
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            int damage = other.gameObject.GetComponent<Obstacle>().damage;

            if (isShieldActive)
            {
                Destroy(other.gameObject);
                isShieldActive = false;
                GameObject vfx = Instantiate(WallBreakVFX, other.transform.position, Quaternion.identity);
                Destroy(vfx, 1f);
            }
            else
            {
                CheckHealth(damage, other.gameObject);
            }

            
        }
    }
    private void CheckHealth(int damage, GameObject other)
    {
        Health -= damage;
        if (Health <= 0)
        {
            anim.SetBool("Death", true);
            playerSounds.PlayOneShot(DeathSound);
            GameObject vfx = Instantiate(DeathVFX, transform.position, Quaternion.Euler(-90, 0, 0));
            isDead = true;
        }
        else
        {
            GameObject vfx = Instantiate(WallBreakVFX, other.transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
            GameObject Health = Instantiate(HealthDeclineVFX, transform.position, Quaternion.identity, this.transform);
            Destroy(Health, 2f);
            Destroy(other.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            Collectables collectables = other.GetComponent<Collectables>();
            switch (collectables.collectablesEnum)
            {
                case CollectablesEnum.Coin:
                    AddScore(collectables.toBeAddedScore);
                    break;
                case CollectablesEnum.Shield:
                    ActivateShield();
                    break;
                case CollectablesEnum.Score2X:
                    ActivateBonus();
                    break;
                case CollectablesEnum.SpeedUp:
                    AddSpeed(collectables.toBeAddedSpeed);
                    break;
                case CollectablesEnum.Health:
                    AddHealth(collectables.toBeAddedHealth);
                    break;
                case CollectablesEnum.Magnet:
                    ActivateMagnet();
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    private void AddSpeed(int toBeAddedSpeed)
    {
        beforeSpeed = speed;
        speed += toBeAddedSpeed;
        Invoke("BackToOrijinalSpeed", 5f);
    }

    void BackToOrijinalSpeed()
    {
        speed = beforeSpeed;
    }
    void AddScore(int toBeAddedScore)
    {
        if (isMagnetActive)
        {
            playerSounds.clip = MagnetCoinSound;
            playerSounds.Play();

        }
        else
        {
            playerSounds.clip = CoinSound;
            playerSounds.Play();
        }
        GameObject vfx = Instantiate(coinCollectedVFX, transform.position + new Vector3(0, 1, 0), Quaternion.identity, this.transform);
        Destroy(vfx, 1f);
        if (is2XActive)
        {
            toBeAddedScore *= 2;
        }
        score += toBeAddedScore;
    }

    void ActivateShield()
    {
        isShieldActive = true;
        playerSounds.PlayOneShot(ShieldSound);
        GameObject vfx = Instantiate(ShieldVFX, transform.position, Quaternion.identity, this.transform);
        Destroy(vfx, 5f);
        Invoke("DeactivateShield", 5f);
    }

    void DeactivateShield()
    {
        isShieldActive = false;
    }

    void AddHealth(int toBeAddedHealth)
    {
        Health += toBeAddedHealth;
        if (Health <= 0)
        {
            anim.SetBool("Death", true);
            isDead = true;
        }
    }
    void ActivateBonus()
    {
        is2XActive = true;
        AudioSource.PlayClipAtPoint(bonusSound, transform.position);
        Invoke("DeactivateBonus", 5f);
    }

    void DeactivateBonus()
    {
        isShieldActive = false;
    }

    void ActivateMagnet()
    {
        isMagnetActive = true;
        GameObject vfx = Instantiate(MagnetVFX, this.transform.position, Quaternion.identity, this.transform);
        Destroy(vfx, 5f);
        Invoke("DeactivateMagnet", 5f);
    }

    void DeactivateMagnet()
    {
        isMagnetActive = false;
    }
}
