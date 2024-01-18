using UnityEngine;

namespace EndlessRunner {
    public class Dino : MonoBehaviour {
        public Ground ground;
        public Animator animator;
        public Rigidbody2D physics;
        public Collider2D groundCollider;
        public Collider2D normalCollider;
        public Collider2D dodgeCollider;
        public float jumpForce;

        private bool _gameStarted = false;

        private void Update() {
            if (ground.GameEnded) return;
            
            if (ground.GameStarted && !_gameStarted) {
                animator.SetTrigger("StartGame");
                _gameStarted = true;
            }

            if (Input.GetButton("Dodge")) {
                animator.SetBool("Dodge", true);
                normalCollider.enabled = false;
                dodgeCollider.enabled = true;
            }
            else {
                animator.SetBool("Dodge", false);
                normalCollider.enabled = true;
                dodgeCollider.enabled = false;
            }

            if (Input.GetButtonDown("Jump") && physics.IsTouching(groundCollider)) {
                physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            ground.GameEnded = true;
            animator.SetTrigger("GameOver");
        }
    }
}