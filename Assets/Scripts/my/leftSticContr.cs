using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class leftSticContr : MonoBehaviour
{
    public MainControl mc;
    public Animator anim;
    public Image play;
    public Sprite playS, playPause;
    bool isTap;
    private SteamVR_TrackedObject trackedObj;
    public SteamVR_TrackedController stc;

    public audioControl ac;
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    bool isPause;

    bool prest;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
        }
        if (!prest && stc.padPressed)
        {
            prest = true;
            ac.PlayClip();
            Debug.Log("yhsefcgjdfsbhkasnf");
            if ((Controller.GetAxis().x > 0.1f) && !isPause && stc)
            {
                mc.DeleteGO();
            }
            else if (stc.padPressed)
            {
                Debug.Log("stc");
                if (isPause == true) isPause = false;
                else if (isPause == false) isPause = true;
                play.sprite = isPause ? playS : playPause;
                if (isPause == true)
                {
                    anim.SetBool("isPause", true);
                    mc.PauseDepause();
                    //Debug.Log("isPause=" + isPause);
                }
                else
                {
                    Debug.Log("isPause=" + isPause);
                    anim.SetBool("isPause", false);

                    mc.PauseDepause();
                }
            }

        }
        else if (!stc.padPressed)
        {
            prest = false;
        }

        {
            if (Controller.GetAxis().x > 0.1f)
            {
                anim.SetBool("toDel", true);
                anim.SetBool("toPause", false);
            }
            else if (Controller.GetAxis().x < -0.1f)
            {
                anim.SetBool("toDel", false);
                anim.SetBool("toPause", true);
            }
            else
            {
                anim.SetBool("toDel", false);
                anim.SetBool("toPause", false);
            }
        }
        if (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x == 1 && !isTap)
        {
            isTap = true;
            mc.StartStopDrag(Controller);
        }
        if (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x < 0.9f && isTap)
        {
            isTap = false;
        }

        if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_DPad_Right))
        {
            mc.DeleteGO();
        }

        if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_DPad_Left))
        {
            mc.PauseDepause();
        }

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
