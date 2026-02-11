using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] public GameObject gameStartMenu;
    [SerializeField] public GameObject gameRestartMenu;
    [SerializeField] public TextMeshProUGUI endScore;
    [SerializeField] public TextMeshProUGUI gameScore;

    public void StartGame()
    {
        playerController.isStart = true;
        playerController.anim.SetBool("Run", true);
        gameStartMenu.SetActive(false);
    }
    private void Start()
    {
        gameStartMenu.SetActive(true);
        gameRestartMenu.SetActive(false);
    }


    private void Update()
    {
        gameScore.text = "Score : " + playerController.score;

        if (playerController.isDead)
        {
            gameRestartMenu.SetActive(true);
            endScore.text = "Score : " + playerController.score;
        }
    }
    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
