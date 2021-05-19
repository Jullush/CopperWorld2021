using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl: MonoBehaviour
{
    private float _activeHoverSpeed;
    private float _activeStrafeSpeed;
    private float _activeForwardSpeed;
    private float lookRateSpeed = 70f;
    private float rollInput;
    
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float hoverSpeed;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollAcceleration;
    [SerializeField] private float forwardAcceleration;
    [SerializeField] private float strafeAcceleration;
    [SerializeField] private float hoverAcceleration;
    
    public Vector2 lookInput;
    public Vector2 screenCenter;
    public Vector2 mouseDistance;
    
    
    
    // Start is called before the first frame update
    private void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    private void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.x;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
        
        transform.Rotate(mouseDistance.x* lookRateSpeed * Time.deltaTime, 
            rollInput * rollSpeed * Time.deltaTime, -mouseDistance.y * lookRateSpeed * Time.deltaTime, Space.Self);
        
        _activeForwardSpeed = Mathf.Lerp(_activeForwardSpeed,
            Input.GetAxisRaw("Horizontal") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        _activeStrafeSpeed = Mathf.Lerp(_activeStrafeSpeed,
            -Input.GetAxisRaw("Vertical") * strafeSpeed, strafeAcceleration* Time.deltaTime);
        _activeHoverSpeed = Mathf.Lerp(_activeHoverSpeed,
            Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime );

        transform.position += transform.forward * _activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * _activeStrafeSpeed * Time.deltaTime) + (transform.up * _activeHoverSpeed * Time.deltaTime);
    }
}
