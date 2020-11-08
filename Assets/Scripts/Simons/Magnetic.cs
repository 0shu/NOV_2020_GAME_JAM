using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public MagnetManager manager;
    public float power = 1f;
    public Polarity pole = Polarity.Neutral;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if (manager) manager.AddMagnet(this);
        else Debug.LogWarning("Magnet does not have manager!");

        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void SetNeutral() { pole = Polarity.Neutral; }
    public void SetNorth() { pole = Polarity.North; }
    public void SetSouth() { pole = Polarity.South; }

    public void Swap()
    {
        if(pole == Polarity.North) pole = Polarity.South; 
        else if(pole == Polarity.South) pole = Polarity.North;
        else if(pole == Polarity.Neutral)
        {
            Debug.Log("Tried to swap neutral polarity, nothing to swap it to!");
        }
    }

    public MagInfo GetInfo()
    {
        MagInfo val;
        val.pos = transform.position;
        val.power = power;
        val.pole = pole;
        val.rb = rb;

        return val;
    }
}

public struct MagInfo
{
    public Vector3 pos;
    public float power;
    public Polarity pole;
    public Rigidbody rb;
}

public enum Polarity
{
    North,
    South,
    Neutral
}
