using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardController : MonoBehaviour
{
    
    
    [SerializeField]
    public Button ligth;
    [SerializeField]
    public Button middle;
    [SerializeField]
    public Button hard;
    int defaultComplexity=1;
    int complexity;
    [SerializeField]
    public Sprite buttonWrapper;
    // Start is called before the first frame update
    void Start()
    {
        complexity=PlayerPrefs.GetInt("Complexity");
        if(complexity==null){
            complexity=defaultComplexity;
        }
        SelectComplexity(complexity);

    }

    public void SelectComplexity(int complexityGet){
        complexity=complexityGet;
        PlayerPrefs.SetInt("Complexity", complexity);
        Debug.Log(complexity);
        
        switch(complexity){
            case 1:
                ligth.image.sprite=buttonWrapper;
                
                
                middle.image.sprite=null;
                
                
                hard.image.sprite=null;
                
                break;
            case 2:
                ligth.image.sprite=null;
                
                middle.image.sprite=buttonWrapper;
               
                hard.image.sprite=null;
                
                break;
            case 3:
                ligth.image.sprite=null;
                
                middle.image.sprite=null;
                
                hard.image.sprite=buttonWrapper;
               
                
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
