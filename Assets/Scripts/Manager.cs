using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum gameStatus {
    next, play, gameover, win
}

public class Manager : Loader<Manager> {
    
    [SerializeField]
    int totalWaves=10;
    [SerializeField]
    Text totalMoneyLabel;
    [SerializeField]
    Text currentWave;
    [SerializeField]
    Text totalEscapedLabel;
    [SerializeField]
    Text playBtnLabel;
    [SerializeField]
    Button playBtn;
    [SerializeField]
    GameObject spawnPoint;
    [SerializeField]
    Enemy[] enemies;
    
    [SerializeField]
    int totalEnemies=5;
    [SerializeField]
    int enemiesPerSpawn;

    int waveNumber=0;
    int totalMoney=30;
    int totalEscaped=0;
    int roundEscaped = 0;
    int totalKilled=0;
    int whichEnemiesToSpawn = 0;
    int enemiesToSpawn = 0;
    gameStatus currentStatus = gameStatus.play;

    public List<Enemy> EnemyList = new List<Enemy>();
    AudioSource audioSource;

    const float spawnDelay=0.5f;
    
    public AudioSource AudioSource{
        get {
            return audioSource;
        }
    }

    public int TotalEscaped
    {
        get{
            return totalEscaped;
        }
        set {
            totalEscaped=value;
        }
    }

    public int RoundEscaped
    {
        get{
            return roundEscaped;
        }
        set {
            roundEscaped=value;
        }
    }
    public int TotalKilled
    {
        get{
            return totalKilled;
        }
        set {
            totalKilled=value;
        }
    }

    public int TotalMoney {
        get
        {
            return totalMoney;
        }
        set
        {
            totalMoney = value;
            totalMoneyLabel.text = TotalMoney.ToString();
        }
    }

    IEnumerator Spawn() {
        if(enemiesPerSpawn> 0 && EnemyList.Count< totalEnemies) {
            for (int i = 0; i < enemiesPerSpawn; i++) {
                if(EnemyList.Count < totalEnemies) {
                    Enemy newEnemy=Instantiate(enemies[Random.Range(0, enemiesToSpawn)]) as Enemy;
                    newEnemy.transform.position = spawnPoint.transform.position;

                }
            }

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playBtn.gameObject.SetActive(false);
        audioSource=GetComponent<AudioSource>();
        audioSource.volume=0.3f;
        ShowMenu();
    }


    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }


   public void RegisterEnemy(Enemy enemy) {
        EnemyList.Add(enemy);
   }

   public void UnregisterEnemy(Enemy enemy) {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
   }

   public void DestroyEnemies(){
        foreach( Enemy enemy in EnemyList) {
            Destroy(enemy.gameObject);
        }
       
        EnemyList.Clear();
        
   }


   //UI methods
   public void addMoney(int amount)
   {
        TotalMoney+=amount;
   }
   public void subtractMoney(int amount)
   {
        TotalMoney-=amount;
   }

    public void IsWaveOver()
    {
        totalEscapedLabel.text = "Пропущено " + TotalEscaped + "/10";
        if((RoundEscaped+TotalKilled)==totalEnemies) {
            if(waveNumber<=enemies.Length){
                enemiesToSpawn=waveNumber;
            }
            SetCurrentGameState();
            ShowMenu();
        }
       
    }

    public void SetCurrentGameState() {
        if(TotalEscaped >=10) {
            currentStatus = gameStatus.gameover;
            
        } else if(waveNumber==0 && (RoundEscaped+TotalKilled)==0) {
            currentStatus = gameStatus.play;
           
        } else if (waveNumber >=totalWaves) {
            currentStatus = gameStatus.win;
            
        } else {
            currentStatus = gameStatus.next;
            // DestroyEnemies();
            
        }
    }

    public void PlayButtonPressed(){
        switch(currentStatus){
            case gameStatus.next:
                waveNumber+=1;
                totalEnemies+=(waveNumber);
                break;
            case gameStatus.play:
                totalEnemies = 5;
                TotalEscaped = 0;
                TotalMoney = 40;
                enemiesToSpawn = 0;
                TowerManager.Instance.DestroyAllTowers();
                TowerManager.Instance.RenameTagBuildSite();
                totalMoneyLabel.text = TotalMoney.ToString();
                totalEscapedLabel.text = "Пропущено " + TotalEscaped + "/10";
                break;
            // default:
            //     totalEnemies = 5;
            //     TotalEscaped = 0;
            //     TotalMoney = 30;
            //     totalMoneyLabel.text = TotalMoney.ToString();
            //     totalEscapedLabel.text = "Пропущено " + TotalEscaped + "/10";
            //     break;
        }
        DestroyEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        currentWave.text = "Волна " + (waveNumber + 1);
        StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);
    }


   public void ShowMenu()
   {
        switch(currentStatus){
            case gameStatus.gameover:
                playBtnLabel.text="Новая игра";
                break;
            case gameStatus.next:
                playBtnLabel.text="Новая волна";
                break;
            case gameStatus.play:
                playBtnLabel.text="Начать игру";
                break;
            case gameStatus.win:
                playBtnLabel.text="Следущий уровень";
                break;
    }
        playBtn.gameObject.SetActive(true);
   }
   private void HandleEscape() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisableDrag();
            TowerManager.Instance.towerBtnPressed = null;
        }
   }

    
}
