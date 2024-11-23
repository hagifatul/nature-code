using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Vector2 movement;
    private Rigidbody2D rb;

    [SerializeField] private Animator anime;

    private GoldManager goldManager; // Referensi ke GoldManager
    private Collider2D currentFishCollider; // Collider ikan yang berada dalam jangkauan

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;

        // Cari GoldManager di scene
        goldManager = FindObjectOfType<GoldManager>();
        if (goldManager == null)
        {
            Debug.LogError("GoldManager tidak ditemukan di scene!");
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        FlipAnimation();

        if (Input.GetMouseButtonDown(0) && currentFishCollider != null)
        {
            CatchFish();
        }
    }

    private void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            rb.gravityScale = 0;
            rb.velocity = movement.normalized * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 10;
        }
    }

    private void FlipAnimation()
    {
        if (movement.x != 0)
        {
            anime.SetBool("SwimX", true);
            anime.SetBool("SwimY", false);
            anime.SetBool("Swim", false);

            if (movement.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (movement.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (movement.y > 0)
        {
            anime.SetBool("Swim", true);
            anime.SetBool("SwimX", false);
            anime.SetBool("SwimY", false);
        }
        else if (movement.y < 0)
        {
            anime.SetBool("Swim", false);
            anime.SetBool("SwimX", false);
            anime.SetBool("SwimY", true);
        }
        else
        {
            anime.SetBool("Swim", false);
            anime.SetBool("SwimX", false);
            anime.SetBool("SwimY", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            currentFishCollider = collision; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish") && collision == currentFishCollider)
        {
            currentFishCollider = null;
        }
    }

    private void CatchFish()
    {
        if (goldManager != null && currentFishCollider != null)
        {
            goldManager.ChangeGold(10); 

            
            Destroy(currentFishCollider.gameObject);

            FishSpawner fishSpawner = FindObjectOfType<FishSpawner>();
            if (fishSpawner != null)
            {
                fishSpawner.SpawnFish(); 
            }

            currentFishCollider = null;
        }
    }

}
