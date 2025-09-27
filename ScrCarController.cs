using UnityEngine;

public class ScrCarController : MonoBehaviour
{
    public ScrWheel[] wheels;

    [Header("Car Specs")]
    public float wheelBase;
    public float rearTrack;
    public float turnRadius;

    [Header("Inputs")]
    public float steerInput;

    private float ackermanAngleLeft;
    private float ackermanAngleRight;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        steerInput = Input.GetAxis("Horizontal");

        // Calculate Ackerman steering angles based on input direction
        if (steerInput > 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if (steerInput < 0)
        {
            ackermanAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            ackermanAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
        }
        else
        {
            ackermanAngleLeft = 0;
            ackermanAngleRight = 0;
        }

        //Apply steering angles to the appropriate front wheels
        foreach (ScrWheel w in wheels)
        {
            if (w.wheelFrontLeft)
                w.steerAngle = ackermanAngleLeft;
            if (w.wheelFrontRight)
                w.steerAngle = ackermanAngleRight;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCar();
        }
    }

    //Reset the car position and orientation when R is pressed
    void ResetCar()
    {
        transform.position += Vector3.up * 2f;
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}