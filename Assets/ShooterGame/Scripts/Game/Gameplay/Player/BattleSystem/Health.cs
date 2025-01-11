using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float currentHp, maxHp;

    [SerializeField] float invulTime;

    bool canTakeDamage;

    void Start(){
        canTakeDamage = true;
        currentHp = maxHp;
    }

    public void TakeDamage(float value){
        if(!canTakeDamage) return;

        currentHp -= value;
        if(currentHp <= 0)
        {
            Destroy(gameObject);
        }

        if(gameObject.CompareTag("Player")){
            StartCoroutine("InvulTime");
        }   
    }

    public void RecoverHealth(float value){
        currentHp += value;
        if(currentHp >= maxHp)
        {
            currentHp = maxHp;
        }
    }

    IEnumerator InvulTime(){
        canTakeDamage = false;

        yield return new WaitForSeconds(invulTime);

        canTakeDamage = true;
    }
}