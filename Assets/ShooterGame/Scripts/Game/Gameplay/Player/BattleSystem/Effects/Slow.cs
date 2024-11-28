using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Slow", fileName = "Slow")]
public class Slow : Effect
{
    [Header("Special parameters")]
    [SerializeField] float slow;
    GameObject slowVFX;
    float startSpeed;

    public override void Apply(GameObject target){
        Debug.Log($"{target} замедлен на {slow * 100}%");
        if(target.TryGetComponent<EnemyBase>(out EnemyBase enemy)){
            enemy.ChangeSpeed(1 - slow/100);
        }
    }

    public override void Remove(GameObject target){
        if(target.TryGetComponent<EnemyBase>(out EnemyBase enemy)){
            enemy.ChangeSpeed(1/(1-slow/100));
        }
    }

    public override void Extend(GameObject target, float extendDuration){
        base.Extend(target, extendDuration);
    }
}
