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

    public float mana, maxMana;

    [SerializeField] Element[] elements;

    Element currentElement;

    int currentElementNumber;

    void Start(){
        mana = maxMana;
        currentElement = elements[0];
        currentElementNumber = 0;
    }

    void Update(){
        PerformMeleeAttack();
        PerformRangeAttack();
    }

    void PerformRangeAttack(){
        if(Input.GetKey(KeyCode.Mouse0)){
            Debug.Log("RangeAttack");

            currentElement.RangeAttack();

        }
    }

    void PerformMeleeAttack(){
        if(Input.GetKey(KeyCode.Mouse1)){
            Debug.Log("MeleeAttack");

            currentElement.MeleeAttack();
        }
    }

    void SwapElements(){
        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentElementNumber += 1;
            if(currentElementNumber == elements.Length){
                currentElementNumber = 0;
            }

            currentElement = elements[currentElementNumber];
        }
    }

    void CalculateElementsAttackCooldown(float value){
        foreach(Element element in elements){
            element.CalculateCooldown(value);
        }
    }
}