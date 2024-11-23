using UnityEngine;

public class Caster : MonoBehaviour
{

    [SerializeField] Transform castPoint;

    public void MeleeAttack(Element element){

    }


    public void RangeAttack(Element element){
        GameObject bullet = Instantiate(element.projectile, castPoint.position, Quaternion.identity);
        
        Debug.Log(bullet);
        bullet.GetComponent<Projectile>().SetParameters(element);

        bullet.GetComponent<Projectile>().direction = GetLookDirection();

        Debug.Log(GetLookDirection());
        //bullet.GetComponent<Projectile>().Move();
    }

    Vector3 GetLookDirection(){
        return (castPoint.position - transform.position).normalized;
    }
}
