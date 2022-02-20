using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    public ShopUIController shop;

    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public LayerMask solidObjectsLayer;
    public LayerMask grassLayer;
    public LayerMask interactableLayer;

    

    // Start is called before the first frame update
    void Start()
    {

        
        if (PlayerPrefs.GetFloat("justReturned") == 1) {
            PlayerIsBack();
            PlayerPrefs.SetFloat("justReturned", 0f);
        }


    }



    void PlayerIsBack() {
        //transform.position.x = PlayerPrefs.GetFloat("X");
        //transform.position.y = PlayerPrefs.GetFloat("Y");
        //transform.position.z = PlayerPrefs.GetFloat("Z");
        transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
        animator.SetFloat("moveX", PlayerPrefs.GetFloat("facingX"));
        animator.SetFloat("moveY", PlayerPrefs.GetFloat("facingY"));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //if you don't want diagonal movement
            if (input.x != 0)
                input.y = 0;

            if (input != Vector2.zero) {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y; //-.5f if you want origin at feet

                if(IsWalkable(targetPos))
                StartCoroutine(Move(targetPos));
              
            }
        }

        animator.SetBool("isMoving", isMoving);
        
        if (Input.GetKeyDown(KeyCode.Z)) {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("ShopScene");
        }

        
    }

    //used to do something over a period of time, moving from current to target pos over time
    IEnumerator Move(Vector3 targetPos) {
        
        isMoving = true;
        
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos) {
        if (Physics2D.OverlapCircle(targetPos, .1f, solidObjectsLayer|interactableLayer) != null) {

            //Debug.Log("can't walk there");
            //SceneManager.LoadScene("Shop");
            SceneManager.LoadScene("ShopScene");

            return false;
            
        }
        return true;
    }

    private void CheckForEncounters() {
        if (Physics2D.OverlapCircle(transform.position, .1f, grassLayer) != null) { 
            if(Random.Range(1,101) <= 30) //we can change 10 to change the odds 
            {
                Debug.Log("Encountered a Pokemon");
                PlayerPrefs.SetFloat("X", transform.position.x);
                PlayerPrefs.SetFloat("Y", transform.position.y);
                PlayerPrefs.SetFloat("Z", transform.position.z);
                PlayerPrefs.SetFloat("facingX", animator.GetFloat("moveX"));
                PlayerPrefs.SetFloat("facingY", animator.GetFloat("moveY"));
                PlayerPrefs.SetFloat("justReturned", 1f);
                SceneManager.LoadScene("Combat Scene");
            }
        }
    }

    
    void Interact() {
        var facingDirection = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDirection;


        var collider = Physics2D.OverlapCircle(interactPos, .3f, interactableLayer);
        if (collider != null) {
            collider.GetComponent<Interactable>()?.Interaction();
        }
        
    }

}
