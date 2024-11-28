using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [Header("Base parameters")]
    public bool stackable;

    public bool ticking;

    public float tickRate;

    public float duration;


    public float extensionDuration;


    public abstract void Apply(GameObject target);

    public virtual void Remove(GameObject target){
        Debug.Log($"{this} zakon4ilsa na {target}");
    }

    public virtual void Extend(GameObject target, float duration){
        Debug.Log($"{this} prodolzen na {target} na {duration} secund");
    }
}
