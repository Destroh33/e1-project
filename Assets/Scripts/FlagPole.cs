using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NewSceneLoader sceneLoader;
    [SerializeField] private float initialY;
    [SerializeField] private float finalY;
    private float interp = 0.0f;
    [SerializeField] GameObject flag;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerController>().GetScore()>=3)
        {
            StartCoroutine(LowerFlag());
        }
    }
    IEnumerator LowerFlag()
    {
        while(interp< 1.0f)
        {
            interp += Time.deltaTime;
            flag.transform.position = new Vector3(flag.transform.position.x, Mathf.Lerp(initialY, finalY, interp), flag.transform.position.z);
            yield return null;
        }
        sceneLoader.LoadScene();

    }

}
