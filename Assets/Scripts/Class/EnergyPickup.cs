using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    public float energyAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        
        PlayerEnergy energy = other.GetComponentInParent<PlayerEnergy>();
        if (energy != null)
        {
            energy.AddEnergy(energyAmount);

            Destroy(gameObject); 
        }
    }
}
