/// <summary>
/// Description of systems nedded implementation
/// </summary>
public interface ISystem
{
    void Initialize();
    void Shutdown();
    bool IsInitialized { get; }
}
