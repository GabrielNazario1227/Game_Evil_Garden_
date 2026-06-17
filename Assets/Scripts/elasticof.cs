using UnityEngine;

public class PlataformaElastica : MonoBehaviour
{
    public GameObject plataforma2;

    public float k = 10f;

    public float massa1 = 1f;
    public float massa2 = 1f;

    public Vector2 velocidade1;
    public Vector2 velocidade2;

    public float comprimentoEquilibrio = 2f;


    void FixedUpdate()
    {

        Vector2 direcao = new Vector2(
            0,
            plataforma2.transform.position.y - transform.position.y
        );


        float distancia = direcao.magnitude;



        float x = distancia - comprimentoEquilibrio;

        Vector2 forca = k * x * direcao.normalized;

        Vector2 aceleracao1 = forca / massa1;
        Vector2 aceleracao2 = -forca / massa2;


        velocidade1 += aceleracao1 * Time.deltaTime;
        velocidade2 += aceleracao2 * Time.deltaTime;

        transform.position += (Vector3)(velocidade1 * Time.deltaTime);

        plataforma2.transform.position +=
            (Vector3)(velocidade2 * Time.deltaTime);
    }
}