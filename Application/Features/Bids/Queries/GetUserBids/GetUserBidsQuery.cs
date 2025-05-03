using Application.Common;
using Application.DTOs.Bid;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bids.Queries.GetUserBids
{
    public class GetUserBidsQuery : IRequest<OperationResult<UserBidsVm>>
    {
    }
}
