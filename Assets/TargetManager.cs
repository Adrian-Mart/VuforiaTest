using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerUmbrella;
    [SerializeField] private GameObject playerKey;
    [SerializeField] private GameObject umbrella;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject locker;
    [SerializeField] private GameObject originTarget;
    [SerializeField] private TextMeshProUGUI messageLabel;

    [SerializeField] private float speed = 1.0f;

    private GameObject prevTargetObject;
    private GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        prevTargetObject = originTarget;
        started = false;
    }

    bool moving = false;
    public void Move()
    {
        if (targetObject != null)
        {
            if (!moving)
            {
                Debug.Log("Moving to: " + targetObject.name);
                switch (targetObject.name)
                {
                    case "1":
                        if (hasKey)
                        {
                            StartCoroutine(ShowMessage("Has encontrado la llave"));
                        }
                        else
                        {
                            StartCoroutine(MoveToTarget(1));
                        }
                        break;
                    case "2":
                        if (hasUmbrella)
                        {
                            StartCoroutine(ShowMessage("Has encontrado la sombrilla"));
                        }
                        else
                        {
                            StartCoroutine(MoveToTarget(2));
                        }
                        break;
                    case "3":
                        if (hasKey && hasUmbrella)
                        {
                            StartCoroutine(ShowMessage("Has encontrado la salida"));
                            StartCoroutine(MoveToTarget(3));
                        }
                        else if (hasKey)
                        {
                            StartCoroutine(ShowMessage("Te falta la sombrilla"));
                        }
                        else if (hasUmbrella)
                        {
                            StartCoroutine(ShowMessage("Te falta la llave"));
                        }
                        break;

                    default:
                        StartCoroutine(ShowMessage("No puedes ir a este lugar"));
                        break;
                }
            }
        }
    }

    private IEnumerator MoveToTarget(int id)
    {
        moving = true;
        player.transform.SetParent(null);
        Vector3 targetPosition = targetObject.transform.position;

        while (Vector3.Distance(player.transform.position, targetPosition) > 0.1f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        player.transform.SetParent(targetObject.transform);

        switch (id)
        {
            case 1:
                locker.SetActive(false);
                key.SetActive(false);
                playerKey.SetActive(true);
                hasKey = true;
                break;
            case 2:
                umbrella.SetActive(false);
                playerUmbrella.SetActive(true);
                hasUmbrella = true;
                break;
        }

        moving = false;
    }

    private IEnumerator ShowMessage(string message)
    {
        Debug.Log(message);
        messageLabel.text = message;
        yield return new WaitForSeconds(2);
        messageLabel.text = "";
    }

    bool started = false;
    bool hasKey = false;
    bool hasUmbrella = false;
    public void SetTarget(GameObject target)
    {
        Debug.Log("Detected: " + target.name);
        if (!started)
        {
            if (target == originTarget)
            {
                player.transform.SetParent(target.transform);
                started = true;
                Debug.Log("Started");
            }
            else
            {
                target.SetActive(false);
            }
        }
        else
        {
            if (target != originTarget)
            {
                target.SetActive(true);
                prevTargetObject = targetObject;
                targetObject = target;
                Debug.Log("New target: " + targetObject.name);
            }
        }
    }

}
