using UnityEngine;

public class Enemy : Characters
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider _other)
    {
        base.OnTriggerEnter(_other);
        if (_other.CompareTag("Attack_Player"))
        {
            life -= 1;
            Debug.Log("Inimigo acertado!!!! Vida atual: " + life);
            CheckLife();
        }
    }

    protected override void CheckLife()
    {
        base.CheckLife();
        if (life <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
}
