using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Renderer meshRenderer;

    private GameObject currentTarget;
    private GameObject previousTarget;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if(!meshRenderer.enabled && currentTarget != null)
        {
            meshRenderer.enabled = true;
            transform.SetParent(currentTarget.transform);
        }
    }

    public void OnTargetFound(GameObject obj)
    {
        previousTarget = currentTarget;
        currentTarget = obj;
    }

    public void Move()
    {
        if(moving) return;
        StartCoroutine(MoveToTarget());
    }

    private bool moving;
    IEnumerator MoveToTarget()
    {
        moving = true;
        while(Vector3.Distance(transform.position, currentTarget.transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
            yield return null;
        }
        moving = false;
    }
}
