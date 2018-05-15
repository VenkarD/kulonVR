using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Qulons
{
    public class zarad : MonoBehaviour
    {

        public GameObject ll, rig, target;
        public Vector3 force;
        public int CurrentQ = 1;
        public float DistTarget;
        public Animator[] anim;


        Rigidbody rb;
        GlobalVar gv;
        public MeshRenderer mr;
        MainControl mc;
        private void Awake()
        {
            mc = GetComponentInParent<MainControl>();
            gv = GlobalVar.gv;
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            ChangeQ(CurrentQ);
        }

        void GetMinDist()
        {
            float force = 0;
            GameObject go = null;
            foreach(zarad g in mc.qulons)
            {
                float d = Vector3.Distance(transform.position, g.transform.position);
                if (g!=this && force < (Mathf.Abs(CurrentQ) * Mathf.Abs(g.CurrentQ)) / (d * d) && d < 2)
                {
                    Debug.Log(force);
                    force = (Mathf.Abs(CurrentQ) * Mathf.Abs(g.CurrentQ)) / Mathf.Max((d * d), 10f);
                    go = g.gameObject;
                }
            }
            target = go;
        }

        Vector3 GetQ(Transform t, int q)
        {
            Vector3 ret = new Vector3();
            float r = Vector3.Distance(transform.position, t.position);
            ret = t.position - transform.position;
            float force = (Mathf.Abs(CurrentQ) * Mathf.Abs(q)) / Mathf.Max((r * r), 10f) * gv.K;
            ret *= force / r;
            return ret;
        }

        public void ChangeQ(int newQ)
        {
            if (newQ != 0)
            {
                rig.transform.localScale = Vector3.one * Mathf.Max(0.6f+ Mathf.Abs(newQ) * 0.4f, 1);
                GetComponent<SphereCollider>().radius = Mathf.Max(0.6f + Mathf.Abs(newQ) * 0.4f, 1) * 0.04914215f;
            }
            else
            {
                rig.transform.localScale = Vector3.one * Mathf.Max(1, 1);
                GetComponent<SphereCollider>().radius = Mathf.Max(1, 1) * 0.04914215f;
            }

            CurrentQ = newQ;
            if (newQ > 0)
            {
                mr.material = gv.Plus;
                for (int i = 0; i < 6; i++)
                {
                    anim[i].GetComponent<LineRenderer>().material = gv.PlusL;
                }
            }
            else if (newQ < 0)
            {
                mr.material = gv.Minus;
                for (int i = 0; i < 6; i++)
                {
                    anim[i].GetComponent<LineRenderer>().material = gv.MinusL;
                }
            }
            else
            {
                mr.material = gv.Neitron;
                for (int i = 0; i < 6; i++)
                {
                    anim[i].GetComponent<LineRenderer>().material = gv.NeitronL;
                }
            }


        }

        // Update is called once per frame
        void Update()
        {
            GetMinDist();
            UpdateLines();
            force = Vector3.zero;
            foreach (var q in mc.qulons)
            {
                if (q != this)
                {
                    if (CurrentQ *q.CurrentQ < 0)
                        force += GetQ(q.transform, q.CurrentQ);
                    else
                        force -= GetQ(q.transform, q.CurrentQ);
                }
            }
            //transform.position += force * Time.deltaTime * 5;
            if (force != null)
                rb.AddForce(force * Time.deltaTime, ForceMode.Impulse);
        }

        void UpdateLines()
        {
            //  ll.transform.LookAt(target.transform);
            if (target != null)
            {
                Vector3 t = target.transform.position - transform.position;
                Quaternion q = new Quaternion();
                if (Vector3.Distance(transform.position, target.transform.position) < 2)
                {
                    q.SetLookRotation(t);
                }
                else
                {
                    q = Quaternion.identity;
                }

                ll.transform.rotation = q;
                //ll.transform.rotation = Quaternion.Lerp(ll.transform.rotation, q, Time.deltaTime * 1/ Vector3.Distance(transform.position, target.transform.position)*10);
                DistTarget = Vector3.Distance(transform.position, target.transform.position);

                DistTarget *= (target.GetComponent<zarad>().CurrentQ * CurrentQ > 0) ? -1:1;

                for (int i = 0; i < 6; i++)
                {
                    anim[i].SetFloat("Blend", Mathf.Min(DistTarget, 2));
                }
            }else
            {
                ll.transform.rotation = Quaternion.identity;
                for (int i = 0; i < 6; i++)
                {
                    anim[i].SetFloat("Blend", 2);
                }
            }
            /*endGo.transform.localPosition = new Vector3(DistTarget, 0, 0);
            startGo.transform.localScale = Vector3.one * DistTarget * 0.9f + new Vector3(0.1f, 0.1f, 0.1f);
            endGo.transform.localScale = Vector3.one * DistTarget * 0.9f + new Vector3(0.1f, 0.1f, 0.1f);
            p4.transform.localPosition = new Vector3(DistTarget/2, 0, (DistTarget*0.9f+0.1f) * 0.4f);*/
        }
    }
}
