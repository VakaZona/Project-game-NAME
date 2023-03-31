using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControler : MonoBehaviour
{
    [SerializeField]
    Transform exit;
    [SerializeField]
    float timeBetweenAttacks;
    [SerializeField]
    float attackRadius;
    [SerializeField]
    Projectile projectile;
    float navigationTime=0;
    float attackCounter;
    Animator anim;
    bool isAttacking = false;
    Transform helicopter;
    Collider2D helicopterCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        helicopter = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        helicopterCollider = GetComponent<Collider2D>();
        
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {   
            navigationTime += Time.deltaTime/10;
            helicopter.position=Vector2.MoveTowards(helicopter.position, exit.position, navigationTime);
            
        
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag=="FinishHelicopter") {
           Destroy(helicopter.gameObject);
           Debug.Log("Yes");
        }
    }
}
