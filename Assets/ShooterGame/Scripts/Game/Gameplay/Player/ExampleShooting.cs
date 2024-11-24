using UnityEngine;

public class ExampleShooting : MonoBehaviour
{
    private Camera _cam;
    private float _shootRange = 1000f;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, _shootRange))
        {
            Debug.Log(hit.transform.name);

            EnemyBase enemy = hit.transform.GetComponent<EnemyBase>();
            
            enemy?.TakeDamage(55f);
        }
    }
}
