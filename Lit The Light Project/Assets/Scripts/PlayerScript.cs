using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private Vector3 playerPos;
    private Animator playerAnim;

    public GameObject canvas;

    public GameObject Door;
    public Transform respawn;   //точка, с которой (обычно) начинается уровень и в которую возвращается персонаж, если его разуплотнит
    public LayerMask whatIsGround = 8;  //для определения объектов, являющихся "землёй" для определения доступности бега и прыжков

    // флаги, определяющие состояния персонажа
    public bool isActive = true;    // если персонаж активен, ему доступны передвижения и прыжки
    public bool hasKey = false;     // при наличии кристального ключа персонаж способен меенять свет фонарей
    private bool isFacingRight = true;  // переменная для определения и смены направления персонажа
    public bool isGrounded = false; // есть ли у персонажа почва под ногами
    private bool isJumping = false; /*true, когда нажимается кнопка прыжка при условии, что прыжок доступен;
    этакий костылёк для дабл-джампа; реализация через таймер, пока клавиша прыжка нажата*/
    private bool isJumpAviable = true;  // false, если персонаж совершает прыжок, уже находясь в воздухе; true сразу же по приземлении
    public bool isEnded = false;

    private int COINCounter = 0; // каунтер собранных C.O.I.N
    public float timeJumping = 0;   //таймер прыжка; движение вверх прекращается по превышении выставляемого в условии лимита
    public float maxJumpTime = .4f; //собственно лимит времени прыжка
    public float groundCheckRadius = 0.2f;  //радиус, в котором "ищется" земля под ногами персонажа; реализация через OverlapCircle от ног
    public float runSpeed = 4f; //с этой и следующей переменными всё должно быть и так понятно
    public float jumpSpeed = 8f;

    // перечисление состояний персонажа для передачи в аниматор
    public enum State
    {
        Appear,     //0
        Idle,       //1
        Run,        //2
        Jump,       //3
        Squat,      //4
        Climb,      //5
        Disappear   //6
    }

    // передача текущего состояния персонажа в аниматор
    private State animState
    {
        get => (State)playerAnim.GetInteger("State");
        set => playerAnim.SetInteger("State", (int)value);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        // восстановление данных из сохранения, если оно доступно; иначе обычный старт
        int Level = PlayerPrefs.HasKey("Level") ? PlayerPrefs.GetInt("Level") : 0;
        if (Level == SceneManager.GetActiveScene().buildIndex)
        {
            hasKey = PlayerPrefs.GetInt("hasKey") == 1 ? true : false;
            COINCounter = PlayerPrefs.GetInt("COINCounter");
            if (COINCounter == 3)
            {
                Door.GetComponent<doorScript>().Opened();
            }
            if (PlayerPrefs.GetInt("PlayerFacing") == -1)
                Flip();
            transform.localPosition = new Vector3
                (PlayerPrefs.GetFloat(gameObject.name + "X"), PlayerPrefs.GetFloat(gameObject.name + "Y"), playerPos.z);

            if (PlayerPrefs.GetInt("isActive") == 0)
            {
                Appear();
            }
        }
        else
            Appear();
    }

    //проверка наличия земли под ногами
    void FixedUpdate()
    {
        groundCheck();
        if (isGrounded)
            isJumpAviable = true;
    }

    void Update()
    {

        if (isActive)
        {
            if (isGrounded || PlayerPrefs.GetString("Jumpmode") == "Guided")
            {
                //бег и покой с передачей данных в аниматор
                float moveDirection = Input.GetAxis("Horizontal");
                playerBody.velocity = new Vector2(moveDirection * runSpeed, playerBody.velocity.y);
                if (moveDirection == 0)
                    animState = State.Idle;
                else if (isGrounded)
                    animState = State.Run;
                if ((moveDirection > 0 && !isFacingRight) || (moveDirection < 0 && isFacingRight))
                    Flip();
            }

            //прыжки
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isJumpAviable)
            {
                isJumping = true;
                if (!isGrounded)
                    isJumpAviable = false;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
            {
                isJumping = false;
                timeJumping = 0;
            }
            if (isJumping && timeJumping < maxJumpTime)
            {
                timeJumping += Time.deltaTime;
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpSpeed - timeJumping * 10);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isActive)
            switch (collider.gameObject.tag)
            {
                case "Enemy":
                    Disappear();
                    break;
                case "Lantern":
                    if (hasKey)
                    {
                        Animator animLantern = collider.gameObject.GetComponent<Animator>();
                        animLantern.SetBool("Lit", true);
                    }
                    break;
                case "Respawn":
                    respawn = collider.transform;
                    collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    Save();
                    break;
            }

    }

    private void groundCheck()
    {
        playerPos = GetComponent<Transform>().localPosition;
        isGrounded = Physics2D.OverlapCircle(new Vector2(playerPos.x, playerPos.y - .93f), groundCheckRadius, whatIsGround);
        if (!isGrounded)
        {
            playerAnim.SetFloat("vSpeed", playerBody.velocity.y);
            Jump();
        }
    }

    private void Jump()
    {
        if (isActive)
        {
            animState = State.Jump;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Disappear()
    {
        isActive = false;
        isJumping = false;
        playerBody.velocity = new Vector2(0, 0);
        animState = State.Disappear;
    }

    public void afterDisapper()
    {
        if (isEnded)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
        else
        {
            if (!PlayerPrefs.HasKey("Gamemode") || PlayerPrefs.GetString("Gamemode") == "Normal")
            {
                Appear();
            }
            else if (PlayerPrefs.GetString("Gamemode") == "Hardcore")
            {
                PlayerPrefs.SetInt("Level", 0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void Appear()
    {
        transform.localPosition = respawn.position;
        animState = State.Appear;
    }

    private void Active()
    {
        isActive = true;
    }

    public void GetCOIN()
    {
        COINCounter += 1;
        pauseScript script = canvas.GetComponent<pauseScript>();
        script.COINCounter = COINCounter;
        if (COINCounter == 3)
        {
            Door.GetComponent<doorScript>().Opened();
        }
    }



    // передача ключевых показателей в файл сохранения
    public void Save()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("hasKey", hasKey ? 1 : 0);
        PlayerPrefs.SetInt("COINCounter", COINCounter);
        PlayerPrefs.SetInt("isActive", isActive ? 1 : 0);
        PlayerPrefs.SetInt("PlayerFacing", isFacingRight ? 1 : -1);
        PlayerPrefs.SetFloat(gameObject.name + "X", playerPos.x);
        PlayerPrefs.SetFloat(gameObject.name + "Y", playerPos.y);
    }

}