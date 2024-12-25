using UnityEngine;

public class BlackHoleExplosion : BaseExplosion
{
    protected override void ExplosionEffect(IExplodable obj){
        if(obj.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb)){
            rb.AddForce(CalculateForceDirection(transform.position, obj.gameObject.transform.position) * effectStrength, ForceMode.Impulse);
        }
    }

    Vector3 CalculateForceDirection(Vector3 pos1, Vector3 pos2)
    {
        Vector3 result = Vector3.zero;

        result = (pos1 - pos2)/Vector3.Distance(pos1, pos2);

        return result;
    }
}
