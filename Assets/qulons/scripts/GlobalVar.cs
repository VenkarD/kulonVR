using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qulons
{
    public class GlobalVar : MonoBehaviour
    {
        public float K;
        public Material Plus, Minus, Neitron;
        public Material PlusL, MinusL, NeitronL;
        public static GlobalVar gv;


        private void Awake()
        {
            gv = this;
        }
    }
}
