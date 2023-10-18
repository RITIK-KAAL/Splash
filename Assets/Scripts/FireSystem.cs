using UnityEngine;
using UnityEngine.EventSystems;
public class FireSystem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Particles ParticlesInstance;
    private bool isPointerDown;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isPointerDown = false;
    }

    private void Update()
    {
        if(isPointerDown == true)
        {
            ParticlesInstance.Fire(); 
        }
        else
        {
            ParticlesInstance.StopFire(); 
        }
    }
}
