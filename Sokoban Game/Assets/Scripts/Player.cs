using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    CommandMng CM;
    RaycastHit2D hit;
    int layerMask;
    public float x;
    public float y;
    void Start() {
        layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("Baggage"));//�ڱ� �ڽ� ���ܽ�Ű��
        CM = new CommandMng();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (SystemMng.ins.state == SystemMng.STATE.PAUSE)       //state�� PUASE�϶� return;
            return;
        Move();
        UndoPos();
        ResetPos();
    }
    public void UndoPos() {                                     //ZŰ �Է½� �������
        if (Input.GetKeyDown(KeyCode.Z)) {
            Vector2 newPos = CM.Undo();
            if (newPos.x == 0 && newPos.y == 0)
                return;
            StageMng.ins.Step -= 1;
            CameraMove.ins.PlayUndoSound();
            this.transform.position = newPos;
        }

    }
    public void ResetPos() {                                    //CŰ �Է½� ��ġ �ʱ�ȭ
        if (Input.GetKeyDown(KeyCode.C)||SystemMng.ins.state==SystemMng.STATE.RESET) {
            Vector2 newPos = CM.Reset();
            if (newPos.x == 0 && newPos.y == 0)
                return;

            StageMng.ins.Step = 0;
            this.transform.position = newPos;
        }

    }
    void Move() {
        //���� �̵�
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            spriteRenderer.sprite = sprites[0];
            hit = Physics2D.Raycast(transform.position, -transform.right, 1f, layerMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Wall")) {                       //���� ���� return
                    print("���� ���� ����");
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Baggage")) {                    //�� �߰�
                    print("�� �߰�");
                    Baggage baggage = hit.collider.gameObject.GetComponent<Baggage>();
                    if (!baggage.MoveSelf("left"))                                      //���� �̵��Ұ��� return
                        return;
                }
            }
            StageMng.ins.StepUp();                                                      //Step�� 1����
            CM.Execute(new Vector2(this.transform.position.x - 1, this.transform.position.y), this.transform.position);//CommnadMng�� �̵� ���
            CameraMove.ins.PlayMoveSound();
            this.transform.Translate(new Vector3(-1, 0, 0));
        }
        //������ �̵�
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            spriteRenderer.sprite = sprites[1];
            hit = Physics2D.Raycast(transform.position, transform.right, 1f, layerMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Wall")) {
                    print("������ ���� ����");
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Baggage")) {
                    print("�� �߰�");
                    Baggage baggage = hit.collider.gameObject.GetComponent<Baggage>();
                    if (!baggage.MoveSelf("right"))
                        return;
                }
            }
            StageMng.ins.StepUp();
            CM.Execute(new Vector2(this.transform.position.x + 1, this.transform.position.y), this.transform.position);
            CameraMove.ins.PlayMoveSound();
            this.transform.Translate(new Vector3(1, 0, 0));
        }
        //���� �̵�
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            spriteRenderer.sprite = sprites[2];
            hit = Physics2D.Raycast(transform.position, transform.up, 1f, layerMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Wall")) {
                    print("���� ���� ����");
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Baggage")) {
                    print("�� �߰�");
                    Baggage baggage = hit.collider.gameObject.GetComponent<Baggage>();
                    if (!baggage.MoveSelf("up"))
                        return;
                }
            }
            StageMng.ins.StepUp();
            CM.Execute(new Vector2(this.transform.position.x, this.transform.position.y + 1), this.transform.position);
            CameraMove.ins.PlayMoveSound();
            this.transform.Translate(new Vector3(0, 1, 0));
        }
        //�Ʒ��� �̵�
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            spriteRenderer.sprite = sprites[3];
            hit = Physics2D.Raycast(transform.position, -transform.up, 1f, layerMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Wall")) {
                    print("�Ʒ��� ���� ����");
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Baggage")) {
                    print("�� �߰�");
                    Baggage baggage = hit.collider.gameObject.GetComponent<Baggage>();
                    if (!baggage.MoveSelf("down"))
                        return;
                }
            }
            StageMng.ins.StepUp();
            CM.Execute(new Vector2(this.transform.position.x, this.transform.position.y - 1), this.transform.position);
            CameraMove.ins.PlayMoveSound();
            this.transform.Translate(new Vector3(0, -1, 0));
        }
    }

}
