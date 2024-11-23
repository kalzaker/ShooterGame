using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;

    float speed;

    int damage;

    Rigidbody rb;

    bool explodes, bounces, affectedByGravity;

    int bounceNumber;

    GameObject explosionVFX;

    float explosionRange;

    float maxVertSpeed;

    float maxNegativeVertSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        UpdateParameters();
        
        Move();
        Destroy(gameObject, 10f);
    }

    void Update(){

    }

    public void SetParameters(Element element){
        speed = element.projectileSpeed;
        damage = element.rangeDamage;
        explodes = element.explodes;
        bounces = element.bounces;
        bounceNumber = element.bounceNumber;
        explosionVFX = element.explosionVFX;
        affectedByGravity = element.affectedByGravity;
        explosionRange = element.explosionRange;
    }

    void UpdateParameters(){
        rb.useGravity = affectedByGravity;

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
        if(other.gameObject.TryGetComponent<PlayerMovement>(out _))
        {
            return;
        }

        if(other.gameObject.TryGetComponent<Health>(out Health target)){
           target.TakeDamage(damage);
        }

        if(explodes){
            //Instantiate(explosionVFX);
            Explode();
        }

        if(bounces && bounceNumber > 0){
            bounceNumber--;
            return;
        }

        Destroy(gameObject);
    }

    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach(Collider coll in colliders){
            if(coll.gameObject.TryGetComponent<Health>(out Health objHealthComponent)){
                objHealthComponent.TakeDamage(damage);
            }
        }
    }
}
