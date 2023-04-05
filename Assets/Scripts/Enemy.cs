using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    
    [SerializeField]
    Transform exit;
    [SerializeField]
    Transform[] wayPoints;
    [SerializeField]
    float navigation;
    [SerializeField]
    int health;
    [SerializeField]
    int rewardAmount;


    Collider2D enemyCollider;
    bool isDead=false;
    int target = 0;
    Transform enemy;
    float navigationTime=0;
    Animator anim;
    int complexity;
    public bool IsDead {
        get {
            return isDead;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        Manager.Instance.RegisterEnemy(this);
        anim = GetComponent<Animator>();
        complexity=PlayerPrefs.GetInt("Complexity");
        if(complexity==null){
            complexity=1;
        }
        if(complexity==2){
            health+=10;
        }
        if(complexity==3){
            health+=20;
        }
        Debug.Log(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(wayPoints!=null && isDead==false) {
            navigationTime += Time.deltaTime;
            if(navigationTime> navigation) {
                if (target<wayPoints.Length) {
                    enemy.position=Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                } else {
                    enemy.position=Vector2.MoveTowards(enemy.position, exit.position, navigationTime);
                }
                navigationTime=0;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag=="MoviengPoint") {
            target+=1;
        } else if (collision.tag=="Finish") {
            Manager.Instance.RoundEscaped+=1;
            Manager.Instance.TotalEscaped+=1;
            Manager.Instance.UnregisterEnemy(this);
            Manager.Instance.IsWaveOver();
        } else if (collision.tag == "Projectile") {
            Projectile newP=collision.gameObject.GetComponent<Projectile>();
            if(newP!=null){
                EnemyHit(newP.AttackDamage);
                
            }
            Destroy(collision.gameObject);
            
        } else if (collision.tag=="GroundLeftDown" || collision.tag=="GroundRightDown") {
            anim.Play("bullRunDown");
        } else if (collision.tag=="GroundLeftTop" || collision.tag=="GroundRightTop") {
            anim.Play("bullRunTop");
        } else if (collision.tag=="GroundTopRight" || collision.tag=="GroundDownRight") {
            anim.Play("bullRun");
        } else if (collision.tag=="GroundTopLeft" || collision.tag=="GroundDownLeft") {
            anim.Play("bullRunLeft");
        }
    }
    public void EnemyHit(int hitPoints) {
        if(health - hitPoints > 0) {
            health-=hitPoints;
            anim.Play("Hunt");
        } else {
            //die
            anim.SetTrigger("didDie");
            Die();
        }
        
    }

    public void Die(){

        isDead=true;
        enemyCollider.enabled=false;
        Manager.Instance.TotalKilled+=1;
        Manager.Instance.addMoney(rewardAmount);
        Manager.Instance.IsWaveOver();
    }
}
