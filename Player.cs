using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameNameSpace
{
    public struct BoxWallBlock
    {
        public bool blockedByBox;
        public bool blocked;
    }

    public struct Moves
    {
        public Vector3 fromPos;
        public Vector3 toPos;
        public Box movedBox;
        public Vector3 boxPos;
        public bool withBox;
    }


    public class Player : MonoBehaviour
    {
        bool canMove = true;
        public int moveNums;
        public Stack<Moves> moves = new Stack<Moves>();
        public Moves move;


        public bool Move(Vector2 direction)
        {
            direction *= (7f / 9f);
            if (Mathf.Abs(direction.x) < 0.5)
            {
                direction.x = 0;
            }
            else
            {
                direction.y = 0;
            }
            direction.Normalize();
            if (canMove)
            {
                if (isBlocked(direction, transform.position).blockedByBox)
                {
                    if (isBlocked(direction, transform.position).blocked)
                    {
                        Debug.Log($"{isBlocked(direction, transform.position).blockedByBox}");
                        Debug.Log("Blocked!!!");
                        return false;
                    }
                    else
                    {
                        Debug.Log("Is Moving but with box");
                        move.fromPos = transform.position;
                        move.toPos = new Vector3(direction.x, direction.y, 0);
                        move.withBox = true;
                        moves.Push(move);
                        Debug.Log($"Moved from {move.fromPos} To Pos {move.toPos} with box");
                        transform.Translate(direction * (7f / 9f));
                        moveNums++;
                    }
                }
                else
                {
                    if (isBlocked(direction, transform.position).blocked)
                    {
                        Debug.Log("isBlocked!!!");
                        return false;
                    }
                    else
                    {
                        Debug.Log("Moving without box");
                        move.fromPos = transform.position;
                        move.toPos = new Vector3(direction.x, direction.y, 0);
                        move.withBox = false;
                        moves.Push(move);
                        Debug.Log($"Moved from {move.fromPos} To Pos {move.toPos} without box");
                        transform.Translate(direction * (7f / 9f));
                        moveNums++;
                    }
                }
                return true;
            }
            return false;
        }

        public void DisableMove()
        {
            canMove = false;
        }

        BoxWallBlock isBlocked(Vector2 direction, Vector3 position)
        {
            Vector2 posTo = new Vector2(position.x, position.y) + direction;
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            BoxWallBlock block;
            foreach (var wall in walls)
            {
                if (Mathf.Abs(wall.transform.position.x - posTo.x) < 0.3 && Mathf.Abs(wall.transform.position.y - posTo.y) < 0.3)
                {
                    block.blocked = true;
                    block.blockedByBox = false;
                    return block;
                }
            }
            GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
            foreach (var box in boxes)
            {

                Debug.Log($"Position of box is X {box.transform.position.x} && Position of box is Y {box.transform.position.y}");
                Debug.Log($"Position of PosTo X {posTo.x} && Position of PosTo Y {posTo.y}");
                //Debug.Log($"Difference in X is {Mathf.Abs(box.transform.position.x - posTo.x) < 0.01} && Difference in Y is {Mathf.Abs(box.transform.position.y - posTo.y) < 0.01}");
                if (Mathf.Abs(box.transform.position.x - posTo.x) < 0.3 && Mathf.Abs(box.transform.position.y - posTo.y) < 0.3)
                {
                    Box BlockBox = box.GetComponent<Box>();
                    if (BlockBox)
                    {
                        move.movedBox = BlockBox;
                        move.boxPos = BlockBox.transform.position;
                    }
                    if (BlockBox && BlockBox.Move(direction))
                    {
                        block.blockedByBox = true;
                        block.blocked = false;
                        return block;
                    }
                    else
                    {
                        block.blockedByBox = true;
                        block.blocked = true;
                        return block;
                    }
                }
            }
            block.blocked = false;
            block.blockedByBox = false;
            return block;
        }
    }
}