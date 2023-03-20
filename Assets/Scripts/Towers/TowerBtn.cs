
using UnityEngine;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    GameObject towerObject;

    public GameObject TowerObject {
        get{
            return towerObject;
        }
    }
}
