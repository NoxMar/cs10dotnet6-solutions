using Grpc.Core;
using Packt.Shared;

namespace Northwind.gRPC.Services;

public class ShipperService : Shipr.ShiprBase
{
    private readonly ILogger<ShipperService> _logger;
    private readonly NorthwindContext _db;

    public ShipperService(ILogger<ShipperService> logger, NorthwindContext db)
    {
        _logger = logger;
        _db = db;
    }

    public override async Task<ShipperReply> GetShipper(ShipperRequest request, ServerCallContext ctx)
    {
        return ToShipperReply(await _db.Shippers.FindAsync(request.ShipperId));
    }

    private ShipperReply ToShipperReply(Shipper? shipper)
    {
        return new ShipperReply
        {
            ShipperId = shipper?.ShipperId ?? 0,
            CompanyName = shipper?.CompanyName ?? string.Empty,
            Phone = shipper?.Phone ?? string.Empty
        };
    }
}