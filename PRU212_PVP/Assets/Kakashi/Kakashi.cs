using UnityEngine;

public class Kakashi : CharacterController
{

    //public Transform dartPoint;
    //public Transform attackPoint;
    //public float punchRange = 0.3f;
    //public float kickRange = 0.5f;
   // public GameObject[] darts;

    private void Start()
    {
        //// Find the DartHolder object
        //GameObject dartHolder = GameObject.Find("DartHolder");

        //if (dartHolder != null)
        //{
        //    // Get all darts that are children of DartHolder
        //    darts = new GameObject[dartHolder.transform.childCount];
        //    for (int i = 0; i < dartHolder.transform.childCount; i++)
        //    {
        //        darts[i] = dartHolder.transform.GetChild(i).gameObject;
        //    }
        //}
        //else
        //{
        //    Debug.LogError("DartHolder not found! Make sure it's in the scene.");
        //}
    }
    

    //public override void KickAttack()
    //{
    //    if (CanKick())
    //    {
    //        GetComponent<Animator>().SetTrigger("kick");
    //        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, kickRange);
    //        if (hit != null)
    //        {
    //            Debug.Log("Hit: " + hit.name);
    //            // You can add logic here to damage the other player or object
    //        }
    //        StartKickCooldown();
    //    }
    //}

    //public override void PunchAttack()
    //{
    //    if (CanPunch())
    //    {
    //        GetComponent<Animator>().SetTrigger("punch");
    //        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, punchRange);
    //        if (hit != null)
    //        {
    //            Debug.Log("Hit: " + hit.name);
    //            // You can add logic here to damage the other player or object
    //        }
    //        StartPunchCooldown();
    //    }

    //}

    public override void SpecialAttack1()
    {
        throw new System.NotImplementedException();
    }

    public override void SpecialAttack2()
    {
        throw new System.NotImplementedException();
    }

    //public override void ThrowDart()
    //{
    //    if (CanThrowDart())
    //    {
    //        GetComponent<Animator>().SetTrigger("throwDart");
    //        darts[FindDart()].transform.position = dartPoint.position;
    //        darts[FindDart()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    //        StartDartCooldown();
    //    }
    //}

    //private int FindDart()
    //{
    //    for (int i = 0; i < darts.Length; i++) 
    //    {
    //        if (!darts[i].gameObject.activeInHierarchy)
    //            return i;
    //    }
    //    return 0;
    //}

}
