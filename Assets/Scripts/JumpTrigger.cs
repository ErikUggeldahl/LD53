using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    public bool Grounded { get; private set; }
    
    private int _groundedParam;

    private void Start()
    {
        _groundedParam = Animator.StringToHash("Grounded");
    }
    private void OnTriggerEnter(Collider other)
    {
        Grounded = true;
        anim.SetBool(_groundedParam, true);
    }

    private void OnTriggerExit(Collider other)
    {
        Grounded = false;
        anim.SetBool(_groundedParam, false);
    }
}
