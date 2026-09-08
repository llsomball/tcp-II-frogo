using UnityEngine;

public class Characters : MonoBehaviour
{
    [Tooltip("Atributos")]
    [SerializeField] protected int life;
    [SerializeField] protected int attackDamage;
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;

    [Tooltip("Condições")]
    [SerializeField] protected bool canJump;
    [SerializeField] protected bool canWalk;
    [SerializeField] protected bool canAttack;

    [Tooltip("Compoentes externos")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Collider collider;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        if (canWalk)
        {
            Walk();
        }
    }

    // As classes aqui pra baixo são pra  caso fomos fazer outros personagens
    protected virtual void Walk()
    {

    }

    protected virtual void Attack()
    {

    }

    protected virtual void Jump()
    {

    }

    protected virtual void CheckLife()
    {
        if (life == 0)
        {
            canAttack = false;
            canWalk = false;
            canJump = false;
        }
    }

    protected virtual void OnTriggerEnter(Collider _other)
    {

    }
}