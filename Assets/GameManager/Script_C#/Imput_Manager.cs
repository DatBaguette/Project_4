using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Imput_Manager : Singleton<Imput_Manager>
{
    // Start is called before the first frame update
    public bool GetInput()
    {
        bool is_touched = true;


#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            is_touched = true;
        }
#else
        if (Input.touchCount > 0)
        {
           is_touched = true
        }

            
#endif
        return (is_touched);
    }
}
