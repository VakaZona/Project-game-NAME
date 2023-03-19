using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Loader<Manager> {
    

    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int totalEnemies;
    public int enemiesPerSpawn;

    const float spawnDelay=0.5f;

    int enemiesOnScreen = 0;


    IEnumerator Spawn() {
        if(enemiesPerSpawn> 0 && enemiesOnScreen< totalEnemies) {
            for (int i = 0; i < enemiesPerSpawn; i++) {
                if(enemiesOnScreen < maxEnemiesOnScreen) {
                    GameObject newEnemy=Instantiate(enemies[1]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen+=1;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
   

    public void removeEnemyFromScreen() {
        if (enemiesOnScreen > 0) {
            enemiesOnScreen -=1;
        }
    }
}
