using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;

    float speed;

    int damage;

    Rigidbody rb;

    bool explodes, bounces, affectedByGravity;

    int bounceNumber;

    GameObject explosionPrefab;

    float maxVertSpeed;

    float maxNegativeVertSpeed;

    Effect[] effects;

    Element _element;

    Material material;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        UpdateParameters();
        
        Move();
        Destroy(gameObject, 10f);
    }


    public void SetParameters(Element element){
        speed = element.projectileSpeed;
        damage = element.rangeDamage;
        explodes = element.explodes;
        bounces = element.bounces;
        bounceNumber = element.bounceNumber;
        explosionPrefab = element.explosionPrefab;
        affectedByGravity = element.affectedByGravity;
        effects = element.effects;
        _element = element;
        material = element.projectileMaterial;
    }

    void UpdateParameters(){
        rb.useGravity = affectedByGravity;

        GetComponent<MeshRenderer>().material = material;

        CreatePhysMaterial();
    }

    void CreatePhysMaterial(){
        PhysicsMaterial newMaterial = new PhysicsMaterial("CustomMaterial");

        if(bounces)
        {
            newMaterial.bounciness = 1f;
        }
        else
        {
            newMaterial.bounciness = 0f;
        }

        newMaterial.dynamicFriction = 0.3f;
        newMaterial.staticFriction = 0.4f;
        newMaterial.frictionCombine = PhysicsMaterialCombine.Average;
        newMaterial.bounceCombine = PhysicsMaterialCombine.Maximum;

        GetComponent<Collider>().sharedMaterial = newMaterial;
    }

    public void Move(){
        rb.linearVelocity = new Vector3(direction.normalized.x * speed, direction.y * speed, direction.normalized.z * speed);
    }

    void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.TryGetComponent<EnemyBullet>(out _)) return;

        
        if(other.gameObject.TryGetComponent<IPlayer>(out _))
        {
            return;
        }

        if(other.gameObject.TryGetComponent<Health>(out Health target)){
           target.TakeDamage(damage);
           foreach(Effect effect in effects)
           {
                EffectManager.instance.ApplyEffect(target.gameObject, effect);

           }
        }

        if(explodes){
            Explode();
        }

        if(bounces && bounceNumber > 0){
            bounceNumber--;
            return;
        }

        EventManager.soundPlayed.Invoke(Clip.OrbCollision, transform.position);

        Destroy(gameObject);
    }

    void Explode(){
        Debug.Log(explosionPrefab);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        explosion.GetComponent<BaseExplosion>().SetParameters(_element);
    }
}
