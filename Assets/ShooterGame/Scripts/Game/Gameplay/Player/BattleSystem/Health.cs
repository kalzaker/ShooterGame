using UnityEngine;
using System.Collections;
using ShooterGame.Scripts.Game.Gameplay.Enemy;

public class Health : MonoBehaviour
{
    public float currentHp, maxHp;

    [SerializeField] float invulTime;

    bool canTakeDamage = true;

    void Start(){
        canTakeDamage = true;
        currentHp = maxHp;
    }

    public void TakeDamage(float value){
        if(!canTakeDamage) return;

        currentHp -= value;
        if(currentHp <= 0)
        {
            Die();
        }

        if(gameObject.CompareTag("Player")){
            StartCoroutine("InvulTime");
        }

        EventManager.soundPlayed.Invoke(Clip.Hurt, transform.position);
    }
    
    void Die(){

        if(gameObject.CompareTag("Player")){
            Time.timeScale = 0;    
            var GameUI = FindFirstObjectByType<GameUI>();
            GameUI.GameOver();
            
            EventManager.soundPlayed.Invoke(Clip.Die, transform.position);
            return;
        }

        EventManager.soundPlayed.Invoke(Clip.EnemyDie, transform.position);
        EventManager.enemyDied.Invoke(GetComponent<EnemyBase>());

        Destroy(gameObject);
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