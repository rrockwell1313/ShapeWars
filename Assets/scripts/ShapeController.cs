using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShapeController : MonoBehaviour
{
    public int hp;
    public float speed;
    public float bumpForce;
    public GameObject hpTextPrefab;
    public int sortingOrder = 2;
    public int scoreValue;
    public int currencyValue;

    private ScoreManager scoreManager;
    private CurrencyManager currencyManager;

    private Rigidbody2D rb;
    private TextMeshPro hpText;
    private Vector2 targetDirection;
    private bool isBumped;
    private GameObject collidedObject;
    private int currentScore;
    private int currentHp;
    
    void Start()
    {
        bumpForce = Random.Range(1f, 2f);
        scoreManager = FindObjectOfType<ScoreManager>();
        currencyManager = FindObjectOfType<CurrencyManager>();

        rb = GetComponent<Rigidbody2D>();

        // Check for existing HP text objects and destroy them
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("HpText"))
            {
                Destroy(child.gameObject);
            }
        }
        GameObject hpTextObj = Instantiate(hpTextPrefab, transform.position, Quaternion.identity, transform);

        hpText = hpTextObj.GetComponent<TextMeshPro>();
        currentHp = hp;
        hpText.text = currentHp.ToString();

        // Center the HP text
        hpTextObj.transform.localPosition = Vector3.zero;
        hpText.alignment = TextAlignmentOptions.Center;
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);//dont destroy, replace self with shape hit by and reset hp to new shape hp. 
        }
        if (!isBumped)
        {
            MoveTowardsEnemy();
        }
    }

    private void MoveTowardsEnemy()
    {
        // Find all objects with the opposite tag
        string targetTag = gameObject.CompareTag("Player") ? "Player2" : "Player";
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length > 0)
        {
            GameObject closestTarget = null;
            float closestDistance = Mathf.Infinity;

            // Find the closest target
            foreach (GameObject target in targets)
            {
                float distance = Vector2.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            if (closestTarget != null)
            {
                targetDirection = (closestTarget.transform.position - transform.position).normalized;
                rb.velocity = targetDirection * speed;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    /*
     So now, we have a new issue where the new spawn is coming in with two hp stats. 
     */

    void OnCollisionEnter2D(Collision2D collision)
    {
        ShapeController collisionShapeController = collision.collider.GetComponent<ShapeController>();

        if (collision.collider.CompareTag(gameObject.CompareTag("Player") ? "Player2" : "Player"))
        {
            collidedObject = collision.collider.gameObject;
            currentHp -= 1;
            collisionShapeController.hp -= 1;
            UpdateHpText();
            StartCoroutine(BumpBack());

            if (currentHp <= 0)
            {
                // Increment the score of the shape that destroyed this one
                if (collisionShapeController.CompareTag("Player2"))
                {
                    scoreManager.AddScore(true, scoreValue);
                    currencyManager.ChangeCurrency2(currencyValue);
                }
                else
                {
                    scoreManager.AddScore(false, scoreValue);
                    currencyManager.ChangeCurrency1(currencyValue);
                }

                if (collisionShapeController.currentHp > 0)
                {
                    Instantiate(collidedObject, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
        else if (collision.collider.CompareTag("Obstacle"))
        {
            StartCoroutine(BumpBack());
        }
    }


    public void UpdateHpText()
    {
        hpText.text = currentHp.ToString();
    }



IEnumerator BumpBack()
    {
        isBumped = true;
        float randomAngle = Random.Range(-15f, 15f);
        Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
        Vector2 randomizedDirection = rotation * -targetDirection;
        rb.velocity = randomizedDirection * bumpForce;
        yield return new WaitForSeconds(0.25f);
        isBumped = false;
    }
}
