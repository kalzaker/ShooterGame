using UnityEngine;

public class NewMonoBehaviourScript : BaseExplosion
{
    protected override void ExplosionEffect(IExplodable obj){
        base.ExplosionEffect(obj);

        if(obj.gameObject.TryGetComponent<Rigidbody>(out Rigidbody objrb)){
            objrb.AddForce(CalculateForce(objrb), ForceMode.Impulse);
        }
        
        Debug.Log($"Explosion effect applied to {obj}");
    }

    Vector3 CalculateForce(Rigidbody obj){
        Vector3 result;
        float distance = Vector3.Distance(obj.transform.position, transform.position);
        result = new Vector3(obj.transform.position.x - transform.position.x, obj.transform.position.y - transform.position.y, obj.transform.position.z - transform.position.z).normalized;
        return result * effectStrength * 40 / distance;
    }
}