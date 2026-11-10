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
        int attack = Random.Range(0, 1);

        switch (attack)
        {
            case 0:
                currentState = BossState.AttackBoxes;
                break;

            case 1:
                currentState = BossState.CounterAttack;
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
                // Contra-ataque
                break;

            case BossState.JumpAttack:
                // Ataque de salto
                break;

            case BossState.Stunned:
                // Boss vulnerável
                break;

            case BossState.Return:
                // Retorno ao centro da arena
                break;

            case BossState.Dead:
                // Boss derrotado
                break;
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
}