using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0f;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator anim;
    public float fireRate = 0.5f;
    public float nextFire = 0f;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(AttackCo());
        }
        UpdateAnimationAndMove(); 
    }

    private IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);
        yield return null;
        MakeFire();
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
    }

    public void MakeFire()
    {
        Vector2 temp = new Vector2(anim.GetFloat("moveX"), anim.GetFloat("moveY"));
        Fire fire = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Fire>();
        fire.Setup(temp, ChooseArrowDirection());
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(anim.GetFloat("moveX"), -1*anim.GetFloat("moveY"))*Mathf.Rad2Deg;
        return new Vector3(0,0, temp);
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
    }
}
