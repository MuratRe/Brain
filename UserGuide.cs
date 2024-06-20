using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

namespace GameNameSpace
{
    public class UserGuide : MonoBehaviour
    {
        public string htmlGuide = "index.htm";

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                OpenGuide();
            }
        }

        public void OpenGuide()
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, htmlGuide);
            Process.Start(filePath);
        }
    }
}