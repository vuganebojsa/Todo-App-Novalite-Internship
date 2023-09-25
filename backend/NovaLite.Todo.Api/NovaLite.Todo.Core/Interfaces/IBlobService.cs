using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.Interfaces
{
    public interface IBlobService
    {
        string GenerateSaSToken(SasPermission permission, string fileName);
    }
}
