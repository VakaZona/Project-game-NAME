using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class TowerManager : Loader<TowerManager>
{
    public TowerBtn towerBtnPressed;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && towerBtnPressed!=null) {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if(hit.collider.tag == "TowerSide"){
                    hit.collider.tag = "TowerSideFull";
                    
                    PlaceTower(hit);
            }
           
            
        }   
        if(spriteRenderer.enabled) {
                FollowMouse();
        } 
    }

    public void PlaceTower(RaycastHit2D hit) {
        if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed!=null){
            GameObject newTower=Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            DisableDrag();
        }
       
    }

    public void SelectedTower(TowerBtn towerSelected) {
        towerBtnPressed = towerSelected;
        EnableDrag(towerBtnPressed.DragSprite);
        Debug.Log("Pressed" + towerBtnPressed.gameObject);
    }

    public void FollowMouse() {
        transform.position=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position= new Vector2(transform.position.x, transform.position.y);
    }
    public void EnableDrag(Sprite sprite){
        spriteRenderer.enabled=true;
        spriteRenderer.sprite=sprite;
    }
    public void DisableDrag(){
        spriteRenderer.enabled=false;
    }
}
