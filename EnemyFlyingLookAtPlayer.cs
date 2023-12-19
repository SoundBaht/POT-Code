using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFlyingLookAtPlayer : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    [SerializeField] private Slider slider;
    private Rigidbody2D rb;
    private MonsterScript monster;


    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private Animator anim;
    private AudioManager audiomanager;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        monster = GetComponent<MonsterScript>();
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isDead();


    }

    public void FlyLookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;

            slider.direction = Slider.Direction.LeftToRight;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;

            slider.direction = Slider.Direction.RightToLeft;
        }
    }

    public void FlyLookBack()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;

            slider.direction = Slider.Direction.LeftToRight;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;

            slider.direction = Slider.Direction.RightToLeft;
        }
    }

    public void shoot()
    {
        timer += Time.deltaTime;

        if (timer > 1.2f)
        {
            timer = 0;
            StartCoroutine(delayBullet());
        }
    }

    IEnumerator delayBullet()
    {
        anim.SetTrigger("Attack");
        audiomanager.PlaySFX(audiomanager.FlyingShootSFX);
        yield return new WaitForSeconds(1f);
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private void isDead()
    {
        if (monster._hp <= 0)
        {
            anim.SetBool("Die" , true);
        }
    }
}
