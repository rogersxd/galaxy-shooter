using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;

    public int score = 0;

    public Image livesImage;

    public Image title;

    public Text start;

    public Text scoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            && Time.timeScale == 0)
        {
            score = 0;
            Time.timeScale = 1;

            start.enabled = false;
            title.enabled = false;

        }
    }

    public void UpdateLives(int currentLive)
    {
        if (currentLive < 0)
        {
            GameOver();
            return;
        }

        livesImage.sprite = lives[currentLive];
    }

    public void UpdateScore(int points)
    {
        score += points;

        scoreCounter.text = "Score: " + score.ToString();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        start.enabled = true;
        title.enabled = true;
    }
}
