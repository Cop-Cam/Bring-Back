
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : GenericSingletonClass<PlayerController>
{
    private Vector2 move;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject playerObj;


    [SerializeField] private Animator playerAnimator;
    

    // Start is called before the first frame update
    void Start()
    {
        //Failsafe
        if(playerObj == null)
        {
            playerObj = GameObject.FindWithTag("Player");
            // InputManager.Instance.playerObj = playerObj;
        }
        if(playerSpeed == 0)
        {
            playerSpeed = 5f;
        }
        if(rb == null)
        {
            rb = playerObj?.GetComponent<Rigidbody>();
            
            if(rb == null)
            {
                rb = playerObj?.transform?.parent?.GetComponentInChildren<Rigidbody>();
                
            }
        }
        if(playerAnimator == null)
        {
            playerAnimator = playerObj.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }
    
    //membaca move value berupa vector
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    //menggerakkan player
    void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        //Tujuan awal agar player menghadap arah terakhir pergerakannya
        if(movement != Vector3.zero)
        {
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, Quaternion.LookRotation(movement), 0.15f);

            rb.MovePosition(rb.position + (movement * playerSpeed * Time.deltaTime));
           // m_player.transform.Translate(movement * playerSpeed * Time.deltaTime, Space.World);
            playerAnimator.SetFloat("Speed", movement.sqrMagnitude);
        }

    }
}
