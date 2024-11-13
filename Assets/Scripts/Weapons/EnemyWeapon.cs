using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
public class EnemyWeapon : MonoBehaviour
{
    [Header("EnemyWeapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;
    [Header("EnemyBullets")]
    public EnemyBullet EnemyBullet;
    [SerializeField] private Transform EnemyBulletSpawnPoint;
    [Header("EnemyBullet Pool")]
    private IObjectPool<EnemyBullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    public void Awake()
    {
        objectPool = new ObjectPool<EnemyBullet>(CreateEnemyBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
        timer = 0f;
        if (EnemyBulletSpawnPoint == null)
        {
            EnemyBulletSpawnPoint = transform;  // Sets it to the EnemyWeapon's own position and rotation
        }
    }

    private EnemyBullet CreateEnemyBullet()
    {
        if (EnemyBullet == null)
        {
            Debug.LogError("EnemyBullet prefab is not assigned in the Inspector!");
            return null;
        }
        EnemyBullet EnemyBulletObject = Instantiate(EnemyBullet);
        EnemyBulletObject.ObjectPool = objectPool;
        return EnemyBulletObject;
    }

    private void OnGetFromPool(EnemyBullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(EnemyBullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(EnemyBullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    void FixedUpdate()
    {
        if (Time.time >= timer && EnemyBullet != null)
        {
            EnemyBullet EnemyBulletObject = objectPool.Get();
            if (EnemyBulletObject != null)
            {
                EnemyBulletObject.transform.SetPositionAndRotation(EnemyBulletSpawnPoint.position, EnemyBulletSpawnPoint.rotation);
                timer = Time.time + shootIntervalInSeconds; // Set next time to shoot
            }
        }
    }
}
