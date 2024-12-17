using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifetime;
    private Vector3 direction;
    private GameObject owner;
    private EnemyAI _enemyAI;
    private float timeElapsed;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        timeElapsed += Time.deltaTime;

        if (timeElapsed > lifetime)
        {
            DestroyProjectile();
        }
    }
    public void SetEnemyAI(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI;
    }
    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    private void DestroyProjectile()
    {
        /* if (_enemyAI != null)   //Убрали проверку и вызов метода
          {
              _enemyAI.ResetFire();
          }*/
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        // Проверка столкновения с врагом
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null && owner != other.gameObject)
        {
            enemy.TakeDamage(damage);
            DestroyProjectile();
            return;
        }

        // Проверка столкновения с игроком
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("PlayerHealth Found");
            playerHealth.TakeDamage(damage);
            DestroyProjectile();
            return;
        }
        DestroyProjectile();
    }
}