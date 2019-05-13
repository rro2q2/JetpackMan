using UnityEngine;

public class Jetpack : MonoBehaviour
{

    public float thrustMultiplier = 15;
    public float thrustEmissionRate = 30;

    private Rigidbody _rigidbody;
    private SteamVR_TrackedController _controller;
    private ParticleSystem _thrust;

    private void OnEnable()
    {
        _controller = GetComponent<SteamVR_TrackedController>();
        _controller.PadClicked += HandlePadClicked;
        _thrust = GetComponentInChildren<ParticleSystem>();
    }

    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        GameObject ammo = Instantiate(GameManager.instance.amoPrefab, transform.position, Quaternion.identity) as GameObject;
        ammo.GetComponent<Rigidbody>().AddForce(transform.forward * 100f, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        _controller.PadClicked -= HandlePadClicked;
    }

    
    private void Start()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 0..1
        var thrust = _controller.controllerState.rAxis1.x;

        if (thrust > 0.1f)
        {
            var forceVector = transform.forward * thrust * thrustMultiplier;
            _thrust.emissionRate = thrust * thrustEmissionRate;
            _rigidbody.AddForce(forceVector);
        }
        else
        {
            _thrust.emissionRate = 0f;
        }
    }
    
}
