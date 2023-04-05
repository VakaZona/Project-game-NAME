using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance = null;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    int levelComplete;
    
    public void PlayGame(){
        SceneManager.LoadScene("MainScene");
    }
    public void PlayLevelOne(){
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevelTwo(){
        SceneManager.LoadScene("Level2");
    }
    public void PlayLevelThree(){
        SceneManager.LoadScene("Level3");
    }
    public void PlayLevelFour(){
        SceneManager.LoadScene("Level4");
    }
     public void PlayLevelFive(){
        SceneManager.LoadScene("Level5");
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
        if (instance==null){
            instance = this;
        }
        if (level2!=null && level3!=null && level4!=null && level5!=null) {
            levelComplete=PlayerPrefs.GetInt("LevelComplete");
        level2.interactable = false;
        level3.interactable = false;
        level4.interactable = false;
        level5.interactable = false;

        }
        
        switch (levelComplete)
        {
            case 1:
                level2.interactable = true;
                break;
            case 2:
                level2.interactable = true;
                level3.interactable = true;
                break;
            case 3:
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                break;  
            case 4:
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                level5.interactable = true;
                break;  
        }

    }
    // public void LoadTo(int level){

    // }
    public void Reset(){
        level2.interactable = false;
        level3.interactable = false;
        level4.interactable = false;
        level5.interactable = false;
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
