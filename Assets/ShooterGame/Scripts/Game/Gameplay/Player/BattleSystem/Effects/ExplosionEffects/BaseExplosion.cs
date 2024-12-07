using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class BaseExplosion : MonoBehaviour
{
    GameObject explosionVFX;

    protected bool affectsPlayer;

    protected bool damagesPlayer;

    protected bool instant;

    protected float explosionDuration;

    protected float explosionRadius;

    bool castsElementalDebuff;

    protected float effectStrength;

    Element _element;

    Effect[] effects;


    List<IExplodable> objectsInRange = new List<IExplodable>();

    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider coll in colliders){
            if(coll.gameObject.TryGetComponent(out IExplodable explodable)){
                objectsInRange.Add(explodable);
            }
        }

        if(explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }
    }

    public void SetParameters(Element element)
    {
        instant = element.instant;
        explosionDuration = element.explosionDuration;
        explosionRadius = element.explosionRadius;
        affectsPlayer = element.affectsPlayer;
        damagesPlayer = element.damagesPlayer;
        _element = element;
        effectStrength = element.effectStrength;
        effects = element.effects;
        castsElementalDebuff = element.castsElementalDebuff;
    }

    void Update()
    {
        foreach(IExplodable obj in objectsInRange){
            ExplosionEffect(obj);
        }

        if(instant){
            Destroy(this);
        }

        explosionDuration -= Time.deltaTime;

        if(explosionDuration <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IExplodable>(out IExplodable obj))
        {
            objectsInRange.Add(obj);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<IExplodable>(out IExplodable obj))
        {
            objectsInRange.Remove(obj);
        }
    }

    protected virtual void ExplosionEffect(IExplodable obj)
    {
        if(castsElementalDebuff)
        {
            foreach(Effect effect in _element.effects)
            {
                
                if(obj == null) return;
                EffectManager.instance.ApplyEffect(obj.gameObject, effect);
            }
        }

        Debug.Log("Explosion effect applied");
    }
}
