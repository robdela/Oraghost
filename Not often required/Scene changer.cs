using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Scenechanger : MonoBehaviour
{
   
    
    [SerializeField] private string scene;
    [SerializeField] public Vector2 spawnPoint;
    [SerializeField] private GameObject canvas;
    private bool shouldWait = false;

    private Rigidbody2D PlayerRigidbody;
    
    private Animator playerAnimator;
    float moveYValue;
    float moveXValue;

    
    public static Scenechanger instance;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadScene(scene, canvas, shouldWait)); // First because the following things can be done while the scene is loading

            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            

            moveYValue = other.GetComponent<Animator>().GetFloat("MoveY");
            moveXValue = other.GetComponent<Animator>().GetFloat("MoveX");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
        }
    }
    public void changeScene(string scene, GameObject canvas, bool shouldWait)
    {
        StartCoroutine(LoadScene(scene, canvas, shouldWait));
    }

    
    private IEnumerator LoadScene(string scene, GameObject canvas, bool shouldWait)
    {
        Debug.Log("test");
        // Initializing to make everything work
        transform.SetParent(null); // make the gameobject root instead of children, otherwise the next line doesn't work
        DontDestroyOnLoad(gameObject);


        // fade in and loading the scene
        GameStateManager.Instance.Loading();
        canvas.GetComponent<Animator>().Play("fade in"); 
        yield return new WaitForSeconds(0.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        yield return new WaitUntil(() => asyncLoad.isDone);

        //Player actions
        GameObject player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
        PlayerRigidbody = player.GetComponent<Rigidbody2D>();

        PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Transform>().position = spawnPoint;
        playerAnimator.SetFloat("MoveY", moveYValue);
        playerAnimator.SetFloat("MoveX", moveXValue);

        if (shouldWait) { yield return new WaitForSeconds(3f); }
        
        // fade out and unlocking the player
        canvas.GetComponent<Animator>().Play("fade out");
        yield return new WaitForSeconds(0.5f);
        PlayerRigidbody.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        GameStateManager.Instance.Playing();
        yield return new WaitForSeconds(0.1f);
        PlayerRigidbody.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        
        Destroy(gameObject);
        

    }
}
