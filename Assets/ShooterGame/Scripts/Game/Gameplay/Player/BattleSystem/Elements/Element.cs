using UnityEngine;

[CreateAssetMenu(menuName = "Element", fileName = "Element")]
public class Element : ScriptableObject
{
    [SerializeField] GameObject meleeAttackPrefab;
    
    public GameObject projectile;

    public Effect[] effects;

    public string elementName;

    public float meleedamage, meleeRange;

    public float attackCooldown;

    public float currentCooldown;

    public float manacost;

    public bool attackReady;

    [Header("Projectile settings")]
    public int rangeDamage; 

    public float projectileSpeed;

    public bool bounces, affectedByGravity;

    public int bounceNumber;

    [Header("Explosion settings")]
    public bool explodes;

    public GameObject explosionPrefab;

    public bool instant;

    public bool affectsPlayer;

    public bool damagesPlayer;

    public bool castsElementalDebuff;

    public float explosionRadius;

    public float effectStrength;

    public float explosionDuration;

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