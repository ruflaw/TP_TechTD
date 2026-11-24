using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private int sous;
    [SerializeField] private float genDelay;
    private GameObject world;

    void Update()
    {
        if (genDelay <= Time.time)
        {
            world.GetComponent<Currency_Manager>().AddCurrency(sous);
            genDelay += Time.time;
        }
    }

    public void AddWorldRef(GameObject world_)
    {
        world = world_;
    }
}
