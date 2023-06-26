using System;
using System.Collections.Generic;

namespace OrderService.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderName { get; set; }

    public DateTime OrderDate { get; set; }
}
