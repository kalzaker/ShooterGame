using UnityEngine;
using UnityEngine.Events;

public class PlayerBattleController : MonoBehaviour
{
    public static PlayerBattleController Instance;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    Caster caster;

    public float mana, maxMana;

    [SerializeField] Element[] elements;

    Element currentElement;

    int currentElementNumber;

    void Start(){
        caster = GetComponent<Caster>();
        mana = maxMana;
        currentElement = elements[0];
        currentElementNumber = 0;
    }

    void Update(){
        PerformMeleeAttack();
        PerformRangeAttack();
        SwapElements();
        CalculateElementsAttackCooldown(Time.deltaTime);
    }

    void PerformRangeAttack(){
        if(Input.GetKey(KeyCode.Mouse0) && currentElement.attackReady){
            Debug.Log("RangeAttack");

            caster.RangeAttack(currentElement);
            currentElement.StartCoolDown();
        }
    }

    void PerformMeleeAttack(){
        if(Input.GetKey(KeyCode.Mouse1) && currentElement.attackReady){
            Debug.Log("MeleeAttack");

            caster.MeleeAttack(currentElement);
            currentElement.StartCoolDown();    
        }
    }

    void SwapElements(){
        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentElementNumber = (currentElementNumber + 1)%elements.Length;

            currentElement = elements[currentElementNumber];
        }
    }

    void CalculateElementsAttackCooldown(float value){
        foreach(Element element in elements){
            element.CalculateCooldown(value);
        }
    }
}