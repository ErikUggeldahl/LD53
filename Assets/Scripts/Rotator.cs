using UnityEngine;

public class Rotator : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up * (45f * Time.deltaTime));
    }
}
