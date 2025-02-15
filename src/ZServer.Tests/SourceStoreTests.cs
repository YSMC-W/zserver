using System.Linq;
using System.Threading.Tasks;
using ZMap.Source.Postgre;
using ZServer.Store;
using Xunit;
using ZMap.Source.ShapeFile;

namespace ZServer.Tests
{
    public class SourceStoreTests : BaseTests
    {
        [Fact]
        public async Task GetPgSource()
        {
            var sourceStore = GetScopedService<ISourceStore>();
            var dataSource = (PostgreSource)await sourceStore.FindAsync("berlin_db");

            Assert.NotNull(dataSource);
            Assert.Equal(
                "User ID=postgres;Password=1qazZAQ!;Host=localhost;Port=5432;Database=berlin;Pooling=true;",
                dataSource.ConnectionString);
            Assert.Equal("berlin", dataSource.Database);
        }

        [Fact]
        public async Task GetShapeSource()
        {
            var sourceStore = GetScopedService<ISourceStore>();
            var dataSource = (ShapeFileSource)await sourceStore.FindAsync("berlin_shp");

            Assert.NotNull(dataSource);
            Assert.EndsWith("osmbuildings.shp", dataSource.File);
            Assert.Equal(4326, dataSource.Srid);
        }

        [Fact]
        public async Task GetAllSource()
        {
            var sourceStore = GetScopedService<ISourceStore>();
            var dataSources = await sourceStore.GetAllAsync();
            var dataSource1 = (PostgreSource)dataSources.First(x => x is PostgreSource);
            Assert.NotNull(dataSource1);
            Assert.Equal(
                "User ID=postgres;Password=1qazZAQ!;Host=localhost;Port=5432;Database=berlin;Pooling=true;",
                dataSource1.ConnectionString);
            Assert.Equal("berlin", dataSource1.Database);

            var dataSource2 = (ShapeFileSource)dataSources.First(x => x is ShapeFileSource);
            Assert.NotNull(dataSource2);

            Assert.EndsWith("osmbuildings.shp", dataSource2.File);
            Assert.Equal(4326, dataSource2.Srid);
        }
    }
}