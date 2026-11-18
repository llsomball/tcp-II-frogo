using UnityEngine;
using System.Collections;

public class Boss : Enemy
{
    [SerializeField] float timeForAttacks;
    private bool isWaiting;

    [Header("Box Attack")]
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject boxWarningPrefab;
    [SerializeField] private Transform boxSpawnPoint;
    [SerializeField] private int boxAmount = 5;
    [SerializeField] private float boxSpawnInterval = 0.5f;
    [SerializeField] private float boxSpawnHeight = 10f;
    private bool isExecutingAttack;

    [Header("Jump Attack")]
    [SerializeField] private GameObject shockwavePrefab;
    private GameObject jumpWarning;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpDuration = 1.5f;
    [SerializeField] private int maxJumps = 3;
    [SerializeField] private int currentJumps;
    [SerializeField] private float stunDuration = 3f;
    [SerializeField] private float jumpWarningTime = 1f;
    private bool hasLanded;
    public enum BossState
    {
        Idle,
        AttackBoxes,
        CounterAttack,
        JumpAttack,
        Stunned,
        Return,
        Dead
    }

    [Header("Boss Settings")]
    [SerializeField] private BossState currentState;

    private Vector3 initialPosition;
    private Transform player;

    protected override void Awake()
    {
        base.Awake();

        initialPosition = transform.position;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void ChooseAttack()
    {
        int attack = Random.Range(0, 2);

        switch (attack)
        {
            case 0:
                currentState = BossState.AttackBoxes;
                break;

            case 1:
                currentState = BossState.JumpAttack;
                break;
        }
    }
    protected override void Update()
    {
        base.Update();

        switch (currentState)
        {
            case BossState.Idle:
                if (!isWaiting)
                {
                    StartCoroutine(WaitForNextAttack());
                }
                break;

            case BossState.AttackBoxes:
                if (!isExecutingAttack)
                {
                    isExecutingAttack = true;
                    StartCoroutine(AttackWithBoxes());
                }
                break;

            case BossState.CounterAttack:
                break;

            case BossState.JumpAttack:
                if (!isExecutingAttack)
                {
                    isExecutingAttack = true;
                    StartCoroutine(JumpAttack());
                }
                break;

            case BossState.Stunned:
                break;

            case BossState.Return:
                break;

            case BossState.Dead:
                break;
        }
    }

    private void CreateJumpWarning(Vector3 targetPosition)
    {
        Vector3 warningPosition = new Vector3(targetPosition.x,0.01f,targetPosition.z);

        jumpWarning = Instantiate(boxWarningPrefab,warningPosition,boxWarningPrefab.transform.rotation);
    }

    private Vector3 GetJumpDirection(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        direction.y = 0;

        return direction.normalized;
    }

    private Vector3 CalculateJumpVelocity(Vector3 targetPosition)
    {
        Vector3 displacement = targetPosition - transform.position;

        Vector3 velocity = new Vector3(displacement.x / jumpDuration,(displacement.y + 0.5f * Physics.gravity.magnitude * jumpDuration * jumpDuration)/jumpDuration,displacement.z / jumpDuration);

        return velocity;
    }

    private void CreateShockwave()
    {
        Vector3 shockwavePosition = new Vector3(transform.position.x,0.02f,transform.position.z);

        Instantiate(shockwavePrefab, shockwavePosition, shockwavePrefab.transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasLanded = true;
        }
    }

    private IEnumerator WaitForNextAttack()
    {
        isWaiting = true;

        yield return new WaitForSeconds(timeForAttacks);

        isWaiting = false;
        ChooseAttack();
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(timeForAttacks);

        ChooseAttack();
    }
    private IEnumerator AttackWithBoxes()
    {
        for (int i = 0; i < boxAmount; i++)
        {
            Vector3 spawnPosition = player.position + Vector3.up * boxSpawnHeight;

            GameObject box = Instantiate(boxPrefab, spawnPosition, Quaternion.identity);

            Vector3 warningPosition = new Vector3(player.position.x, 0.01f, player.position.z);

            GameObject warning = Instantiate(boxWarningPrefab, warningPosition, boxWarningPrefab.transform.rotation);

            box.GetComponent<BossBox>().SetWarning(warning);

            yield return new WaitForSeconds(boxSpawnInterval);
        }

        isExecutingAttack = false;
        currentState = BossState.Idle;
    }

    private IEnumerator JumpAttack()
    {
        currentJumps = 0;

        for (int i = 0; i < maxJumps; i++)
        {
            Vector3 targetPosition = player.position;

            CreateJumpWarning(targetPosition);

            yield return new WaitForSeconds(jumpWarningTime);

            Destroy(jumpWarning);
            jumpWarning = null;

            Vector3 direction = GetJumpDirection(targetPosition);

            hasLanded = false;

            rb.linearVelocity = CalculateJumpVelocity(targetPosition);

            yield return new WaitUntil(() => hasLanded);

            CreateShockwave();

            currentJumps++;

            yield return new WaitForSeconds(0.5f);
        }
        currentState = BossState.Stunned;

        yield return new WaitForSeconds(stunDuration);

        isExecutingAttack = false;
        currentState = BossState.Idle;
    }
}