using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int currentHP;
    public int maxHP;
    public HealthBar healthBar;
    public Text textHP;
    public Text textMaxHP;

    public float movementSpeed;
    public Text textMovementSpeed;
    public float frictionForce;

    public int damage;
    public Text textDamage;

    public int power;
    public Text textPower;

    public int points;
    public Text textPoints;
    public Animator aniPoints;

    public int priceToUpDamage;
    public Text textPriceToUpDamage;
    public int priceToUpMovementSpeed;
    public Text textPriceToUpMovementSpeed;
    public int priceToUpAttackSpeed;
    public Text textPriceToUpAttackSpeed;
    public int priceToUpHP;
    public Text textPriceToUpHP;
    public int priceToRecover;
    public Text textPriceToRecover;
    public int priceToUpAmountOfWeapon;
    public Text textPriceToUpAmountOfWeapon;

    public int weaponRotationSpeed;
    public Text textAttackSpeed;
    public int amountOfWeapon;

    public int spellLevel;
    public bool SpellToggle;

    public GameObject camera;
    public Rigidbody2D rg;
    public GameObject spell;
    public GameObject weapon;
    public GameObject individualWeapon;

    private float timeToReloadAddforce;
    private const float DEFAULT_TIME_TO_RELOAD_ADDFORCE = 0.1f;
    private int i;


    void Start()
    {
        timeToReloadAddforce = DEFAULT_TIME_TO_RELOAD_ADDFORCE;
        i = 1;

        currentHP = maxHP;
        healthBar.SetMaxBar(maxHP);

        reloadPriceOfUpgradeMenu();
              
    }

    void Update()
    {
        camera.transform.position = transform.position;

        healthController();
        statsController();

        spell.GetComponent<SpellManager>().setLevel(spellLevel);
        weapon.GetComponent<WeaponManager>().setRotateSpeed(weaponRotationSpeed);
        movementController();
        manageAmountOfWeapon();


        powerController();
        displaySpell();
    }

    public void powerController()
    {
        power = (int)(((damage*30 + weaponRotationSpeed*10)*amountOfWeapon*8/10 + maxHP*6 + movementSpeed*30) /10);

        textPower.text = "POWER: " + power;

        if (power <= 700)
        {
            spellLevel = 1;
        }
        if (power > 700 && power <= 1500)
        {
            spellLevel = 2;
        }
        if(power > 1500 && power <= 3000)
        {
            spellLevel = 3;
        }
        if (power > 3000 && power <= 5500)
        {
            spellLevel = 4;
        }
        if (power > 5500 && power <= 8000)
        {
            spellLevel = 5;
        }
        if (power > 8000 && power <= 10000)
        {
            spellLevel = 6;
        }
        if (power > 10000 && power <= 18000)
        {
            spellLevel = 7;
        }
        if (power > 18000)
        {
            spellLevel = 8;
        }

    }

    public void reloadAmountOfWeapon()
    {
        foreach (Transform child in weapon.transform)
        {
            if (child.name == "WeaponItem(Clone)")
            {
                Destroy(child.gameObject);
            }
        }
        i = 1;
    }
    private void manageAmountOfWeapon()
    {

        float degree = 360 / amountOfWeapon;
        if (i < amountOfWeapon)
        {
            GameObject tempWeapon = Instantiate(individualWeapon, weapon.transform, false);
            tempWeapon.transform.Rotate(new Vector3(0, 0, degree * i));
            i++;
        }
    }

    private void reloadPriceOfUpgradeMenu()
    {
        textPriceToUpDamage.text = "Damage: [" + priceToUpDamage + "]";
        if (movementSpeed < 100)
        {
            textPriceToUpMovementSpeed.text = "M.Speed: [" + priceToUpMovementSpeed + "]";
        }
        else
        {
            textPriceToUpMovementSpeed.text = "M.Speed: [MAX]";
        }
        if (weaponRotationSpeed < 1200)
        {
            textPriceToUpAttackSpeed.text = "A.Speed: [" + priceToUpAttackSpeed + "]";
        }
        else
        {
            textPriceToUpAttackSpeed.text = "A.Speed: [MAX]";
        }
        textPriceToUpHP.text = "Max HP: [" + priceToUpHP + "]";
        textPriceToRecover.text = "Recover: [" + priceToRecover + "]";
        if (amountOfWeapon < 8)
        {
            textPriceToUpAmountOfWeapon.text = "Weapons: [" + priceToUpAmountOfWeapon + "]";
        }
        else
        {
            textPriceToUpAmountOfWeapon.text = "Weapons: [MAX]";
        }
    }

    public void upgradeDamage()
    {
        if (points >= priceToUpDamage)
        {
            points -= priceToUpDamage;
            damage += 7;
            priceToUpDamage += 50;
            reloadPriceOfUpgradeMenu();
        }
        else
        {
            aniPoints.SetTrigger("NotEnought");
        }
    }
    public void upgradeMovementSpeed()
    {
        if (points >= priceToUpMovementSpeed)
        {
            if (movementSpeed < 100) {
                points -= priceToUpMovementSpeed;
                movementSpeed += 5;
                priceToUpMovementSpeed += 100;
                reloadPriceOfUpgradeMenu();
            }
        }
        else
        {
            aniPoints.SetTrigger("NotEnought");
        }
    }
    public void upgradeAttackSpeed()
    {
        if (points >= priceToUpAttackSpeed)
        {
            if (weaponRotationSpeed < 1200)
            {
                points -= priceToUpAttackSpeed;
                weaponRotationSpeed += 20;
                priceToUpAttackSpeed += 75;
                reloadPriceOfUpgradeMenu();
            }
        }
        else
        {
            aniPoints.SetTrigger("NotEnought");
        }
    }
    public void upgradeHP()
    {
        if (points >= priceToUpHP)
        {
            points -= priceToUpHP;
            currentHP += 50;
            maxHP += 50;
            priceToUpHP += 50;
            healthBar.SetMaxBar(maxHP);
            reloadPriceOfUpgradeMenu();
        }
        else
        {
            aniPoints.SetTrigger("NotEnought");
        }
    }
    public void upgradeRecoverHP()
    {
        if (points >= priceToRecover)
        {
            points -= priceToRecover;
            currentHP = maxHP;
            priceToRecover += 250;
            reloadPriceOfUpgradeMenu();
        }
        else
        {
            aniPoints.SetTrigger("NotEnought");
        }
    }
    public void upgradeAmountWeapon()
    {
        if (points >= priceToUpAmountOfWeapon)
        {
            if (amountOfWeapon < 8)
            {
                points -= priceToUpAmountOfWeapon;
                reloadAmountOfWeapon();
                amountOfWeapon++;
                priceToUpAmountOfWeapon *= 2;
                reloadPriceOfUpgradeMenu();
            }
        }
        else
        {
            aniPoints.SetTrigger("NotEnought");
        }
    }
