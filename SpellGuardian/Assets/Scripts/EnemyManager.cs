using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int currentHP;
    public int maxHP;
    public double movementSpeed;
    public int damage;
    public float attackDelay;
    private float defaultAttackDelay;
    public int points;


    public Animator ani;
    public GameObject hitAnimation;

    private GameObject mainCharacter;
    public Collider2D thisCollider;
    public Collider2D mainCharacterCollider;
    void Start()
    {

        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(thisCollider, mainCharacterCollider) ;
        currentHP = maxHP;

        defaultAttackDelay = attackDelay;
    }

    void Update()
    {
        if (currentHP <= maxHP / 2 && currentHP  > maxHP / 4)
        {
            transform.localScale = new Vector2(0.5f, 0.5f);
        }
        else if ( currentHP <= maxHP / 4)
        {
            transform.localScale = new Vector2(0.35f, 0.35f);
        }


        moveToMainCharacter();
        attackMainCharacter();

        dieCondition();
    }

    private void attackMainCharacter()
    {
        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
        }
        
        if (attackDelay <= 0 && Vector2.Distance(transform.position, mainCharacter.transform.position) <= 0.8f)
        {
            mainCharacter.GetComponent<PlayerManager>().takeDamage(damage);
            attackDelay = defaultAttackDelay;
        }
    }

    public void takeDamage(int amount)
    {
        currentHP -= amount;
    }
    private void dieCondition()
    {
        if (currentHP <= 0)
        {
            mainCharacter.GetComponent<PlayerManager>().addPoints(points);
            Destroy(this.gameObject);
        }
    }
    private void moveToMainCharacter()
    {
        if (Vector2.Distance(transform.position, mainCharacter.transform.position) > 0.75f)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                                                    mainCharacter.transform.position,
                                                    (float)(movementSpeed) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            takeDamage(mainCharacter.GetComponent<PlayerManager>().damage);
            ani.SetTrigger("hit");
            GameObject temp = Instantiate(hitAnimation, transform.position, Quaternion.identity);
            Destroy(temp, 0.35f);
        }
    }

}
