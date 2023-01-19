using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    //keeping Music Player on for other scene as well this function is called before start and all others

    [System.Obsolete]
    private void Awake()
    {
        int numMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;
       if(numMusicPlayer>1)
        {
            Destroy(gameObject);
        }
       else
        { 
            DontDestroyOnLoad(gameObject); 
        }
    }


    // Start is called before the first frame update
  
}
