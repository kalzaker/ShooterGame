using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHp, maxHp;
    
    void Start(){
        currentHp = maxHp;
    }

    public void TakeDamage(float value){
        currentHp -= value;
        if(currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void RecoverHealth(float value){
        currentHp += value;
        if(currentHp >= maxHp)
        {
            currentHp = maxHp;
        }
    }
}