using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void LevelOne(){
        SceneManager.LoadScene("Level1");
    }

    public void LevelTwo(){
        SceneManager.LoadScene("Level2");
    }

    public void LevelThree(){
        SceneManager.LoadScene("Level3");
    }
}
