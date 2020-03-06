using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{   
    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private Animator animator;

    private float moveSpeed = 3f;
    private float bounceSpeed = 4f;

    private Button flapButton;
    private bool didFlap;
    public bool isAlive;

    private GameObject[] backgrounds;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;
        isAlive = true;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(TheBirdFlap);
2

        Array.Sort(backgrounds, new GameObjectHorizontalComparer());
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAlive){
            Vector3 pos = transform.position;
            pos.x += moveSpeed * Time.deltaTime;
            transform.position = pos;

            if(didFlap){
                didFlap = false;
                body.velocity = new Vector3(0, bounceSpeed, 0 );
                animator.SetTrigger("Flap");
            }

            if(body.velocity.y >=0){
                float angle = 0;
                angle = Mathf.Lerp(0, 45, body.velocity.y / 3);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }else{
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -body.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public void TheBirdFlap(){
        didFlap = true;
    }

    public class GameObjectHorizontalComparer : IComparer<GameObject>
    {
        public int Compare(GameObject from, GameObject to)
        {
            if(from.transform.position.x > to.transform.position.x)
                return 1;
            else if (from.transform.position.x < to.transform.position.x)
                return -1;
            else return 0;
        }
    }
}
