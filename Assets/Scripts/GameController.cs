using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool GameIsOver;

	public GameObject gameOverUI;
	public GameObject victoryUI;
	public GameObject pauseUI;
	public GameObject pauseBtn;

	public static GameController instance;

	void Start ()
	{
		ResumeGame();
		FindObjectOfType<AudioManager>().PlaySound("Beach");
		instance = this;
		GameIsOver = false;
	}

	void Update () {
		if (GameIsOver)
			return;

		if (Wall.health <= 0 || Collector.health <= 0)
		{
			EndGame();
		}
	}

	void EndGame ()
	{
		if(pauseBtn != null) pauseBtn.SetActive(false);
		GameIsOver = true;
		Time.timeScale = 0;
		if(gameOverUI != null) gameOverUI.SetActive(true);
	}

	public void WinGame ()
	{
		if(pauseBtn != null) pauseBtn.SetActive(false);
		GameIsOver = true;
		Time.timeScale = 0;
		if(victoryUI != null) victoryUI.SetActive(true);
	}

	public void PauseGame(){
        if(pauseUI != null) pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame(){
		
        Time.timeScale = 1;
        if(pauseUI != null) pauseUI.SetActive(false);
    }

    public void RestartGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void ShowItens(GameObject obj){
        obj.SetActive(true);
    }

    public void HideItens(GameObject obj){
        obj.SetActive(false);
    }
}
