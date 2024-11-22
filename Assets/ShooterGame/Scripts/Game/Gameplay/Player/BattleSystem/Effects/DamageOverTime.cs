using UnityEngine;
using System.Collections;

public class DamageOverTime : Effect
{
    GameObject dotVFX;

    Health healthComponent;

    public float tickDamage;

    public float tickRate;

    public bool stackable;

    protected override void Start()
    {
        base.Start();

        healthComponent = GetComponent<Health>();
    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator DamageTick(){
        healthComponent.currentHp -= tickDamage;
        
        yield return new WaitForSeconds(1/tickRate);

        effectDuration -= 1/tickRate;

        StartCoroutine("DamageTick");
    }

    void RefreshDotDuration(){
        if(stackable) return;

        if(TryGetComponent<DamageOverTime>(out DamageOverTime anotherDOT)){
            anotherDOT.effectDuration += effectDuration;
        }
    }
}
