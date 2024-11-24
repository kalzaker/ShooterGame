using UnityEngine;

public class ExampleShoot : MonoBehaviour
{
    private Camera _cam;
    private float _shootRange = 100f;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _shootRange))
        {
            Debug.Log(hit.transform.name);
            
            EnemyBase enemy = hit.transform.GetComponent<EnemyBase>();
            enemy?.TakeDamage(55f);
        }
    }
}
