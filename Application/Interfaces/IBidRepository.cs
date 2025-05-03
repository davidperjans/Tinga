using Application.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBidRepository : IRepository<Bid>
    {
        Task<OperationResult<IEnumerable<Bid>>> GetByListingIdAsync(Guid listingId);
        Task<OperationResult<IEnumerable<Bid>>> GetByUserIdAsync(Guid userId);
        Task<OperationResult<decimal>> GetHighestBidForListingAsync(Guid listingId);
    }
}
