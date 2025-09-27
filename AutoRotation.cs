using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    //Rotates the car in the menu
    private Vector3 rotationSpeed = new Vector3(0, 30f, 0);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
