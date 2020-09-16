using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterPoint : MonoBehaviour
{
    [Header("Interaction Attributes")]
    //The jumpForce for this object
    public float jumpForce;

    [Header("Lists")]
    //List for all the modes
    public bool[] interMode;

    [Header("Mode Booleans")]
    //Variables that set the value for the interMode list
    public bool jump;
    public bool lightChangePath;
    //public bool "modeName"; example

    //Start is called once in the beginning
    private void Start()
    {
        //If you wan't to add modes, add the corresponding line under these lines, the corresponding variable and change the size of the list in Unity
        interMode[0] = jump;
        interMode[1] = lightChangePath;
        //interMode[2] = "modeName"; example
    }
}
