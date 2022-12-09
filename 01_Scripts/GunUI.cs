using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    public Image gun1;
    public Image gun2;
    public Image gun3;

    public void setImage(SpriteRenderer gunSprite)
    {
        if(!gun1.IsActive())
        {
            gun1.sprite = gunSprite.sprite;
            gun1.enabled = true;
        }else if(!gun2.IsActive())
        {
            gun2.sprite = gunSprite.sprite;
            gun2.enabled = true;
        }else if(!gun3.IsActive())
        {
            gun3.sprite = gunSprite.sprite;
            gun3.enabled = true;
        }
    }

    public void setImage1(SpriteRenderer gunSprite)
    {
        gun1.sprite = gunSprite.sprite;
        
    }
    public void setImage2(SpriteRenderer gunSprite)
    {
        gun2.sprite = gunSprite.sprite;
    }
    public void setImage3(SpriteRenderer gunSprite)
    {
        gun3.sprite = gunSprite.sprite;
    }
}
