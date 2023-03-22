using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Loader<Manager> {
    
    [SerializeField]
    GameObject spawnPoint;
    [SerializeField]
    GameObject[] enemies;
    [SerializeField]
    int maxEnemiesOnScreen;
    [SerializeField]
    int totalEnemies;
    [SerializeField]
    int enemiesPerSpawn;

    public List<Enemy> EnemyList = new List<Enemy>();
    

    const float spawnDelay=0.5f;

    

    IEnumerator Spawn() {
        if(enemiesPerSpawn> 0 && EnemyList.Count< totalEnemies) {
            for (int i = 0; i < enemiesPerSpawn; i++) {
                if(EnemyList.Count < maxEnemiesOnScreen) {
                    GameObject newEnemy=Instantiate(enemies[0]) as GameObject;
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
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
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

    
}
