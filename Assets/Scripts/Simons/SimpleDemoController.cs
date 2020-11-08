using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDemoController : MonoBehaviour
{
    [Range(0f, 1000f)]
    public float movePower = 100f;
    public float topVelocity = 10f;

    public GameObject Player1;
    public GameObject Player2;

    public Material northMat;
    public Material southMat;

    public MeshRenderer p1Mesh;
    public MeshRenderer p2Mesh;

    private Rigidbody p1Body;
    private Rigidbody p2Body;

    private Magnetic p1Mag;
    private Magnetic p2Mag;

    // Start is called before the first frame update
    void Start()
    {
        p1Body = Player1.GetComponent<Rigidbody>();
        p2Body = Player2.GetComponent<Rigidbody>();

        p1Mag = Player1.GetComponent<Magnetic>();
        p2Mag = Player2.GetComponent<Magnetic>();

        //if (!p1Mesh) p1Mesh = Player1.GetComponent<MeshRenderer>();
        //if (!p2Mesh) p2Mesh = Player2.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        p1Body.AddForce((Vector3.back * movePower * Input.GetAxis("HorizontalPlayer1")) + (Vector3.right * movePower * Input.GetAxis("VerticalPlayer1")));
        p2Body.AddForce((Vector3.back * movePower * Input.GetAxis("HorizontalPlayer2")) + (Vector3.right * movePower * Input.GetAxis("VerticalPlayer2")));

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            p1Mag.Swap();
            if (p1Mag.pole == Polarity.North) p1Mesh.material = northMat;
            else if (p1Mag.pole == Polarity.South) p1Mesh.material = southMat;
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            p2Mag.Swap();
            if (p2Mag.pole == Polarity.North) p2Mesh.material = northMat;
            else if (p2Mag.pole == Polarity.South) p2Mesh.material = southMat;
        }
    }
}
