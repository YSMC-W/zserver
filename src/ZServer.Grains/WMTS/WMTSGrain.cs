using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using ZMap.Extensions;
using ZServer.Interfaces;
using ZServer.Interfaces.WMTS;
using ZServer.Wmts;

namespace ZServer.Grains.WMTS
{
    // ReSharper disable once InconsistentNaming
    public class WMTSGrain : Grain, IWMTSGrain
    {
        private readonly IWmtsService _wmtsService;

        public WMTSGrain(IWmtsService wmtsService)
        {
            _wmtsService = wmtsService;
        }

        public async Task<MapResult> GetTileAsync(string layers, string styles, string format,
            string tileMatrixSet,
            string tileMatrix, int tileRow,
            int tileCol, string cqlFilter, IDictionary<string, object> arguments)
        {
            var result = await _wmtsService.GetTileAsync(layers, styles, format, tileMatrixSet, tileMatrix, tileRow,
                tileCol, cqlFilter,
                arguments);
            if (!string.IsNullOrEmpty(result.Code))
            {
                return MapResult.Failed(result.Message, result.Code);
            }

            await using var stream = result.Stream;
            var bytes = await result.Stream.ToArrayAsync();
            return MapResult.Ok(bytes, format);
        }
    }
}