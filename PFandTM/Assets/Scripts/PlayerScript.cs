using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text win;
    public Text lose;
    public Text lives;
    private int scoreValue = 0;
    private int livesValue = 3;
    public AudioClip WinMusic;
    public AudioSource musicSource;
    Animator anim;
    private bool facingRight = true;

    // Start is called before the first frame update
  void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        anim = GetComponent<Animator>();
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        
        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
         }
     if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
         }
         if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
         }
     if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
    }
        if (Input.GetKeyDown(KeyCode.W))
        {
          anim.SetInteger("State", 2);
         }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
          anim.SetInteger("State", 0);
         }
    

    if (facingRight == false && hozMovement > 0)
    {
        Flip();
    }
        else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }


    }
    //this is the enemies and the coins
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 5)
            {
                transform.position = new Vector3(28.47f, -1.09f, 00.0f);
                livesValue = 3;
                lives.text = "Lives:" + livesValue.ToString();
            }
            if (scoreValue == 9)
            {
                musicSource.clip = WinMusic;
                musicSource.Play();
                win.text = "Yay! You win! Created by Elisa.";
            }
        }

        if (collision.collider.tag == "enemy")
        {
            livesValue -= 1;
            lives.text = "Lives:" + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (livesValue == 0)
            {
                lose.text = "You lost. Try again.";
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.           
            }
        }
    }

}
