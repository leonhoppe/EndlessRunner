using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace EndlessRunner {
    public class Ground : MonoBehaviour {
        public Transform[] grounds;
        public float speed;
        public float groundLength;
        public GameObject cactus;
        public GameObject bird;
        public float enemySpawnInterval;
        public float spawnChangeRate;
        public float minSpawnInterval;
        public float birdSpawnInterval;

        [NonSerialized] public bool GameStarted;
        [NonSerialized] public bool GameEnded;
        [NonSerialized] public readonly Vector3 Direction = new(-1, 0, 0);
        
        private float _teleportThreshold;
        private float _spawnTime;
        private float _nextSpawn;
        private float _birdSpawnTime;
        private float _nextBirdSpawn;

        private void Start() {
            var sumLength = grounds.Length * groundLength;
            _teleportThreshold = sumLength * 0.5f;

            _nextSpawn = enemySpawnInterval + Random.value;
            _nextBirdSpawn = birdSpawnInterval + Random.value;
        }

        private void Update() {
            if (Input.anyKeyDown) GameStarted = true;

            if (GameEnded && Input.anyKeyDown) {
                SceneManager.LoadScene(gameObject.scene.name);
            }
            
            if (!GameStarted || GameEnded) return;
            _spawnTime += Time.deltaTime;
            _birdSpawnTime += Time.deltaTime;
            var deltaSpeed = Direction * (speed * Time.deltaTime);
            
            foreach (var ground in grounds) {
                ground.position += deltaSpeed;

                if (ground.position.x < -_teleportThreshold) {
                    var pos = ground.position;
                    pos.x = _teleportThreshold;
                    ground.position = pos;
                }
            }

            if (_spawnTime > _nextSpawn) {
                var enemy = Instantiate(cactus);
                enemy.GetComponent<Enemy>().ground = this;
                _spawnTime = 0;
                _nextSpawn = enemySpawnInterval + Random.value;
            }

            if (_birdSpawnTime > _nextBirdSpawn) {
                var enemy = Instantiate(bird);
                enemy.GetComponent<Enemy>().ground = this;
                _birdSpawnTime = 0;
                _nextBirdSpawn = birdSpawnInterval + Random.value;
            }

            if (enemySpawnInterval > minSpawnInterval) {
                enemySpawnInterval -= spawnChangeRate * Time.deltaTime;
            }
        }
    }
}