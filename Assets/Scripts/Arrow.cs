using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform target;
    
    private void Update()
    {
        transform.LookAt(target.position);
    }
}
