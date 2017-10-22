using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    // Rain drop prefab.
    public GameObject rainDrop;

    // Rain drops are spawned randomly within the area defined by 
    // (position.x +- cloudSize, position.y, position.z +- cloudSize).
    public float cloudSize = 10f;

    // Size and speed of rain drops are set randomly
    // between min and max values.
    public float minSize = .02f;
    public float maxSize = .1f;
    public float minSpeed = 15f;
    public float maxSpeed = 30f;

    // Time after which a rain drop is destroyed.
    public float dropLifeTime = 3f;

    // Rain drops per second.
    public float spawnRate = 10f;

    // Next spawn Time.
    private float nextSpawn;

    private void Start()
    {
        // Set first spawn time to Now.
        nextSpawn = Time.time;
    }
    
    private void FixedUpdate()
    {
        // Check if we should spawn next rain drop.
        // If so, do the spawning and set next spawn time.
        // Do it in while loop, since our spawn rate might be 
        // faster than the FixedUpdate rate. This allows us to
        // spawn multiple rain drops if we need.
        while (Time.time > nextSpawn)
        {
            SpawnRainDrop();
            nextSpawn += 1f / spawnRate;
        }
    }
    
    // Spawns a single rain drop at random position, size and velocity
    // (within the predefined bounds).
    public void SpawnRainDrop()
    {
        // Instantiate the prefab and set RainSpawner's transform as parent.
        GameObject spawned = Instantiate(rainDrop, transform);

        // Set the local position randomly inside the "cloud".
        spawned.transform.localPosition = new Vector3(
            Random.Range(-cloudSize, cloudSize),
            0f,
            Random.Range(-cloudSize, cloudSize)
        );

        // Scale the prefab randomly and make it a bit thinner.
        spawned.transform.localScale = new Vector3(.5f, 1f, .5f) * Random.Range(minSize, maxSize);

        // Set the velocioty randomly within predefined bounds.
        Rigidbody body = spawned.GetComponent<Rigidbody>();
        body.velocity = new Vector3(0f, Random.Range(-minSpeed, -maxSpeed), 0f);

        // Set the lifetime of the rain drop.
        Destroy(spawned, dropLifeTime);
    }
}
