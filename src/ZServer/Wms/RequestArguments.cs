﻿using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace ZServer.Wms;

public class RequestArguments
{
    public  Envelope Envelope { get; set; }
    public int SRID { get; set; }
    public List<(string ResourceGroup, string Layer)> Layers { get; set; }
}