using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public float x;
    public float y;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Baggage")) {
            print("충돌");
            spriteRenderer.color = Color.black;
            StageMng.ins.CntUp();
            if (gameObject.name == "Start") {
                CameraMove.ins.GameStart();
            }
            else if (gameObject.name == "Exit")
                Application.Quit();

        }

    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Baggage")) {
            print("충돌해제");
            spriteRenderer.color = Color.red;
            StageMng.ins.CntDown();
        }

    }
}
