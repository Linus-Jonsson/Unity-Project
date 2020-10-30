using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] GameObject pauseLabel;
    GameObject player;
    bool gameIsPaused;

    void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        pauseLabel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
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
        player.SetActive(false);
        Time.timeScale = 0;
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        player.SetActive(false);
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        if (gameIsPaused) {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<PlayerFootstep>().enabled = false;
            pauseLabel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<PlayerFootstep>().enabled = true;
            pauseLabel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
