namespace Elysium.Service
{
    public interface IService
    {
        string Name { get; }

        string Application { get; }

        string Version { get; }
    }
}