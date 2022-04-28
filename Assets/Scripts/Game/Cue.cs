using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{
    public GameObject blanca;
    public float Multiplicador;
    private Vector3 diferencia;
    private Camera mainCamera;
    private float zangle;
    private bool dragging = false;
    private float fuerza = 0f;
    private Vector3 direccion;
    private Rigidbody rb;
    public List<GameObject> balls;
    private void Start()
    {
        diferencia = gameObject.transform.position - blanca.transform.position;
        mainCamera = Camera.main;
        zangle = gameObject.transform.rotation.z * Mathf.Rad2Deg;
        rb = blanca.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            GameObject child = gameObject.transform.GetChild(0).gameObject;
            child.transform.localPosition = new Vector3(-2.5f, child.transform.localPosition.y, child.transform.localPosition.z);
            direccion = gameObject.transform.GetChild(0).position - blanca.transform.position;
            rb.AddForce(-direccion * (Mathf.Pow(fuerza, 2)) * Multiplicador);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GameManager.Instance.faseactual = GameManager.turnphase.moviendo;
        }
        if (GameManager.Instance.faseactual == GameManager.turnphase.apuntar)
        {
            if (!dragging)
            {
                setRotation();
            }
            else
            {
                setPosition();
            }
        }

        gameObject.transform.position = blanca.transform.position + diferencia;

        if (Mathf.Abs(rb.velocity.magnitude) < 0.05f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Invoke("activeTrue", 5f);
        }
        foreach (var ball in balls)
        {
            if (ball != null)
            {
                Rigidbody ballrb = ball.GetComponent<Rigidbody>();
                if (Mathf.Abs(ballrb.velocity.magnitude) < 0.05f)
                {
                    ballrb.velocity = Vector3.zero;
                    ballrb.angularVelocity = Vector3.zero;

                }

            }
        }
    }
    private void setRotation()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castpoint = mainCamera.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castpoint, out hit, Mathf.Infinity))
        {
            Vector3 aux = hit.point - blanca.transform.position;
            float angle = Mathf.Atan2(aux.z, -aux.x);
            angle = Mathf.Rad2Deg * angle;
            gameObject.transform.rotation = Quaternion.Euler(0f, angle, zangle);
        }
    }
    private void setPosition()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castpoint = mainCamera.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castpoint, out hit, Mathf.Infinity))
        {
            float distance = Vector3.Distance(hit.point, blanca.transform.position);
            fuerza = distance;
            GameObject child = gameObject.transform.GetChild(0).gameObject;
            child.transform.localPosition = new Vector3(-2.5f - (distance / 2), child.transform.localPosition.y, child.transform.localPosition.z);
        }
    }
    private void activeTrue()
    {
        if (GameManager.Instance.faseactual == GameManager.turnphase.moviendo && !gameObject.transform.GetChild(0).gameObject.activeSelf )
        {
            Debug.Log("activado");
            if (!GameManager.Instance.repetirturno)
            {
                GameManager.Instance.changeTurn();
            }
            GameManager.Instance.faseactual = GameManager.turnphase.apuntar;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
