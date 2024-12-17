using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 5.0f;
    public float obstacleRange = 5.0f;
    private bool _alive = true;
    [Header("Атака")]
    [SerializeField]
    private GameObject[] _fireballsPrefab;
    private GameObject _fireball;
    public float fireRate = 8f;    // Выстрелов в секунду
    public float projectileSpeed = 10f;   // Скорость снаряда
    public float projectileDamage = 8f; //Урон, наносимый снарядами.
    private float _nextFireTime = 0f;
    //private bool _canFire = true; //Убрали флаг
    public float health = 100f;
    public float inaccuracyAngle = 3f;   // Угол неточности
    public float projectileScale = 0.5f;
    private Transform _playerTransform;
    void Start()
    {
        _alive = true;
        // Найти объект игрока и сохранить его transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            _playerTransform = player.transform;
        else
            Debug.LogError("В сцене не найден объект Player с тегом 'Player'!");
    }
    void Update()
    {
        if (_alive)
        {
            Move();
            CheckForObstacles();
            if (Time.time >= _nextFireTime)
                TryAttack();
        }
    }
    void Move()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
    void TryAttack()
    {

        Attack();

    }
    void Attack()
    {
        if (_fireballsPrefab == null || _fireballsPrefab.Length <= 0)
        {
            Debug.LogError($"Не назначены _fireballsPrefab врагу {gameObject.name}. Пожалуйста, назначьте один в инспекторе.");
            return;
        }
        int randFireball = Random.Range(0, _fireballsPrefab.Length);
        _fireball = Instantiate(_fireballsPrefab[randFireball], transform.TransformPoint(Vector3.forward * 1.5f), transform.rotation);

        // Изменяем размер снаряда
        _fireball.transform.localScale = _fireball.transform.localScale * projectileScale;

        //Получаем позицию игрока перед каждым выстрелом
        if (_playerTransform != null)
        {
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;

            // Добавляем случайное отклонение
            float angleX = Random.Range(-inaccuracyAngle, inaccuracyAngle);
            float angleY = Random.Range(-inaccuracyAngle, inaccuracyAngle);
            Quaternion rotation = Quaternion.Euler(angleX, angleY, 0);
            Vector3 inaccurateDirection = rotation * directionToPlayer;


            // Получаем компонент Projectile и устанавливаем ему направление
            Projectile projectileScript = _fireball.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetOwner(gameObject);
                projectileScript.SetDirection(inaccurateDirection);
                projectileScript.speed = projectileSpeed;
                projectileScript.damage = projectileDamage;
                projectileScript.SetEnemyAI(this);
            }
            else
            {
                Debug.LogError("Компонент Projectile не найден на префабе огненного шара");
            }
            //_canFire = false; //Убрали установку флага
        }
        else
        {
            Debug.LogError("Объект Player не назначен врагу: " + gameObject.name);
        }

        _nextFireTime = Time.time + (1f / fireRate);
    }
    /* public void ResetFire()  //Убрали метод
     {
         _canFire = true;
     }*/
    void CheckForObstacles()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance < obstacleRange && !hit.collider.isTrigger)
            {
                AvoidObstacle();
            }
        }
    }
    void AvoidObstacle()
    {
        float angleRotation = Random.Range(-100, 100);
        transform.Rotate(0, angleRotation, 0);
    }
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Враг уничтожен");
        }
        else
        {
            Debug.Log("Враг получил урон: " + damageAmount);
        }
    }
}