using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Sightline sightline;
    [SerializeField] MeshRenderer sightlineMesh;
    [SerializeField] CapsuleCollider collision;

    AudioManager AM;

    PlayerController player;

    bool attacking;
    Vector3 hitPosition;

    float smoothTime = 0.1F;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update

    private void Awake()
    {
        AM = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        attacking = false;
        hitPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            SendRayCast();
        } else {
            transform.position = Vector3.SmoothDamp(transform.position, hitPosition, ref velocity, smoothTime);
        }
    }

    void SendRayCast()
    {
        Vector3 rayCastPos = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(rayCastPos, transform.TransformDirection(Vector3.forward), out hit, sightline.maxLength))
        {
            //Debug.DrawRay(rayCastPos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            sightline.SetDistance(hit.distance);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                attacking = true;
                hitPosition = hit.collider.gameObject.transform.position;
                StartCoroutine(AttackPlayer());
            }
        } else
        {
            sightline.SetDistance(sightline.maxLength);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attacking = true;
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        sightlineMesh.enabled = false;
        collision.enabled = false;
        player.canMove = false;
        AM.PlaySFX("MonsterAttack");
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadNextLevel();
    }
}
