using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(PlayerStats))]
public class Player : MonoBehaviour
{
    #region Singleton
    public static Player GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Player>();
        }
        return instance;
    }

    private static Player instance;
    #endregion

    [SerializeField] private float yDeadZone = -10;

    private CharacterController2D controller;
    private PlayerStats playerStats;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool enableMovement;

    // Update is called once per frame
    void Update()
    {
        if(!enableMovement) { return; }

        if(transform.position.y <= yDeadZone)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * playerStats.speed;

        if (Input.GetButtonDown("Jump"))
        {
            controller.m_JumpForce = playerStats.jumpForce;
            jump = true;
        } 
        else if (Input.GetKeyDown(KeyCode.S) && controller.m_Grounded && !controller.fallThrough)
        {
            //StartCoroutine(JumpOff());
        }

        //controller.Move(horizontalMove * Time.deltaTime, crouch, jump);
        //jump = false;

        //if (Input.GetButtonDown("Crouch"))
        //{
        //    crouch = true;
        //}
        //else if (Input.GetButtonUp("Crouch"))
        //{
        //    crouch = false;
        //}

    }

    private void OnIntroComplete()
    {
        enableMovement = true;
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void OnEnable()
    {
        IntroManager.IntroCompleteEvent += OnIntroComplete;
    }

    private void OnDisable()
    {
        IntroManager.IntroCompleteEvent -= OnIntroComplete;
    }

    private IEnumerator JumpOff()
    {
        controller.fallThrough = true;
        controller.m_Grounded = false;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), false);
        controller.fallThrough = false;
    }
}