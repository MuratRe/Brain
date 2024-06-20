using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameNameSpace
{
    public class Box : MonoBehaviour
    {
        Color newColor = new Color(151f / 255f, 255f / 255f, 2f / 255f);
        public bool arrived;

        // Update is called once per frame
        void Update()
        {

            isOnFlag();
        }

        public bool BoxBlocked(Vector3 position, Vector2 direction)
        {
            Vector2 posTo = new Vector2(position.x, position.y) + direction;
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (var wall in walls)
            {

                if (Mathf.Abs(wall.transform.position.x - posTo.x) < 0.3 && Mathf.Abs(wall.transform.position.y - posTo.y) < 0.3)
                {
                    Debug.Log("Blocked by wall");
                    return true;
                }
            }
            GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

            foreach (var box in boxes)
            {
                //Debug.Log($"Position of box is X {box.transform.position.x} && Position of box is Y {box.transform.position.y}");
                //Debug.Log($"Position of PosTo X {posTo.x} && Position of PosTo Y {posTo.y}");
                if (Mathf.Abs(box.transform.position.x - posTo.x) < 0.3 && Mathf.Abs(box.transform.position.y - posTo.y) < 0.3)
                {
                    //Debug.Log("Blocked by another box");
                    return true;
                }
            }
            return false;
        }
        public bool Move(Vector2 direction)
        {
            //Debug.Log($"Check box block to move {BoxBlocked(transform.position, direction)}");
            if (BoxBlocked(transform.position, direction))
            {
                return false;
            }
            else
            {
                transform.Translate(direction * 7 / 9f);
                //Debug.Log("Moved box");
                isOnFlag();
                return true;
            }
        }
        void isOnFlag()
        {
            GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");
            SpriteRenderer boxColor = GetComponent<SpriteRenderer>();

            foreach (var flag in flags)
            {
                if (Mathf.Abs(transform.position.x - flag.transform.position.x) < 0.3 && Mathf.Abs(transform.position.y - flag.transform.position.y) < 0.3)
                {
                    boxColor.color = newColor;
                    arrived = true;
                    return;
                }
                boxColor.color = Color.white;
                arrived = false;
            }
        }
    }
}