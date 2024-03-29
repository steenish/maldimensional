﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private GameObject[] platformPrefabs;
    [SerializeField]
    private GameObject spawnPlatform;
    [SerializeField]
    private GameObject goalPlatform;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject portal;
    [SerializeField]
    private int numPlatforms = 5;
    [SerializeField]
    private float intersectMargin = 0.0f;
    [SerializeField]
    private float playerIntersectMargin = 0.0f;
    [SerializeField]
    private int spawnRetries = 10;
#pragma warning restore

    private Bounds spawnBounds;
    private Bounds goalPlatformBounds;
    private Bounds playerBounds;
    private Bounds portalBounds;
    private List<GameObject> platforms;

    void Start() {
        platforms = new List<GameObject>();
        platforms.Add(spawnPlatform);

        spawnBounds = Camera.main.GetComponent<CameraController>().cameraBounds;
        goalPlatformBounds = goalPlatform.GetComponent<SpriteRenderer>().bounds;
        goalPlatformBounds.Expand(intersectMargin);
        portalBounds = portal.GetComponent<SpriteRenderer>().bounds;
        portalBounds.Expand(intersectMargin);
    }

    public void RespawnPlatforms() {
        foreach (GameObject platform in platforms) {
            Destroy(platform);
        }
        platforms.Clear();
        SpawnPlatforms();
    }

    public void SpawnPlatforms() {
        playerBounds = player.GetComponent<SpriteRenderer>().bounds;
        playerBounds.Expand(playerIntersectMargin);

        for (int i = 0; i < numPlatforms; ++i) {
            GameObject platformPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
            GameObject platformInstance = Instantiate(platformPrefab);

            bool hopelessPlatform = false;

            // Try to find a non-intersecting spawn location.
            for (int j = 0; j < spawnRetries; ++j) {
                // Pick a random location within spawn bounds.
                platformInstance.transform.position = HelperFunctions.RandomPointInBounds2D(spawnBounds);
                Bounds currentBounds = platformInstance.GetComponent<SpriteRenderer>().bounds;

                bool intersects = false;

                // Check for intersection with all the already spawned platforms.
                foreach (GameObject platform in platforms) {
                    Bounds platformBounds = platform.GetComponent<SpriteRenderer>().bounds;
                    platformBounds.Expand(intersectMargin);

                    if (currentBounds.Intersects(platformBounds)) {
                        intersects = true;
                    }
                }

                // Check for intersection with the goal platform bounds.
                if (currentBounds.Intersects(goalPlatformBounds)) intersects = true;

                // Check for intersection with the player bounds.
                if (currentBounds.Intersects(playerBounds)) intersects = true;

                // Check for intersection with the portal bounds.
                if (currentBounds.Intersects(portalBounds)) intersects = true;

                // If no intersection was found, the spawn location is okay.
                if (!intersects) break;

                // If the last iteration is reached and there is still intersection, mark platform as hopeless.
                if (j == spawnRetries - 1 && intersects) {
                    hopelessPlatform = true;
                }
            }

            if (hopelessPlatform) {
                DestroyImmediate(platformInstance);
            } else {
                platforms.Add(platformInstance);
                platformInstance.GetComponentsInChildren<PolygonCollider2D>(true)[0].enabled = true;
            }
        }
    }
}
