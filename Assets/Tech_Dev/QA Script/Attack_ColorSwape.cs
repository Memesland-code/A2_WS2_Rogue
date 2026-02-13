using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Attack_ColorSwape : MonoBehaviour
{
    [SerializeField] private KeyCode AttackKeybord;
    [SerializeField] private KeyCode AttackControler = KeyCode.JoystickButton2;
    
    private bool ColorChange = false;
    private float changeTime = 0f;
    private const float Delay = 0.1f;
    [SerializeField] private GameObject Sword;
    [SerializeField] private Material mAttack;
    [SerializeField] private Material mBase;
    [SerializeField]  Renderer swordRenderer;


    void Start()
    {
         Sword = GameObject.FindGameObjectWithTag("Sword");
         swordRenderer = Sword.GetComponent<Renderer>();
         swordRenderer.material = mBase;



    }
    void Update()
    {
        if (Sword == null)
        {
            Sword = GameObject.FindGameObjectWithTag("Sword");
            swordRenderer = Sword.GetComponent<Renderer>();
            swordRenderer.material = mBase;
        }

        if (Input.GetKeyDown(AttackControler) || Input.GetKeyDown(AttackKeybord) && Sword != null)
        {
            Debug.Log("Attack");
            changeTime = Time.time + Delay; 
            if (Time.time <= changeTime)
            {
                swordRenderer.material = mAttack;

               ColorChange = true;
               
            }
            
        }
        if (Time.time >= changeTime && ColorChange)
        {
            swordRenderer.material = mBase;
            ColorChange = false;
        }
        }
        
    }

