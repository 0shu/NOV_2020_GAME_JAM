using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetManager : MonoBehaviour
{
    [Range(0f, 10000f)]
    public float strength = 1f;
    [Range(0.25f, 2.0f)]
    public float ratio = 1f;
    [Range(0f, 200.0f)]
    public float cutoff = 10f;

    public List<Magnetic> magnets = new List<Magnetic>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //New list for only the polar magnets
        List<Magnetic> polar = new List<Magnetic>();
        foreach(Magnetic mag in magnets)
        {
            if (mag.pole != Polarity.Neutral) polar.Add(mag);
        }

        while(polar.Count > 1)
        {
            //Take off the first item
            Magnetic mag = polar[0];
            polar.RemoveAt(0);

            MagInfo info1 = mag.GetInfo();

            //Go through every other item
            foreach(Magnetic net in polar)
            {
                MagInfo info2 = net.GetInfo();

                //Check if they're close enough to bother
                Vector3 separation = info1.pos - info2.pos;
                float magnitude = separation.magnitude;
                if (magnitude >= cutoff) continue;

                magnitude += 1f;
                separation.Normalize();
                float force = (info1.power * info2.power * strength) / (magnitude * magnitude);
                force *= Time.deltaTime;

                if (mag.pole == net.pole)
                {
                    //Repel
                    force *= ratio;
                    info1.rb.AddForce(separation * force);
                    info2.rb.AddForce(-separation * force);
                }
                else
                {
                    //Attract
                    force /= ratio;
                    info1.rb.AddForce(-separation * force);
                    info2.rb.AddForce(separation * force);
                }
            }
        }
    }

    public void AddMagnet(Magnetic magnet)
    {
        //Check if its already in the list if not add it to it
        if(!magnets.Contains(magnet))
        {
            magnets.Add(magnet);
        }
    }
}
