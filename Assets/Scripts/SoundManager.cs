using UnityEngine;

public class SoundManager : Loader <SoundManager>
{
    [SerializeField]
    AudioClip lasergun;
    [SerializeField]
    AudioClip minigun;
    [SerializeField]
    AudioClip canon;

    public AudioClip Lasergun{
        get {
            return lasergun;
        }
    }
    public AudioClip Minigun{
        get {
            return minigun;
        }
    }
    public AudioClip Canon{
        get {
            return canon;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
