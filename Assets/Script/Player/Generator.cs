using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private int sous;
    [SerializeField] private float genDelay;
    [SerializeField] private float genTimer = 0f;
    private GameObject world;

    void Update()
    {
        genTimer += Time.deltaTime;

        if (genTimer >= genDelay)
        {
            world.GetComponent<Currency_Manager>().AddCurrency(sous);
            genTimer = 0f;
        }
    }

    public void AddWorldRef(GameObject world_)
    {
        world = world_;
    }
}
