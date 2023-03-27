using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{

    [SerializeField]
    float timeBetweenAttacks;
    [SerializeField]
    float attackRadius;
    [SerializeField]
    Projectile projectile;
    Enemy targetEnemy = null;
    float attackCounter;

    bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        attackCounter-=Time.deltaTime;

        if(targetEnemy==null || targetEnemy.IsDead) {
            Enemy nearestEnemy=GetNearestEnemy();
            if(nearestEnemy!=null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition)<=attackRadius){
                targetEnemy=nearestEnemy;
                // var direction=targetEnemy.transform.localPosition - transform.localPosition;
                // var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // transform.rotation = Quaternion.Euler(0, 0, angle);
                //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(targetEnemy.transform.localPosition.y - transform.localPosition.y, targetEnemy.transform.localPosition.x - transform.localPosition.x) * Mathf.Rad2Deg - 90);
                
            }
        }
        else {
            if(attackCounter<=0) {
                isAttacking = true;
                attackCounter=timeBetweenAttacks;
            }
            else {
                isAttacking = false;
            }
            if(Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition)> attackRadius) {
                targetEnemy=null;
            }
        }
       
        
    }

    public void FixedUpdate() {
        if(isAttacking == true) {
            Attack();
         } 
         //else if(projectile!=null) {
        //     Destroy(projectile);
        // }

    }

    public void Attack() {
        isAttacking = false;
        Projectile newProjectile=Instantiate(projectile) as Projectile;
        newProjectile.transform.localPosition = transform.localPosition;
        if(newProjectile.PType==projectileType.miniBullet){
            Manager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Minigun);
        } else if(newProjectile.PType==projectileType.bigBullet){
            Manager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Canon);
        } else if(newProjectile.PType==projectileType.laser){
            Manager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Lasergun);
        }

        if (targetEnemy==null){
            Destroy(newProjectile);
        }
        else {
            //move projectile
            
            StartCoroutine(MoveProjectile(newProjectile));
        }
        
    }

    IEnumerator MoveProjectile(Projectile projectile) {
        while (GetTargetDistance(targetEnemy)>0.20f && projectile !=null && targetEnemy !=null)
        {
            var dir = targetEnemy.transform.localPosition-transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation=Quaternion.AngleAxis(angleDirection, Vector3.forward);
            transform.rotation = Quaternion.Euler(0, 0, angleDirection-90);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f*Time.deltaTime);
            yield return null;
        }
        if(projectile != null ) {
            Destroy(projectile.gameObject);
        }
        
    }

    private float GetTargetDistance(Enemy thisEnemy) {
        if(thisEnemy==null) {
            thisEnemy = GetNearestEnemy();
            if(thisEnemy == null) {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }

    private List<Enemy> GetEnemiesInRange() 
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (Enemy enemy in Manager.Instance.EnemyList) 
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <=attackRadius) 
            {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    private Enemy GetNearestEnemy() 
    {
        Enemy nearestEnemy = null;
        float smallesDistance = float.PositiveInfinity;

        foreach(Enemy enemy in GetEnemiesInRange()) 
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= smallesDistance) 
            {
                smallesDistance=Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
