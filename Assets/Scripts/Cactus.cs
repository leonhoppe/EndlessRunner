using UnityEngine;

namespace EndlessRunner {
    public class Cactus : MonoBehaviour {
        public SpriteRenderer spriteRenderer;
        public Sprite[] variants;
        public Sprite bigCactus;
        public Collider2D smallCollider;
        public Collider2D bigCollider;

        public float bigPropability;

        private void Awake() {
            if (Random.value <= bigPropability) {
                spriteRenderer.sprite = bigCactus;
                bigCollider.enabled = true;
                smallCollider.enabled = false;
            }
            else {
                var value = Random.value;
                var increment = 1 / variants.Length;
                for (var i = 0; i < variants.Length; i++) {
                    if (value <= increment * (i + 1)) {
                        spriteRenderer.sprite = variants[i];
                        break;
                    }
                }

                smallCollider.enabled = true;
                bigCollider.enabled = false;
            }
        }
    }
}