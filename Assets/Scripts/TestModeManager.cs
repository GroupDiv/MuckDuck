using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Simply sets a flag if test mode is enabled, survives through start screen into main game
public class TestModeManager : MonoBehaviour
{

    //! True if test mode, false otherwise.  This is literally the only point of this object.  It also is the only object that lives between the start screen menu and the main game
    public bool testFlag;

    void Start()
    {
        testFlag = false;
    }

    void Update()
    {
        
    }
}
