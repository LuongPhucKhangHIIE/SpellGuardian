using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private int rotateSpeed;
    private const int individualWeaponSpeed = 400;
    public bool isIndividual;

    public int weaponID;
    public Sprite[] weaponImage;
    private GameObject weaponItem;

    private void Start()
    {
    }

    void Update()
    {
        if (isIndividual)
        {
            transform.Rotate(new Vector3(0, 0, individualWeaponSpeed) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
        }

    }

    public void setRotateSpeed(int Speed)
    {
        rotateSpeed = Speed;
    }
}
