using UnityEngine;
using UnityEngine.AI;

namespace Platformer.Mechanics
{
    public class Enemy : MonoBehaviour
    {
        public Transform target; // Referencia al jugador
        public float moveSpeed = 2f; // Velocidad de movimiento hacia el jugador
        public float shootRange = 5f; // Rango para disparar
        public GameObject projectilePrefab; // Prefab del proyectil
        public float fireRate = 1f; // Disparos por segundo (1.0 = un disparo por segundo)
        public int maxShots = 5; // M�ximo de disparos antes de retirarse

        private NavMeshAgent agent; // Agente de navegaci�n
        private float nextFireTime = 0f; // Tiempo para el pr�ximo disparo
        private int shotCount = 0; // Contador de disparos

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (target == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            // Si est� dentro del rango de disparo y es el momento de disparar
            if (distanceToPlayer <= shootRange && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; // Actualiza el tiempo para el pr�ximo disparo
            }
            else
            {
                MoveTowardsPlayer();
            }
        }

        protected void MoveTowardsPlayer()
        {
            // Moverse hacia el jugador
            agent.SetDestination(target.position);
            
            /*
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            */
        }

        protected void Shoot()
        {
            // L�gica para disparar al jugador
            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Vector3 direction = (target.position - transform.position).normalized; // Calcular direcci�n hacia el jugador
                projectile.GetComponent<Projectile>().Initialize(direction); // Inicializa el proyectil
            }
        }
    }
}