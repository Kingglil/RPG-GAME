using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{

    private float slowMotionTimeScale = 0.25f;

    void Update()
    {
        if (Input.GetAxis("RSB") == 1 && Input.GetAxis("LT") >= 0.5f && !AbilitiesManager.stopTime)
        {
            if (!AbilitiesManager.slowMotion)
            {
                StartCoroutine(GoInSlowMotion());
                AbilitiesManager.slowMotion = true;
            }
        }
        else if (AbilitiesManager.slowMotion)
        {
            StopCoroutine(GoInSlowMotion());
            StartCoroutine(GoOutOfSlowMotion());
            AbilitiesManager.slowMotion = false;
        }
    }

    IEnumerator GoInSlowMotion()
    {
        for (float i = 1; i >= slowMotionTimeScale; i -= 0.1f)
        {
            Time.timeScale = i;
            Time.fixedDeltaTime = 0.016f * i;
            print(i);
            Camera.main.fieldOfView -= 0.5f;
            yield return null;
        }
        StopCoroutine(GoInSlowMotion());
    }

    IEnumerator GoOutOfSlowMotion()
    {
        for (float i = slowMotionTimeScale; i <= 1; i += 0.1f)
        {
            Time.timeScale = i;
            Time.fixedDeltaTime = 0.016f * i;
            Camera.main.fieldOfView += 0.5f;
            yield return null;
        }
        StopCoroutine(GoOutOfSlowMotion());
    }
}
