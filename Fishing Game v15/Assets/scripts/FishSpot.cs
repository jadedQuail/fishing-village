using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator fishAnim;

    [Header("Attributes")]
    [SerializeField] int numOfAnimations;
    [SerializeField] float lifeCycleMin;    // Minimum time before fish disappears
    [SerializeField] float lifeCycleMax;    // Maximum time before fish disappears

    GameObject attachedFish;                // The fish that is attached to this fish spot

    float lifeCycleActual;
    bool pauseLifeCycle = false;

    // References
    GameObject player;
    HUD hud;
    Bobber bobberScript;
    PlayerFishing playerFishing;

    bool bobberFound = false;

    GameObject spawnPoint;                  // The spawn point that this fish spawned into and is actively using

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
        playerFishing = player.GetComponent<PlayerFishing>();

        // Select a random swimming animation
        int animSelection = Random.Range(1, numOfAnimations + 1);
        if (animSelection == 1) { fishAnim.SetTrigger("swimming1"); }
        else if (animSelection == 2) { fishAnim.SetTrigger("swimming2"); }
        else if (animSelection == 3) { fishAnim.SetTrigger("swimming3"); }
        else if (animSelection == 4) { fishAnim.SetTrigger("swimming4"); }
        else { fishAnim.SetTrigger("swimming5"); }

        // Select random life cycle time (in seconds) for this fish spot
        lifeCycleActual = Random.Range(lifeCycleMin, lifeCycleMax);
    }

    private void Update()
    {
        // Find the bobberScript when it activates, then stop calling
        if (!bobberFound)
        {
            bobberScript = player.GetComponentInChildren<Bobber>();
            
            if (bobberScript != null)
            {
                bobberFound = true;
            }
        }

        // Count down this fish's life cycle, then despawn it
        if (!pauseLifeCycle) { lifeCycleActual -= Time.deltaTime; }
        if (lifeCycleActual <= 0f)
        {
            fishAnim.SetTrigger("despawn");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bobber"))
        {
            // For the very rare case where OnTriggerEnter2D is called before Update's first call:
            if (bobberScript == null) { bobberScript = player.GetComponentInChildren<Bobber>(); }
            
            // Stop the bobber from moving
            bobberScript.SetBobberVelocity(new Vector2(0f, 0f));

            // Pause the fish's animator and the bobber animator
            fishAnim.speed = 0f;
            bobberScript.ToggleBobberAnim(false);

            // Pause the fish's life cycle
            pauseLifeCycle = true;

            // Enable the player's surprise emote
            playerFishing.ToggleSurpriseEmote(true);

            // Give this GameObject to the fishing controller
            playerFishing.SetHookedFish(gameObject);
        }
    }

    // PUBLIC FUNCTIONS

    // Function that sets the attached fish
    public void SetAttachedFish(GameObject selection)
    {
        attachedFish = selection;
    }

    // Function that instantiates the attached fish
    public void InstantiateAttachedFish()
    {
        Instantiate(attachedFish, transform.position, transform.rotation);
    }

    // Function that destroys this fish spot
    public void DestroyFishSpot()
    {
        // Before destroying the fish spot, free up the spawn in the fish spot's spawn manager
        SpawnManager spawnManager = spawnPoint.GetComponent<FishSpawn>().GetSpawnManager();
        spawnManager.FreeUpSpawn(spawnPoint);

        Destroy(gameObject);
    }

    // Function that sets this fish spot's spawn point
    public void SetSpawnPoint(GameObject spawn)
    {
        spawnPoint = spawn;
    }

    // Function that gets this fish spot's spawn point
    public GameObject GetSpawnPoint()
    {
        return spawnPoint;
    }
}
