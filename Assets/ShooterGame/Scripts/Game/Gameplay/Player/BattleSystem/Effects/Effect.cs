using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public float effectDuration;
    float effectTimer = 0;

    protected virtual void Start()
    {
        effectTimer = effectDuration;
    }

    protected virtual void Update()
    {
        effectTimer -= Time.deltaTime;
        RemoveEffect();
    }

    void RemoveEffect(){
        if(effectTimer <= 0)
        Destroy(this);
    }
}
