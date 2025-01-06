using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.Animations;

namespace Platformer.Mechanics
{
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        public float maxSpeed = 7;
        public float baseJumpSpeed = 7; // Velocidad base de salto
        public float maxJumpMultiplier = 2.5f; // Multiplicador máximo para la altura del salto

        public float currentHealth = 100f;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;

        public Collider2D collider2d;
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        private bool jump;
        private Vector2 move;
        private SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        // Estado para seguir la plataforma
        private bool isAttachedToPlatform = false;


        public Bounds Bounds => collider2d.bounds;

        public void AttachToPlatform(Transform platform)
        {
            isAttachedToPlatform = true;
            animator.SetBool("isRunning", true);
            transform.SetParent(platform);
        }

        public void DetachFromPlatform()
        {
            isAttachedToPlatform = false;
            animator.SetBool("isRunning", false);
            transform.SetParent(null);
        }

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
           
            // Lógica para manejar la muerte del jugador
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            gameObject.SetActive(false); // Desactiva el jugador o cualquier otra lógica de muerte
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");

                // Verificar si el salto fue presionado y el personaje está en el suelo
                if (Input.GetButtonDown("Jump") && jumpState == JumpState.Grounded)
                {
                    jumpState = JumpState.PrepareToJump;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }

            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;

                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;

                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;

                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump)
            {
                // Calcular el multiplicador de salto basado en la velocidad horizontal
                float jumpMultiplier = Mathf.Clamp(Mathf.Abs(move.x) / maxSpeed, 0, maxJumpMultiplier);
                // Ajustar la velocidad de salto con el multiplicador
                velocity.y = baseJumpSpeed * (1 + jumpMultiplier);
                jump = false; // Reseteamos el salto para que no siga saltando
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y *= model.jumpDeceleration; // Aplica desaceleración al salto
                }
            }

            // Voltea el sprite basado en la dirección del movimiento
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            // Actualiza las animaciones
            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}