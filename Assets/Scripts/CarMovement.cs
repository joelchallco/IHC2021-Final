using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    public Text UItexto;
    private int counterCoin  =0 ;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(":D");
        UItexto.text = "Coins: " + counterCoin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("XD");
        float vertical = Input.GetAxis("Vertical");
        transform.position = transform.position + vertical * transform.forward * 10f * Time.deltaTime;

        float velocidadGiro = 55f * Input.GetAxis("Horizontal");
        transform.Rotate(0f, velocidadGiro * Time.deltaTime, 0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cylinder")
        {
            Destroy(other.gameObject);
            counterCoin += 1;
            Debug.Log("Num. of coins" + counterCoin.ToString());
            //SetCountText();
            //Debug.Log(UItexto.text);

        }
        UItexto.text = "Coins: " +  counterCoin.ToString();
        ///Debug.Log("Num. of coins" + counterCoin.ToString());
        //Debug.Log(other.name);
    }
}
