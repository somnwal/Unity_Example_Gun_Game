using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSupportOrder : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        sprite.sortingOrder = Mathf.RoundToInt(sprite.transform.position.y * -1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
