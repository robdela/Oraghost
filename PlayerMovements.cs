using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] public Transform Transform;
    private bool isMoving;
    [SerializeField] private float speed = 0.02f;
    private GameObject gameobject;
    private bool canInteract = true;
    private Inventory inventory;

    Vector2 movement;
    Vector2 direction;
    
    private void Start()
    {

        if (GameStateManager.Instance.CurrentGameState != GameState.Playing)
        {
            GameStateManager.Instance.Playing();
        }
        

    }

    /*  private IEnumerator Position()
      {
          yield return new WaitForSeconds(1);
          PlayerPrefs.SetFloat("X position", Transform.position.x);
          PlayerPrefs.SetFloat("Y position", Transform.position.y);
          StartCoroutine(Position());


      }*/

    public void Interact()
    {
        StartCoroutine(Interaction());
    }

    private IEnumerator Interaction()
    {
        Vector2 startPos = transform.position;
        LayerMask layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        float moveYValue = GetComponent<Animator>().GetFloat("MoveY");
        float moveXValue = GetComponent<Animator>().GetFloat("MoveX");


        direction = new Vector2(moveXValue, moveYValue);

        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 1f, layerMask);


        if (hit.collider != null && GameStateManager.Instance.CurrentGameState == GameState.Playing && canInteract)
        {
            canInteract = false;
            Debug.Log("Ray hit: " + hit.collider.gameObject.name);
            hit.collider.gameObject.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
            gameobject = hit.collider.gameObject;
            yield return new WaitForSeconds(1);
            canInteract = true;

        }

        if (GameStateManager.Instance.CurrentGameState == GameState.Chest || GameStateManager.Instance.CurrentGameState == GameState.Dialog || GameStateManager.Instance.CurrentGameState == GameState.Menu)
        {
            canInteract = false;
            gameobject.SendMessage("Exit", SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(1);
            canInteract = true;
        }
    }

    private void Update()
    {

            if (movement != Vector2.zero && (GameStateManager.Instance.CurrentGameState == GameState.Playing || GameStateManager.Instance.CurrentGameState == GameState.Battle))
            {
                
                animator.SetFloat("MoveY", movement.y);
                animator.SetFloat("MoveX", movement.x);
            }
        

        if (movement == Vector2.zero) { isMoving = false;}
        else if (movement != Vector2.zero) { isMoving = true;}
        
        animator.SetBool("IsMoving", isMoving);


        

        

    }
    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime );
    }
    public void Move(InputAction.CallbackContext context)
    {

        movement.x = context.ReadValue<Vector2>().x;
        movement.y = context.ReadValue<Vector2>().y;
    }    
}
