using UnityEngine;

public class ItachiMovement : CharacterController
{

    //public Transform dartPoint;
    //public Transform attackPoint;
    //public float punchRange = 0.3f;
    //public float kickRange = 0.5f;
    // public GameObject[] darts;

    public GameObject special1s;
    public GameObject special2s;
    public Transform special1Point;
    public Transform special2Point;


     void Start()
    {
        AddSpecialSkill1();
        AddSpecialSkill2();
    }
    

    public override void SpecialAttack1()
    {
        if (CanSpecial1())
        {
            GetComponent<Animator>().SetTrigger("special1");
            special1s.transform.position = special1Point.position;
            special1s.GetComponent<Special1>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartSpecial1Cooldown();
        }
    }

    public override void SpecialAttack2()
    {
        if (CanSpecial2())
        {
            special2s.transform.position = special2Point.position;
            special2s.GetComponent<Special2>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartSpecial2Cooldown();
        }
    }


    public void AddSpecialSkill1()
    {
        string Special1Name = (this.tag == "Player1") ? "ItachiS1_P1" : "ItachiS1_P2";
        
        GameObject sp1holder = GameObject.Find(Special1Name);

        if (sp1holder != null)
        {
          
            special1s = sp1holder;
            sp1holder.SetActive(false);


        }
        else
        {
            Debug.LogError("DartHolder not found! Make sure it's in the scene.");
        }
    }

    public void AddSpecialSkill2()
    {
        string Special2Name = (this.tag == "Player1") ? "ItachiS2_P1" : "ItachiS2_P2";

        GameObject sp2holder = GameObject.Find(Special2Name);

        if (sp2holder != null)
        {
            
            special2s = sp2holder;
            sp2holder.SetActive(false);
        }
        else
        {
            Debug.LogError("DartHolder not found! Make sure it's in the scene.");
        }
    }

    
}
