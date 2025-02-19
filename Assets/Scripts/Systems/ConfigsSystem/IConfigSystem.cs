/// <summary>
/// Override to implement other configs storing systems
/// </summary>
public interface IConfigSystem : ISystem
{
    public abstract T GetConfig<T>(string id) where T : BaseConfig, new();
}
