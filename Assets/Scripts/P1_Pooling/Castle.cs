using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private ObjectPool projectilePool;
    public Projectile Projectile;

    [SerializeField] private Transform _target;
    private int _enemyLayerMask;
    private float _currentCooldown;
    
    const float _maxCooldown = 0.8f;


    void Start()
    {
        this._enemyLayerMask = LayerMask.GetMask("Enemy");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this._target == null)
        {
            AcquireTarget();
        }
        
        if (this._target == null) return;
        
        AcquireTargetIfNecessary();
        TryAttack();
    }

    void AcquireTargetIfNecessary()
    {
        if (!this._target.gameObject.activeSelf)
        {
            AcquireTarget();
        }
    }

    void AcquireTarget()
    {
        this._target = Physics2D.OverlapCircle(this.transform.position, 5f, this._enemyLayerMask)?.transform;
    }

    void TryAttack()
    {
        _currentCooldown -= Time.deltaTime;
        if (!this._target.gameObject.activeSelf || !(_currentCooldown <= 0f)) return; 
        this._currentCooldown = _maxCooldown;
        Attack();
    }

    void Attack()
    {
        PooledObject projectile = projectilePool.RequestPooledObject();
        projectile.transform.position = this.transform.position;
        projectile.transform.rotation = GetTargetDirection();
    }

    Quaternion GetTargetDirection()
    {
        var dir = this._target.transform.position - this.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}