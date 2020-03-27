using UnityEngine;

[System.Serializable]
public class PlayerDestroyedHandler
{
    #region Unity Exposed Variables
    
    [SerializeField] private GameObject destroyedParticle;
    [SerializeField] private GameObject artwork;

    #endregion

    #region Private Variables

    private PlayerParticle particle;

    #endregion

    #region Properties

    public bool IsDestroyed { get; protected set; }

    #endregion

    #region Public Methods

    public void Setup(Transform parent)
    {
        GameObject go = UnityEngine.Object.Instantiate(destroyedParticle, parent) as GameObject;
        particle = go.GetComponent<PlayerParticle>();
        Debug.Log(particle);
        particle.gameObject.SetActive(false);
    }

    public void Execute()
    {
        if (IsDestroyed)
            return;
        
        IsDestroyed = true;

        particle.gameObject.SetActive(true);
        artwork.gameObject.SetActive(false);
        particle.onDisposed.AddListener(OnParticleDisposed);
    }

    #endregion

    #region Private Methods

    private void OnParticleDisposed(PlayerParticle particle)
    {
        particle.onDisposed.RemoveListener(OnParticleDisposed);
        particle.gameObject.SetActive(false);
    }

    #endregion
}
