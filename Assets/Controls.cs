using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject[] Thrusters;
    public float R3HorizontalFactor;
    public float L3HorizontalFactor;
    public float L3VerticalFactor;
    public float ThrustFactor;
    private Rigidbody _rigidbody;
    public ParticleSystem[] _particles;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //_rigidbody.maxAngularVelocity = float.MaxValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        var R3Horizontal = Input.GetAxisRaw("R3 horizontal");

        _rigidbody.AddRelativeTorque(Vector3.up * R3Horizontal * R3HorizontalFactor);
        var L3Horizontal = Input.GetAxisRaw("L3 horizontal");
        _rigidbody.AddRelativeTorque(Vector3.back * L3Horizontal * L3HorizontalFactor);
        var L3Vertical = Input.GetAxisRaw("L3 vertical");
        _rigidbody.AddRelativeTorque(Vector3.left * L3Vertical * L3VerticalFactor);
        var downThrust = Input.GetAxisRaw("L2");
        _rigidbody.AddRelativeForce(Vector3.up * 9.81f * (1 - downThrust), ForceMode.Force);
        var upThrust = Input.GetButton("L1") ? ThrustFactor : 0;
        _rigidbody.AddRelativeForce(Vector3.up * 9.81f * upThrust, ForceMode.Force);
        for (int i = 0; i < _particles.Length; i++)
        {
            var main = _particles[i].main;
            main.startSpeed = new ParticleSystem.MinMaxCurve (7 + 3 * upThrust - 3 * downThrust);
        }
        
        //_particles.main.startSpeed = new ParticleSystem.MinMaxCurve(7 + 7 * upThrust - 7 * downThrust);
    }
}
