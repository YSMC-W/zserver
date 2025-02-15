﻿using System.Linq;
using System.Threading.Tasks;
using ZServer.Store;
using Xunit;

namespace ZServer.Tests
{
    public class LayerGroupStoreTests : BaseTests
    {
        [Fact]
        public async Task FindByName()
        {
            var store = GetScopedService<ILayerGroupStore>();
            var layerGroup = await store.FindAsync(null,"berlin_group");
            Assert.NotNull(layerGroup);
            Assert.Equal("berlin_group", layerGroup.Name);

            var shpLayer =layerGroup.Layers[0];
            var pgLayer = layerGroup.Layers[1];
            Assert.Equal("berlin_shp", shpLayer.Name);
            Assert.Equal("berlin_db", pgLayer.Name);
            Assert.Equal("resourceGroup1", layerGroup.ResourceGroup.Name);
            Assert.Contains("This is my first resource group", layerGroup.ResourceGroup.Description);
        }

        [Fact]
        public async Task GetAll()
        {
            var store = GetScopedService<ILayerGroupStore>();

            var layerGroups = await store.GetAllAsync();
            Assert.NotNull(layerGroups);
            Assert.Equal(2, layerGroups.Count);
            var berlinGroup = layerGroups.First(x => x.Name == "berlin_group");
            var berlinGroup2= layerGroups.First(x => x.Name == "berlin_group2");
 
            Assert.Equal("resourceGroup1", berlinGroup.ResourceGroup.Name);
            Assert.Contains("This is my first resource group", berlinGroup.ResourceGroup.Description);
            Assert.Equal("resourceGroup2", berlinGroup2.ResourceGroup.Name);
            Assert.Contains("This is my second resource group", berlinGroup2.ResourceGroup.Description);
        }

    }
}
