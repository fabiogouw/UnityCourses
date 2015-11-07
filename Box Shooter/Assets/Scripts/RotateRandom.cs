using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System;

public class RotateRandom : MonoBehaviour {

	// the speed of the rotation
	private float speedX = 10.0f;
    private float speedY = 10.0f;
    private float speedZ = 10.0f;

    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

    void Start()
    {
        speedX = GetOne() * GetRandomInteger(40, 80);
        speedY = GetOne() * GetRandomInteger(40, 80);
        speedZ = GetOne() * GetRandomInteger(40, 80);
    }

    // Update is called once per frame
    void Update ()
    {
		transform.Rotate(Vector3.right * Time.deltaTime * speedX);
		transform.Rotate(Vector3.up * Time.deltaTime * speedY);
		transform.Rotate(Vector3.forward * Time.deltaTime * speedZ);
	}

    private int GetOne()
    {
        return GetRandomInteger(0, 1) == 1 ? 1 : -1;
    }

    private int GetRandomInteger(int min, int max)
    {
        uint scale = uint.MaxValue;
        while (scale == uint.MaxValue)
        {
            // Get four random bytes.
            byte[] four_bytes = new byte[4];
            rngCsp.GetBytes(four_bytes);

            // Convert that into an uint.
            scale = BitConverter.ToUInt32(four_bytes, 0);
        }

        // Add min to the scaled difference between max and min.
        return (int)(min + (max - min) * (scale / (double)uint.MaxValue));
    }
}