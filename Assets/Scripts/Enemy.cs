using UnityEngine;

namespace EndlessRunner {
    public class Enemy : MonoBehaviour {
        public Ground ground;
        public float speedMultiplier;

        private void Update() {
            if (!ground.GameStarted || ground.GameEnded) return;
            transform.position += ground.Direction * (ground.speed * speedMultiplier * Time.deltaTime);

            if (transform.position.x < ground.groundLength * -1.5) {
                Destroy(gameObject);
            }
        }
    }
}