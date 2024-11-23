using UnityEngine;

[System.Serializable]
public class Element
{
    [SerializeField] GameObject meleeAttackPrefab;
    [SerializeField] public GameObject projectile;
    [SerializeField] public Effect effect;
    [SerializeField] Caster caster;

    public string elementName;

    public float meleedamage, meleeRange;

    public float attackCooldown;

    public float currentCooldown;

    public float manacost;

    public bool attackReady;

    [Header("Projectile settings")]
    public int rangeDamage; 
    public float projectileSpeed;

    public bool bounces, explodes, affectedByGravity;

    public int bounceNumber;

    public GameObject explosionVFX;

    public float explosionRange;

    public void CalculateCooldown(float value){
        if(currentCooldown >= attackCooldown){

            attackReady = true;
            return;
        }
        currentCooldown += value;

    }

    public void StartCoolDown(){
        currentCooldown = 0;
        
        attackReady = false;
    }
}