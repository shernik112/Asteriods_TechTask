
namespace Project.System
{
    public interface ISaveService
    {
        void Save(string key, int value);
        int Load(string key);
    } 
}
