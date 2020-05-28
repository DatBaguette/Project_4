using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Imput_Manager : Singleton<Imput_Manager>
{
    // Start is called before the first frame update
    bool is_touched = false;
    private void Update()
    {
        
        if(is_touched == true)
        is_touched = false;
    }
    public bool GetInput()
    {

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
           is_touched = true;
        }  
#endif

#if UNITY_IOS
        if (Input.touchCount > 0)
        {
           is_touched = true;
        }  
#endif

#if UNITY_STANDALONE_WIN
        if (Input.touchCount > 0)
        {
           is_touched = true;
        }
#endif

#if UNITY_STANDALONE_OSX
        if (Input.GetMouseButtonDown(0))
        {
            is_touched = true;
        }
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            is_touched = true;
        }
#endif

        return (is_touched);
    }
}
