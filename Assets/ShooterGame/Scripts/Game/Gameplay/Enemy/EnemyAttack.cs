using Fragsurf.Movement;
using UnityEngine;

namespace ShooterGame.Scripts.Game.Gameplay.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private bool canAttack = true;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float agrDistance = 10f;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;


        private Transform _target;
        private float _fireCount = 0;

        private void Awake()
        {
            _target = FindFirstObjectByType<SurfCharacter>().transform;
        }

        private void Update()
        {
            _fireCount -= Time.deltaTime;

            if (Vector3.Distance(transform.position, _target.position) <= agrDistance && _fireCount <= 0 && canAttack)
            {
                _fireCount = fireRate;

                Attack();
            }
        }

        private void Attack()
        {
            var targetDestination = _target.transform.position - transform.position;
            var angle = Vector3.SignedAngle(targetDestination, transform.forward, Vector3.up);

            if (Mathf.Abs(angle) < 45f)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Vector3 shootDirection = (_target.position - firePoint.transform.position).normalized;
                bullet.GetComponent<Rigidbody>().linearVelocity =
                Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * 40;

                EventManager.soundPlayed.Invoke(Clip.Attack, transform.position);
            }
        }
    }
}
