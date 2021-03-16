using CoreAPI.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.HandlerService
{
    public class GetPaidOrderQuery : IRequest<List<Order>>
    {
    }
}