/////////////////////////////////////////////////////////////////////////////////
    public void addPoints(int number)
    {
        points += number;
    }
    private void statsController()
    {
        textAttackSpeed.text = "" + weaponRotationSpeed;
        textDamage.text = "" + damage;
        textMovementSpeed.text = "" + movementSpeed;
        textPoints.text = "" + points;
    }

    private void healthController()
    {
        healthBar.SetBar(currentHP);
        textHP.text = "" + currentHP;
        textMaxHP.text = "" + maxHP;
    }

    private void displaySpell()
    {
        if (SpellToggle)
        {
            spell.SetActive(true);
        }
        else
        {
            spell.SetActive(false);
        }
    }

    private void movementController()
    {
        if (timeToReloadAddforce > 0)
        {
            timeToReloadAddforce -= Time.deltaTime;
        }
        if (timeToReloadAddforce <= 0) {
            if (Input.GetKey(KeyCode.A))
            {
                rg.AddForce(Vector2.left * movementSpeed);
                resetTimeToReloadAddforce(DEFAULT_TIME_TO_RELOAD_ADDFORCE);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rg.AddForce(Vector2.right * movementSpeed);
                resetTimeToReloadAddforce(DEFAULT_TIME_TO_RELOAD_ADDFORCE);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rg.AddForce(Vector2.up * movementSpeed);
                resetTimeToReloadAddforce(DEFAULT_TIME_TO_RELOAD_ADDFORCE);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rg.AddForce(Vector2.down * movementSpeed);
                resetTimeToReloadAddforce(DEFAULT_TIME_TO_RELOAD_ADDFORCE);
            }
        }

        if (rg.velocity != Vector2.zero)
        {
            rg.AddForce(-rg.velocity * (frictionForce));
        }

        if (transform.position.x > 45 || transform.position.x < -45 || 
            transform.position.y > 25 || transform.position.y < -25)
        {
            transform.position = new Vector3(0,0,-3);
        }
        
    }

    public void takeDamage(int amount)
    {
        currentHP -= amount;
    }

    private void resetTimeToReloadAddforce(float t)
    {
        timeToReloadAddforce = t;
    }
}
