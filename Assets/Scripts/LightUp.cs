using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightUp : MonoBehaviour
{
    [Header("Object to trigger or to pass information")]
    //Variable for the 2D light object
    public Light2D _myLight;
    //Variable for the guy character
    public GameObject guy;
    //Variable for the characters move point which has the corresponding condition
    public Transform guyLightTriggerPoint;

    [Header("Lists (You Can Only Have 1 Boolean True At A Time)")]
    //Boolean list for the modes this gameObject can use
    public bool[] InterModes;

    //When the LightNow function is activated
    public void LightNow()
    {
        //If the InterModes boolean list's first boolean is true
        if (InterModes[0])
        {
            //If the characters current move target is equal to the guyLightTriggerPoint variable's transform
            if (guy.GetComponent<GuyScript>().currentMovePoint == guyLightTriggerPoint)
            {
                //Set the LightIsOn boolean from the GuyScript to true
                guy.GetComponent<GuyScript>().LightIsOn = true;
            }
            //Set this object's light to the specified intensity (turning it on)
            _myLight.intensity = 2.5f;
        }
        //If the InterModes list has 2 or more elements
        if (InterModes.Length >= 2)
        {
            //If the InterModes boolean list's second boolean is true
            if (InterModes[1])
            {
                //Set this object's light to the specified intensity (turning it on)
                _myLight.intensity = 2.5f;
            }
        }        
    }

    //When the LightIsOff function is activated
    public void LightOff()
    {
        //If the InterModes boolean list's first boolean is true
        if (InterModes[0])
        {
            //If the characters current move target is equal to the guyLightTriggerPoint variable's transform 
            if (guy.GetComponent<GuyScript>().currentMovePoint == guyLightTriggerPoint)
            {
                //Set the LightIsOn boolean from the GuyScript to false
                guy.GetComponent<GuyScript>().LightIsOn = false;
            }
            //Set this object's light to the specified intensity (turning it off)
            _myLight.intensity = 0f;

        }
        //If the InterModes list has 2 or more elements
        if (InterModes.Length >= 2)
        {
            //If the InterModes boolean list's second boolean is true
            if (InterModes[1])
            {
                //Set this object's light to the specified intensity (turning it off)
                _myLight.intensity = 0f;
            }
        }
    }
}
