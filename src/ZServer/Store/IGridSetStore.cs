using System.Threading.Tasks;
using ZMap.TileGrid;

namespace ZServer.Store
{
    public interface IGridSetStore
    {
        Task<GridSet> FindAsync(string name);
    }
}