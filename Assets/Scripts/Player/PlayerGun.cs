using System;
using UnityEngine;

[System.Serializable]
public class PlayerGun
{

    #region Unity Exposed Variables
    
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private int intialBulletAmount = 10;
    [SerializeField] private Transform fireLocation;
    [SerializeField] private float fireDelay = 2;

    #endregion

    #region Private Variables

    private IPlayerAttackingHandler handler;
    private float fireTime = 0;
    private ItemPool<Bullet> bulletPool;

    #endregion

    #region Public Methods

    public void Setup(IPlayerAttackingHandler handler)
    {
        bulletPool = new ItemPool<Bullet>(prefabBullet, intialBulletAmount);
        this.handler = handler;
    }

    public void AttemptFire()
    {
        if (handler == null)
            return;
        
        if(handler.IsFiring && fireTime < Time.time)
        {
            fireTime = Time.time + fireDelay;
            Bullet bullet = bulletPool.Borrow();
            bullet.onDispose.AddListener(OnBulletDisposed);
            bullet.transform.SetPositionAndRotation(fireLocation.position, fireLocation.rotation);
            bullet.gameObject.SetActive(true);
        }
    }

    #endregion

    #region Private Methods

    private void OnBulletDisposed(Bullet bullet)
    {
        bullet.onDispose.RemoveListener(OnBulletDisposed);
        bulletPool.Return(bullet);
    }

    #endregion

}
