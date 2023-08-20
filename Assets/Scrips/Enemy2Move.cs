using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Move : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;  //�ൿ��ǥ�� ������ ���� ���� -1 , ������ 1
   // public int Gravity;  //�߷� ��

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Wait();
        Invoke("Wait", 3); //�־��� �ð��� ���� ��, ������ �Լ��� �����ϴ� �Լ� -> ( "�Լ� �̸�" , ���ʵ�);

    }
 
    // Update is called once per frame
    void FixedUpdate()
    {   //Move
        rigid.velocity = new Vector2(nextMove, rigid.position.y);
        
        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove , rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        // DrawRay( ) : ������ �󿡼��� Ray�� �׷��ִ� �Լ�  (��ġ , ��� ���� ,���� �÷�(�ڵ忡�� �������))
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));  // (��ġ , ��� ���� ,�Ÿ�(���൵ �´�) , Layer����)
                                                                                                            //RayCastHit : Ray�� ���� ������Ʈ, GetMask : ���̾� �̸��� �ش��ϴ� �������� �����ϴ� ��
        if (rayHit.collider == null)
        {
            nextMove = nextMove *-1;
          //  Gravity -= 1;
            CancelInvoke();
            Invoke("Wait", 5);

        }

    }
    //����Լ� : �ڽ��� ������ ȣ���ϴ� �Լ�
    private List<int> exclusionList = new List<int>() { 0 }; //������ ��
    void Wait()
    {
        nextMove = Random.Range(-1, 1);  //�ִ밪���� ����������

        while (exclusionList.Contains(nextMove))
        {
            nextMove = Random.Range(-1, 1);
        }

    }
}
//�⺻ ������