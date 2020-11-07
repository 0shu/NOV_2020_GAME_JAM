using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magnetised
{

    private bool polarity { get { return polarity; } set { polarity = value; } }//!<True if polarity is North, False if South

    public abstract void switchPolarity();//!<Abstract method for swapping polarity

    public abstract void repel(GameObject otherActor, Vector3 accel);//!<Abstract method for applying repel force
    public abstract void attract(GameObject otherActor, Vector3 accel);//!<Abstract method for applying attract force
}
