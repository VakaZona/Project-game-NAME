
using UnityEngine;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    TowerControl towerObject;
    [SerializeField]
    Sprite dragSprite;
    [SerializeField]
    int towerPrice;

    public TowerControl TowerObject {
        get{
            return towerObject;
        }
    }
    
    public Sprite DragSprite {
        get{
            return dragSprite;
        }
    }
    public int TowerPrice 
    {
        get{
            return towerPrice;
        }
    }
}
