using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Dot", fileName = "DamageOverTime")]
public class DamageOverTime : Effect
{
    [Header("Special parameters")]
    public float tickDamage;
    GameObject dotVFX;


    public override void Apply(GameObject target){
        Debug.Log($"{tickDamage} по {target}");
        target.GetComponent<Health>().TakeDamage(tickDamage);
    }

    public override void Remove(GameObject target){

    }

    public override void Extend(GameObject target, float extendDuration){
        base.Extend(target, extendDuration);
    }
}
