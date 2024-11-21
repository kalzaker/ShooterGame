using UnityEngine;

[System.Serializable]
public class Element
{
    [SerializeField] GameObject meleeAttackPrefab;
    [SerializeField] GameObject rangeAttackPrefab;
    [SerializeField] Effect effect;

    public string elementName;

    public float meleedamage, meleeRange;

    public float rangeDamage;

    float attackCooldown;

    float currentCooldown;

    public float manacost;

    public virtual void MeleeAttack(){

    }

    public virtual void RangeAttack(){

    }

    public void CalculateCooldown(float value){
        attackCooldown += value;
    }

    public void CauseEffect(GameObject target){
        target.AddComponent<Effect>();
    }
}