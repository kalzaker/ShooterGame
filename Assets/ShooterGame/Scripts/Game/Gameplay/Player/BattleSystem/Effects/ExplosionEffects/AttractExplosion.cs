using UnityEngine;

public class AttractExplosion : BaseExplosion
{
    protected override void ExplosionEffect(IExplodable obj){
        if(obj.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb)){
            rb.AddForce((transform.position - rb.transform.position) * effectStrength, ForceMode.Impulse);
            Debug.Log($"Force applied to {rb.gameObject}");
        }
    }
}
