using UnityEngine;

public class BulletTargetHeart : MonoBehaviour
{
    public HotGuy hotGuy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetShot()
    {
        if (!hotGuy.isDead)
        {
            hotGuy.isInLove = true;
        }
    }
}
