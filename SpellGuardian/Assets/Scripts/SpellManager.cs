using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public float rotateSpeed;
    private int level;
    public Sprite[] spellTier;
    public SpriteRenderer sr;

    public bool isSpellOn;
    void Start()
    {
    }
    void Update()
    {
        standardizeLevel();

        sr.sprite = spellTier[level - 1];
        transform.Rotate(new Vector3(0, 0, -rotateSpeed) * Time.deltaTime);

    }

    private void standardizeLevel()
    {
        if (level > spellTier.Length)
        {
            level = spellTier.Length - 1;
        }
        if (level < 1)
        {
            level = 1;
        }

    }

    public void setLevel(int levelOfCharacter)
    {
        level = levelOfCharacter;
    }
}
