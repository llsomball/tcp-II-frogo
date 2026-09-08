using Mono.Cecil.Cil;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Characters
{
    [Tooltip("Tempo de demora para executar novamente")]
    [SerializeField] float attackTime;
    [SerializeField] float jumpTime;

    [Tooltip("Componentes externos")]
    [SerializeField] Transform cameraPos;
    [SerializeField] GameObject AttackPlayer;
    [SerializeField] OnGroundCheck groundCheck;


    protected override void Awake()
    {
        base.Awake();
        AttackPlayer.SetActive(false);
        groundCheck = GetComponent<OnGroundCheck>();
    }

    protected override void Update()
    {
        base.Update();
        Walk();
        CheckControllers();
    }
    protected override void Walk()
    {
        base.Walk();
        //Teclas de movimentação
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Localiza a posição da camera 
        Vector3 forward = cameraPos.forward.normalized;
        Vector3 right = cameraPos.right.normalized;

        forward.y = 0;
        right.y = 0;

        //Adapta a movimentação do Player a direção que a camera esta olhando
        Vector3 direction = forward * vertical + right * horizontal;

        rb.linearVelocity = direction.normalized * speed;
    }
    private void CheckControllers()
    {
        //Vendo se o Player 
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump && groundCheck.onGround)
        {
            Jump();
        }
    }

    protected override void Attack()
    {
        base.Attack();
        AttackPlayer.SetActive(true);
        StartCoroutine(AttackCourtine());
    }

    protected override void Jump()
    {
        base.Jump();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }

    private IEnumerator AttackCourtine()
    {
        canAttack = false;
        Debug.Log("Ataque");
        yield return new WaitForSeconds(attackTime);

        AttackPlayer.SetActive(false);
        canAttack = true;
    }
}
