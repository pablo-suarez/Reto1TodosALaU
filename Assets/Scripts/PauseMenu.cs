using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseButton;

    [SerializeField] private GameObject pauseMenu;

    private bool gamePaused = false;

    public void Pause(){
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        gamePaused = true;
    }

    public void Resume(){
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        gamePaused = false;
    }

    public void Restart(){
        gamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit(){
        SceneManager.LoadScene("InitialMenu");
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gamePaused){
                Resume();
            }else{
                Pause();
            }
        }
    }
 
}
