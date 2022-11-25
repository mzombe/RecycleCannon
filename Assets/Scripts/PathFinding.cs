using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public GameObject alvo;
    public GameObject wayPointAtual;
    public float velocidadeIA;
    public float velocidadeIARun;

    private GameObject objetivo;
    private float distanciaWayPoint;
    private float distanciaAlvo;
    private bool isCol = false;

    public bool ingorePlayer = false;

    private bool notice = false;

    private void Start()
    {
        if(ingorePlayer) FindObjectOfType<AudioManager>().PlaySound("BossSound"); 
        alvo = GameObject.FindGameObjectWithTag("LastPath");
        wayPointAtual = GameObject.FindGameObjectWithTag("FirstPath");
    }

    void Update()
    {
        if(alvo == null){
            alvo = GameObject.FindGameObjectWithTag("LastPath");
        }

        Vector3 distanciaAtual = transform.position - wayPointAtual.transform.position;
        
        distanciaWayPoint = distanciaAtual.magnitude;

        distanciaAlvo = Vector3.Distance(transform.position, alvo.transform.position);

        if (distanciaWayPoint < distanciaAlvo)
        {
            objetivo = wayPointAtual;
        }
        else
        {
            objetivo = alvo;
        }


        if(distanciaWayPoint < 0.3f)
        {
            WayPoint scriptWayPoints = wayPointAtual.GetComponent<WayPoint>();

            float distanciaTemporaria = Mathf.Infinity;

            for(int i = 0; i < scriptWayPoints.vizinhos.Length; i++ )
            {   
                if(notice){
                    float distancia = Vector3.Distance(alvo.transform.position, scriptWayPoints.vizinhos[i].transform.position);

                    if(distancia < distanciaTemporaria)
                    {
                        distanciaTemporaria = distancia;
                        wayPointAtual = scriptWayPoints.vizinhos[i];
                    }
                }else{
                    int random = Random.Range(0, scriptWayPoints.vizinhos.Length);
                    wayPointAtual = scriptWayPoints.vizinhos[random];
                }
            }
        }
      
      if(!isCol){
        if (notice){ 
            transform.Translate(Vector3.forward * velocidadeIARun * Time.deltaTime);
            transform.LookAt(objetivo.transform.position);
        }else{
            transform.Translate(Vector3.forward * velocidadeIA * Time.deltaTime);
            transform.LookAt(wayPointAtual.transform.position);
        }
      }
    }

    private void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag != "Bullet"){
            if(!ingorePlayer){
                isCol = true;  
            }else if(col.gameObject.tag != "Player"){
                isCol = true;
            }
        }
	}

	private void OnCollisionExit(Collision col) {
		StartCoroutine(ReturnWalk());
	}

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player" && !ingorePlayer){
            FindObjectOfType<AudioManager>().PlaySound("Notice");
            alvo = col.gameObject;
            notice = true;
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.tag == "Player" && !ingorePlayer){
            alvo = GameObject.FindGameObjectWithTag("LastPath");
            notice = false;
        }
    }

    IEnumerator ReturnWalk(){
        yield return new WaitForSeconds(1.5f);
		isCol = false;
	}
}
