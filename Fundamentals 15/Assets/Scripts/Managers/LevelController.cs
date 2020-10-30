using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] GameObject pauseLabel;
    PlayerController playerController;
    bool gameIsPaused;

    void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        pauseLabel.SetActive(false);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    public void HandleWinCondition()
    {

        winLabel.SetActive(true);
        playerController.enabled = false;
        Time.timeScale = 0;
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        playerController.enabled = false;
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        if (gameIsPaused)
        {
            playerController.enabled = false;
            pauseLabel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            playerController.enabled = true;
            pauseLabel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
