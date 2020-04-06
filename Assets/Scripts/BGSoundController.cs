using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundController : MonoBehaviour
{
    private static BGSoundController instance = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static BGSoundController Instance
    {
        get { return instance; }
    }

    // Update is called once per frame
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        
    }
}
