using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 target;
    public Animator explosionAnimator;
    public GameObject explosion;
    public bool isMoving = false;
    public Image fadeImage;
    public GameObject blackScreen;
    public VerticalScroll movingFloor;
    public MovingFloor mf;
    public GameObject gameEndScreen;

    void Start()
    {
        target = transform.position; 
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }

    public void Scenario0()
    {
        mf.isMoving = false;
        isMoving = true;
        speed = 1f;


        Delay(2, () =>
        {
            blackScreen.SetActive(true);
            PlayFade();
        });
        Delay(3, () =>
        {
            target = new Vector3(29, -23, 0);
            speed = 100f;
        });
        Delay(4, () =>
        {
            blackScreen.SetActive(false);
            isMoving = false;
            mf.isMoving = true;
        });

    }

    public void Scenario1()
    {
        isMoving = true;
        target = new Vector3(29, -20, 0);
        speed = 1f;

        Delay(3, () =>
        {
            target = new Vector3(27, -20, 0);
            speed = 1f;
        });
        Delay(5, () =>
        {
            target = new Vector3(27, -17, 0);
            speed = 1f;
        });
        Delay(8, () =>
        {
            blackScreen.SetActive(true);
            PlayFade();
        });
        Delay(9, () =>
        {
            target = new Vector3(29, -23, 0);
            speed = 100f;
        });
        Delay(10, () =>
        {
            blackScreen.SetActive(false);
            isMoving = false;
        });
        
    }

    public void Scenario2()
    {
        isMoving= true;
        target = new Vector3(29, -20, 0);
        speed = 1f;


        Delay(3, () =>
        {
            target = new Vector3(27, -20, 0);
        });
        Delay(5, () =>
        {
            target = new Vector3(27, -18, 0);
        });
        Delay(7, () =>
        {
            speed = 3f;
            target = new Vector3(27, -15, 0);
        });
        Delay(8, () =>
        {
            speed = 1f;
            target = new Vector3(27, -14, 0);
        });
        Delay(9, () =>
        {
            target = new Vector3(31, -14, 0);
        });
        Delay(13, () =>
        {
            target = new Vector3(31, -16, 0);
        });
        Delay(15, () =>
        {
            target = new Vector3(33, -16, 0); 
        });
        Delay(17, () =>
        {
            target = new Vector3(33, -22, 0);
        });
        Delay(23, () =>
        {
            blackScreen.SetActive(true);
            PlayFade();
        });
        Delay(24, () =>
        {
            target = new Vector3(29, -23, 0);
            speed = 100f;
        });
        Delay(25, () =>
        {
            blackScreen.SetActive(false);
            isMoving = false;
        });
        
    }

    public void Scenario3()
    {
        isMoving = true;
        target = new Vector3(29, -20, 0);
        speed = 1f;


        Delay(3, () =>
        {
            target = new Vector3(27, -20, 0);
        });
        Delay(5, () =>
        {
            target = new Vector3(27, -18, 0);
        });
        Delay(7, () =>
        {
            speed = 3f;
            target = new Vector3(27, -15, 0);
        });
        Delay(8, () =>
        {
            speed = 1f;
            target = new Vector3(27, -14, 0);
        });
        Delay(9, () =>
        {
            target = new Vector3(31, -14, 0);
        });
        Delay(13, () =>
        {
            movingFloor.pixelsPerSecond = -15f;
            target = new Vector3(31, -10, 0);
        });
        Delay(17, () =>
        {
            target = new Vector3(29, -10, 0);
        });
        Delay(19, () =>
        {
            target = new Vector3(29, -7, 0);
        });
        Delay(22, () =>
        {
            blackScreen.SetActive(true);
            PlayFade();
        });
        Delay(23, () =>
        {
            gameEndScreen.SetActive(true);
        });

    }

    public void Delay(float time, Action action)
    {
        StartCoroutine(DelayCoroutine(time, action));
    }

    IEnumerator DelayCoroutine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    public void PlayFade()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        float fadeInTime = 0.5f;
        float holdTime = 1f;
        float fadeOutTime = 0.5f;

        float time = 0f;

        while (time < fadeInTime)
        {
            time += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / fadeInTime);

            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(holdTime);

        time = 0f;

        while (time < fadeOutTime)
        {
            time += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(1f, 0f, time / fadeOutTime);

            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }
}