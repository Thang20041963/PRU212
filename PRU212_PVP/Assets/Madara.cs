using UnityEngine;
using System.Collections;
public class Madara : CharacterController
{
    public GameObject rockGroup;
    public GameObject Susanoo;
    private Vector3[] initialPositions; // Lưu vị trí ban đầu của đá
    private bool isFalling = false; // Kiểm soát trạng thái rơi



    private void Start()
    {
        FindRockFall();
        FindSusano();
        if (rockGroup != null)
        {
            int count = rockGroup.transform.childCount;
            initialPositions = new Vector3[count];

            for (int i = 0; i < count; i++)
            {
                initialPositions[i] = rockGroup.transform.GetChild(i).position;
                rockGroup.transform.GetChild(i).gameObject.SetActive(false); // Ẩn đá ban đầu
            }
        }
    }
    private void FindRockFall()
    {
        string rockFallName = (this.tag == "Player1") ? "RockFall_P1" : "RockFall_P2";

        GameObject foundRockFall = GameObject.Find(rockFallName);
        if (foundRockFall != null)
        {
            rockGroup = foundRockFall;
            rockGroup.GetComponent<RockFall>().SetOwnerTag(this.tag);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy RockFall trong scene!");
        }
    }

    private void FindSusano()
    {
        string SusanooName = (this.tag == "Player1") ? "Susanoo_P1" : "Susanoo_P2";
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == SusanooName)
            {
                Susanoo = obj;
                Debug.Log("Đã tìm thấy Susanoo (kể cả khi bị disable)");
                return;
            }
        }
        Debug.LogWarning("Không tìm thấy Susanoo trong scene!");
    }



    public override void SpecialAttack1()
    {
        if (CanSpecial1())
        {
            UseChakra(sp1Charka);
            setWaitState(true);
            getAnimator().SetTrigger("specialSkill");
            Invoke(nameof(DropRocks), 1f);
           
        }

    }
    private void DropRocks()
    {
        if (!isFalling)
        {
            setWaitState(false);
            isFalling = true;

            if (rockGroup != null)
            {
                for (int i = 0; i < rockGroup.transform.childCount; i++)
                {
                    GameObject rock = rockGroup.transform.GetChild(i).gameObject;
                    rock.SetActive(true); // Hiện đá lên
                    Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
                    rock.GetComponent<Rock>().setOwnerTag(this.tag);
                    rock.GetComponent<Rock>().setRockDamage(atk);
                    Debug.Log(atk);
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector2.zero; // Reset vận tốc
                        rb.gravityScale = 10; // Kích hoạt trọng lực
                    }
                }
            }

            StartCoroutine(ResetRocksAfterTime(1.5f)); 
        }
    }


    private IEnumerator ResetRocksAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (rockGroup != null)
        {
            for (int i = 0; i < rockGroup.transform.childCount; i++)
            {
                GameObject rock = rockGroup.transform.GetChild(i).gameObject;
                rock.SetActive(false); // Ẩn đi
                rock.transform.position = initialPositions[i]; // Đặt lại vị trí ban đầu
                Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero; // Reset vận tốc để không bị rơi tiếp
                    rb.gravityScale = 0; // Tắt trọng lực để chờ lần rơi tiếp theo
                }
            }
        }

        isFalling = false; // Cho phép rơi lại
    }


    public override void SpecialAttack2()
    {
        if (CanSpecial2())
        {
            UseChakra(sp2Charka);
            setWaitState(true);
            getAnimator().SetTrigger("specialSkill");
            Invoke(nameof(CallSusanoo), 1f);
            
        }

    }

    private void CallSusanoo()
    {
        if (Susanoo != null)
        {
            Susanoo.SetActive(true);
            Susanoo.GetComponent<Susanoo>().SetOwnerTag(this.tag);
            Susanoo.GetComponent<Susanoo>().SetDamage(atk);
            Invoke(nameof(SusanooAtt), 1f);
        }
    }
    private void SusanooAtt()
    {

        Susanoo.GetComponent<Susanoo>().ActivateSusanoo();
        setWaitState(false);
        Invoke(nameof(Disable), 1.5f);
    }
    private void Disable()
    {
        Susanoo.gameObject.SetActive(false);
        
    }

}
