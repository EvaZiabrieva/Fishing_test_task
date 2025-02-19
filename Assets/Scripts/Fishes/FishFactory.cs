using UnityEngine;

public class FishFactory 
{
    public Fish CreateFish(Fish prefab, Transform parent)
    {
        Fish fish = GameObject.Instantiate(prefab, parent.position, Quaternion.identity);
        FishVisualsContainer container = fish.GetComponent<FishVisualsContainer>();

        FishView view = new FishView(container);
        FishData data = new FishData(SystemsContainer.GetSystem<ConfigsSystem>().GetConfig<FishConfig>(fish.ConfigId));
        FishBehaviour behaviour = new FishBehaviour(data.BehaviourData, fish.transform);
        FishBehaviourController controller = new FishBehaviourController(fish, parent);

        fish.Initialize(data, view, behaviour, controller);
        return fish;
    }
}
