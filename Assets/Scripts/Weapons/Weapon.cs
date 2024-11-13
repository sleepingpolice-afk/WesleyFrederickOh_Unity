using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;
    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    public void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
        timer = 0f;
        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = transform;  // Sets it to the Weapon's own position and rotation
        }
    }

    private Bullet CreateBullet()
    {
        if (bullet == null)
        {
            Debug.LogError("Bullet prefab is not assigned in the Inspector!");
            return null;
        }
        Bullet bulletObject = Instantiate(bullet);
        bulletObject.ObjectPool = objectPool;
        return bulletObject;
    }

    private void OnGetFromPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    void FixedUpdate()
    {
        if (Time.time >= timer && bullet != null)
        {
            Bullet bulletObject = objectPool.Get();
            if (bulletObject != null)
            {
                bulletObject.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                timer = Time.time + shootIntervalInSeconds; // Set next time to shoot
            }
        }
    }
}
