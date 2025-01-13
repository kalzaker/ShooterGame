using UnityEngine;
using System;
using System.Collections.Generic;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

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

    protected float VfxLifeTime;

    Element _element;

    Effect[] effects;

    [SerializeField]Clip clipName;


    [SerializeField]protected List<IExplodable> objectsInRange = new List<IExplodable>();

    void Start()
    {
        EventManager.enemyDied.AddListener(RemoveEnemyFromList);
        CreateCollider();

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider coll in colliders){
            if(coll.gameObject.TryGetComponent(out IExplodable explodable)){

                if(explodable.gameObject == null) continue;
                objectsInRange.Add(explodable);
            }
        }

        EventManager.soundPlayed.Invoke(clipName, transform.position);

        if(explosionVFX != null)
        {
            GameObject explVFX = Instantiate(explosionVFX, transform.position, Quaternion.identity);

            explVFX.AddComponent<VFXDestroyer>();

            explVFX.GetComponent<VFXDestroyer>().DestroyVFX(VfxLifeTime);
        }
    }

    void RemoveEnemyFromList(EnemyBase enemy){
        objectsInRange.Remove(enemy);
    }

    void CreateCollider(){
        gameObject.AddComponent<SphereCollider>();

        SphereCollider coll = GetComponent<SphereCollider>();

        coll.isTrigger = true;
        coll.radius = explosionRadius;
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
        explosionVFX = element.explosionVFX;
        VfxLifeTime = element.VfxLifeTime;
    }

    void Update()
    {
        foreach(IExplodable obj in objectsInRange){
            try{
                ExplosionEffect(obj);
                }
            catch(Exception e){
                continue;
            }
        }

        if(instant){
            Destroy(gameObject);
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
            if(obj == null) return;

            objectsInRange.Add(obj);
            
            Debug.Log("Added");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<IExplodable>(out IExplodable obj))
        {
            objectsInRange.Remove(obj);
            Debug.Log("Removed");
        }
    }

    protected virtual void ExplosionEffect(IExplodable obj)
    {
        
        if(castsElementalDebuff)
        {
            foreach(Effect effect in _element.effects)
            {
                
                if(obj == null)
                {
                    objectsInRange.Remove(obj);
                    return;
                }
                EffectManager.instance.ApplyEffect(obj.gameObject, effect);
            }
        }
    }
}
