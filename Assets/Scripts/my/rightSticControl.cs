using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightSticControl : MonoBehaviour {

    public MainControl mc;
    public MeshRenderer mr;
    private SteamVR_TrackedObject trackedObj;
    public rightHandTrigger rht;
    public bool canSpawn;


    bool isTap;
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    void Update()
    {

        if (rht.gos.Count > 0)
        {
            canSpawn = false;
            mr.enabled = false;
        }
        else
        {
            canSpawn = true;
            mr.enabled = true;
        }

        if (Controller.GetAxis().x != 0)
        {
            mc.ChangeQ(Controller.GetAxis().x);
        }

        if (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x == 1 && !isTap)
        {
            isTap = true;
            mc.Spawn(Controller);
        }
        if (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x < 0.9f && isTap)
        {
            isTap = false;
        }


        // if (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))

        /* if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
         {
             mc.DeleteLeft();
         }
         if (Controller.GetAxis().x != 0)
         {
             mc.AxisLeft(Controller.GetAxis().x);
         }

         if (Controller.GetHairTriggerUp())
         {
             mc.LeftSticUp(Controller.velocity, Controller.angularVelocity);
         }

         if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
         {
             mc.LeftSticDown();
         }*/
    }
}
