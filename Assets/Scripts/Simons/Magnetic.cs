using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public MagnetManager manager;
    public float power = 1f;
    public Polarity pole = Polarity.Neutral;
    private Rigidbody rb;
    public MeshRenderer glowMesh;

    public Material northMat;
    public Material southMat;
    public Material neutralMat;

    // Start is called before the first frame update
    void Start()
    {
        if (manager) manager.AddMagnet(this);
        else Debug.LogWarning("Magnet does not have manager!");
        if (!glowMesh) Debug.LogWarning("No mesh renderer attached, waht will glow?");

        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void SetNeutral() 
    { 
        pole = Polarity.Neutral;
        glowMesh.material = neutralMat;
    }

    public void SetNorth() 
    { 
        pole = Polarity.North;
        glowMesh.material = northMat;
    }

    public void SetSouth() 
    { 
        pole = Polarity.South;
        glowMesh.material = southMat;
    }

    public void Swap()
    {
        if (pole == Polarity.North) SetSouth();
        else if (pole == Polarity.South) SetNorth();
        else if (pole == Polarity.Neutral)
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
