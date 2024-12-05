using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    [Header("Player Interaction")]
    public Transform player; // Objek pemain
    public float fleeRadius = 3f; // Jarak ikan mulai menjauh
    public float fleeSpeed = 4f; // Kecepatan saat lari menjauh

    [Header("Normal Behavior")]
    public float normalSpeed = 2f;
    public float directionChangeInterval = 2f;

    [Header("Boundary Settings")]
    public BoxCollider2D boundaryCollider; // Collider pembatas

    private Vector2 targetDirection; // Arah gerak acak
    private Rigidbody2D rb; // Untuk menggerakkan objek dengan Rigidbody
    private Animator animator; // Referensi Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Ambil Animator
        StartCoroutine(ChangeDirectionRoutine());
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < fleeRadius)
        {
            // Aktifkan animasi flee
            animator.SetBool("isFleeing", true);
            FleeFromPlayer();
        }
        else
        {
            // Matikan animasi flee (kembali ke idle)
            animator.SetBool("isFleeing", false);
            MoveInRandomDirection();
        }
    }
        else
        {
            Debug.LogWarning("Player masih belum diatur, ikan hanya akan bergerak secara normal.");
            MoveInRandomDirection();
}

RestrictMovementWithinBoundary();
    }

    void FleeFromPlayer()
{
    if (player != null) // Pastikan player tidak null
    {
        Vector2 fleeDirection = ((Vector2)transform.position - (Vector2)player.position).normalized;
        rb.velocity = fleeDirection * fleeSpeed;

        Debug.Log("Ikan sedang lari menjauh dari pemain!");
        // Perbarui arah ikan berdasarkan arah flee
        UpdateFishDirection(fleeDirection);
    }

    void MoveInRandomDirection()
    {
        rb.velocity = targetDirection * normalSpeed;

        // Perbarui arah ikan berdasarkan gerakan acak
        UpdateFishDirection(targetDirection);
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            PickRandomDirection();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void PickRandomDirection()
    {
        targetDirection = Random.insideUnitCircle.normalized;
    }

    void RestrictMovementWithinBoundary()
    {
        if (boundaryCollider != null)
        {
            Bounds bounds = boundaryCollider.bounds;

            // Ambil posisi saat ini
            Vector3 currentPosition = transform.position;

            float clampedX = Mathf.Clamp(currentPosition.x, bounds.min.x, bounds.max.x);
            float clampedY = Mathf.Clamp(currentPosition.y, bounds.min.y, bounds.max.y);

            transform.position = new Vector3(clampedX, clampedY, currentPosition.z);
        }
        else
        {
            Debug.LogWarning("Boundary Collider belum diatur! Ikan tidak akan dibatasi pergerakannya.");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fleeRadius);

        if (boundaryCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(boundaryCollider.bounds.center, boundaryCollider.bounds.size);
        }
    }

    void UpdateFishDirection(Vector2 movementDirection)
    {
        Vector3 currentScale = transform.localScale;

        if (movementDirection.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
        else if (movementDirection.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }
}