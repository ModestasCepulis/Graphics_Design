using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outcode : MonoBehaviour
{

    public bool up;
    public bool down;
    public bool left;
    public bool right;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Outcode()
    {
        //The constructors are automatically defined as false
    }

    public Outcode(Vector2 vPoint)
    {
        up = vPoint.y > 1;
        down = vPoint.y < -1;
        left = vPoint.x < -1;
        right = vPoint.x > 1;
    }

    public Outcode(bool UP, bool DOWN, bool LEFT, bool RIGHT)
    {
        up = UP;
        down = DOWN;
        left = LEFT;
        right = RIGHT;
    }


    public static bool operator == (Outcode a, Outcode b)
    {
        return (a.up == b.up) && (a.down == b.down) && (a.left == b.left) && (a.right == b.right);
    }

    public static bool operator != (Outcode a, Outcode b)
    {
        return !(a == b);
    }

    public static bool operator +(Outcode a, Outcode b)
    {
        return new Outcode((a.up || b.up),(a.down || b.down), (a.left || b.left), (a.right || b.right));
    }

    public static bool operator *(Outcode a, Outcode b)
    {
        return new Outcode((a.up && b.up), (a.down && b.down), (a.left && b.left), (a.right && b.right));
    }


    public void printOutcode()
    {
        Debug.Log("U: "+ up + " D: " + down + " L: " + left + " R: " + right);
    }
}
