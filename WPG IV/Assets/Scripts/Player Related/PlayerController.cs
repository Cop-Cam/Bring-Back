using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : GenericSingletonClass<PlayerController>
{
    [SerializeField] private float playerSpeed;
    private Vector2 move;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject m_player;
    

    // Start is called before the first frame update
    void Start()
    {
        //Failsafe
        if(m_player == null)
        {
            m_player = GameObject.FindWithTag("Player");
            InputManager.Instance.playerObj = m_player;
        }
        if(playerSpeed == 0)
        {
            playerSpeed = 5f;
        }
        if(rb == null)
        {
            rb = m_player.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }
    
    //membaca move value berupa vector
    void OnMove(InputAction.CallbackContext context)
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
            m_player.transform.rotation = Quaternion.Slerp(m_player.transform.rotation, Quaternion.LookRotation(movement), 0.15f);

            rb.MovePosition(rb.position + (movement * playerSpeed * Time.deltaTime));
           // m_player.transform.Translate(movement * playerSpeed * Time.deltaTime, Space.World);
        }
    }
}
