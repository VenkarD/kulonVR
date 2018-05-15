
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;
    MainControl mc;


    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }


    void Awake()
    {
        mc = GameObject.FindObjectOfType<MainControl>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }



    void Update()
    {
        /*if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            mc.RightSticDown();
        }

        if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            mc.DeleteRight();
        }

        if (Controller.GetAxis().x != 0)
        {
            mc.AxisRight(Controller.GetAxis().x);
        }

        if (Controller.GetHairTriggerUp())
        {
            mc.RightSticUp(Controller.velocity, Controller.angularVelocity);

        }*/
    }
}
