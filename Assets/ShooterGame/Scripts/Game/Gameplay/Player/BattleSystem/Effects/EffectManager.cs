using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private class ActiveEffect{
        public Effect effect;
        public GameObject target;
        public float effectDuration;
        public float tickTime = 0;

        public ActiveEffect(Effect effect, GameObject target, float duration){
            this.effect = effect;
            this.target = target;
            this.effectDuration = duration;
        }
    }

    [SerializeField] List<ActiveEffect> activeEffects = new List<ActiveEffect>();


    public void ApplyEffect(GameObject target, Effect effect){

        if(!effect.stackable)
        {
            foreach(ActiveEffect activeEffect in activeEffects){
                if(activeEffect.target == target && activeEffect.effect == effect){

                    activeEffect.effectDuration += effect.extensionDuration;

                    Debug.Log($"extended {activeEffect}");
                    return;
                }
            }
        }
        ActiveEffect newEffect = new ActiveEffect(effect, target, effect.duration);
        
        activeEffects.Add(newEffect);

        effect.Apply(target);
        Debug.Log("TUT");
    }

    void Update(){
        for(int i = 0; i < activeEffects.Count; i++){
            if(activeEffects[i].target == null){
                RemoveEffect(activeEffects[i]);
                continue;
            }

            ApplyTick(activeEffects[i]);

            activeEffects[i].tickTime += Time.deltaTime;
            activeEffects[i].effectDuration -= Time.deltaTime;

            RemoveEffect(activeEffects[i]);
        }
    }

    void ApplyTick(ActiveEffect activeEffect){
        if(activeEffect.effect.ticking && activeEffect.tickTime >= 1/activeEffect.effect.tickRate){
            activeEffect.effect.Apply(activeEffect.target);
            activeEffect.tickTime = 0;
        }
    }

    void RemoveEffect(ActiveEffect activeEffect){
        if(activeEffect.effectDuration <= 0){
            if(activeEffect.target != null){
                
                activeEffect.effect.Remove(activeEffect.target);
            }
            activeEffects.Remove(activeEffect);
        }
    }
}