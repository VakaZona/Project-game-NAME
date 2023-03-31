using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void PlayGame(){
        SceneManager.LoadScene("MainScene");
    }
    public void PlayLevelOne(){
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevelTwo(){
        SceneManager.LoadScene("Level2");
    }
    public void BackMenu(){
        SceneManager.LoadScene("Menu");
    }
    public void ExitGame(){
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
