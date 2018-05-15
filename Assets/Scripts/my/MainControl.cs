using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainControl : MonoBehaviour {

    public GameObject electronPrefab;
    public rightSticControl handR;
    public handTrack handL;
    public Transform pivot2Spawn;
    public GameObject objectInHand;
    public Animator anim;
    public MeshRenderer mr;
    public Material[] mat;
    public int currentQ = -1;
    public Text t;

    public List<Qulons.zarad> qulons = new List<Qulons.zarad>();

    float targetAnim = 0;
    float currentAnim = 0;
    public void Spawn(SteamVR_Controller.Device Controller)
    {
        if (handR.canSpawn)
        {
            GameObject go = Instantiate(electronPrefab, transform);
            go.GetComponent<Qulons.zarad>().CurrentQ = currentQ;
            qulons.Add(go.GetComponent<Qulons.zarad>());
            go.transform.position = pivot2Spawn.TransformPoint(new Vector3(0, 0.04f + Mathf.Abs(currentQ) * 0.01f, 0));
            go.GetComponent<Rigidbody>().velocity = Controller.velocity;
            go.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
    }

    public void Spawn (int q, float x, float y, float z)
    {
        GameObject go = Instantiate(electronPrefab, transform);
        go.GetComponent<Qulons.zarad>().CurrentQ = q;
        qulons.Add(go.GetComponent<Qulons.zarad>());
        go.transform.position = new Vector3(x, y, z);
    }

    private void Start()
    {
        ChangeQ(1);
    }

    public void ChangeQ(float pos)
    {
        currentQ = (int)(pos * 4.3f);
        targetAnim = Mathf.Abs(currentQ / 4f);
        if (currentQ == 0)
        {
            mr.material = mat[0];
        } else if (currentQ < 0)
        {
            mr.material = mat[1];
        }
        else if (currentQ > 0)
        {
            mr.material = mat[2];
        }
        t.text = "Q = " + currentQ;
    }

    public void StartStopDrag(SteamVR_Controller.Device Controller)
    {
        if (objectInHand == null)
        {
            if (handL.go != null)
            {
                objectInHand = handL.go;
            }
        } else
        {
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
            objectInHand = null;
        }
    }

    public void DeleteGO()
    {
        if (objectInHand != null)
        {
            qulons.Remove(objectInHand.GetComponent<Qulons.zarad>());
            handL.Delete(objectInHand);
            Destroy(objectInHand);
            objectInHand = null;
        }
    }

    public void PauseDepause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }else
        {
            Time.timeScale = 0;
            objectInHand = null;
        }
    }

    private void Update()
    {
        Drag();
        qulons = FindObjectsOfType<Qulons.zarad>().ToList();
        currentAnim += (targetAnim - currentAnim) * Time.deltaTime * 5;
        anim.SetFloat("Blend", currentAnim);
    }

    void Drag()
    {
        if (objectInHand != null)
        {
            objectInHand.transform.position = handL.transform.TransformPoint(new Vector3(0, 0.06f + Mathf.Abs(currentQ) * 0.015f, 0));
            objectInHand.transform.rotation = handL.transform.rotation;
        }
    }
}

    /*  public void SpawnRight(Transform t)
      {
          GameObject go = Instantiate(electronPrefab, transform);
          qulons.Add(go.GetComponent<Qulons.zarad>());
          go.transform.position = t.position;
          objectInRightHand = go;
          objectInRightHand.GetComponent<Qulons.zarad>().Select();

      }

      public void SpawnLeft(Transform t)
      {
          GameObject go = Instantiate(electronPrefab, transform);
          qulons.Add(go.GetComponent<Qulons.zarad>());
          go.transform.position = t.position;
          objectInLeftHand = go;
          objectInLeftHand.GetComponent<Qulons.zarad>().Select();

      }


      public void DeleteRight()
      {
         if(objectInRightHand != null)
          {
              qulons.Remove(objectInRightHand.GetComponent<Qulons.zarad>());
              Destroy(objectInRightHand);
          }else if (objectInLeftHand != null)
          {
              qulons.Remove(objectInLeftHand.GetComponent<Qulons.zarad>());
              Destroy(objectInLeftHand);
          }
      }

      public void DeleteLeft()
      {
          if (objectInLeftHand != null)
          {
              qulons.Remove(objectInLeftHand.GetComponent<Qulons.zarad>());
              Destroy(objectInLeftHand);
          }
          else if (objectInRightHand != null)
          {
              qulons.Remove(objectInRightHand.GetComponent<Qulons.zarad>());
              Destroy(objectInRightHand);
          }
      }

      public void RightSticDown()
      {
          if (handR.go != null)
          {
              StartDragRight();
          }
          else
          {
              SpawnRight(handR.transform);
          }
      }

      public void LeftSticDown()
      {
          if (handL.go != null)
          {
              StartDragLeft();
          }
          else
          {
              SpawnLeft(handL.transform);
          }
      }

      public void Update()
      {
          if (objectInLeftHand != null)
              DragLeft();
          if (objectInRightHand != null)
              DragRight();
      }


      public void RightSticUp(Vector3 vel, Vector3 angl)
      {
          EndDragRight(vel, angl);
      }

      public void LeftSticUp(Vector3 vel, Vector3 angl)
      {
          EndDragLeft(vel, angl);
      }

      public void AxisLeft(float p)
      {
          if (objectInLeftHand!= null)
              objectInLeftHand.GetComponent<Qulons.zarad>().ChangeQ((int)(p * 4.5f));
      }

      public void AxisRight(float p)
      {
          if (objectInRightHand != null)
              objectInRightHand.GetComponent<Qulons.zarad>().ChangeQ((int)(p * 4.5f));
      }

      void StartDragRight()
      {
          if (handR.go.GetComponent<Qulons.zarad>() && objectInLeftHand == null)
          {
              objectInRightHand = handR.go;
              objectInRightHand.GetComponent<Qulons.zarad>().Select();
          }
      }

      void StartDragLeft()
      {
          if (handL.go.GetComponent<Qulons.zarad>() && objectInLeftHand == null)
          {
              objectInLeftHand = handL.go;
              objectInLeftHand.GetComponent<Qulons.zarad>().Select();
          }
      }


      void DragLeft()
      {
          if (objectInLeftHand != null && handL.go != null)
          {
              objectInLeftHand = handL.go;
              objectInLeftHand.transform.position = handL.transform.position;
              objectInLeftHand.transform.rotation = handL.transform.rotation;
          }
      }
      //UI с индикацией заряда около электрона
      void EndDragRight(Vector3 vel, Vector3 angl)
      {
          if (objectInRightHand != null)
          {
              objectInRightHand.GetComponent<Qulons.zarad>().DeSelect();
              objectInRightHand.GetComponent<Rigidbody>().velocity = vel;
              objectInRightHand.GetComponent<Rigidbody>().angularVelocity = angl;
          }
          objectInRightHand = null;
      }

      void EndDragLeft(Vector3 vel, Vector3 angl)
      {
          if (objectInLeftHand != null)
          {
              objectInLeftHand.GetComponent<Qulons.zarad>().DeSelect();
              objectInLeftHand.GetComponent<Rigidbody>().velocity = vel;
              objectInLeftHand.GetComponent<Rigidbody>().angularVelocity = angl;
          }
          objectInLeftHand = null;
      }*/

