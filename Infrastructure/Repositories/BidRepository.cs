using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BidRepository : GenericRepository<Bid>, IBidRepository
    {
        private readonly AppDbContext _context;
        public BidRepository(AppDbContext context) :base(context)
        {
            _context = context;
        }
        public async Task<OperationResult<IEnumerable<Bid>>> GetByListingIdAsync(Guid listingId)
        {
            var bids = await _context.Bids
                .Where(b => b.ListingId == listingId)
                .OrderByDescending(b => b.Amount)
                .ToListAsync();

            return OperationResult<IEnumerable<Bid>>.Success(bids);
        }

        public async Task<OperationResult<IEnumerable<Bid>>> GetByUserIdAsync(Guid userId)
        {
            var bids = await _context.Bids
                .Include(b => b.Listing)
                .Where(b => b.BidderId == userId)
                .OrderByDescending(b => b.PlacedAt)
                .ToListAsync();

            return OperationResult<IEnumerable<Bid>>.Success(bids);
        }

        public async Task<OperationResult<decimal>> GetHighestBidForListingAsync(Guid listingId)
        {
            var highestBid = await _context.Bids
                .Where(b => b.ListingId == listingId && !b.IsRetracted)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefaultAsync();

            var amount = highestBid?.Amount ?? 0;

            return OperationResult<decimal>.Success(amount);
        }
    }
}
