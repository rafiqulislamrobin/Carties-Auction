using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly ILogger<AuctionController> _logger;
        private readonly AuctionDbContext _auctionDbContext;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuctionController(ILogger<AuctionController> logger, AuctionDbContext auctionDbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _auctionDbContext = auctionDbContext;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet(Name = "GetAuctions")]
        public async Task<ActionResult<List<AuctionDto>>> GetAuctions(string? date)
        {
            var query = _auctionDbContext.Auctions.OrderBy(x =>x.Item.Make).AsQueryable();
            if (!string.IsNullOrEmpty(date))
            {
                query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
            }
            var result = await query.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
            return result;
           //var auction = await _auctionDbContext.Auctions
           //     .Include(x =>x.Item)
           //     .OrderBy(x =>x.Item.Make)
           //     .ToListAsync();

           // return _mapper.Map<List<AuctionDto>>(auction);
        }

       

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
        {
            var auction = await _auctionDbContext.Auctions
                .Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (auction == null)
                return NotFound("not available");

            return _mapper.Map<AuctionDto>(auction);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
        {
            
            var auction = _mapper.Map<Auction>(auctionDto);

            auction.Seller = User.Identity.Name;

            _auctionDbContext.Add(auction);
            ///if rabbitmq failed then transaction will be cancalled
            var newAuction = _mapper.Map<AuctionDto>(auction);
            await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));

            var result = await _auctionDbContext.SaveChangesAsync() > 0;

            if (!result)
                return NotFound("Failed to create");

            //return where its created
            return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, _mapper.Map<AuctionDto>(auction));

        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<AuctionDto>> UpdateAuction(Guid id, UpdateAuctionDto auctionDto)
        {
            var auction = await _auctionDbContext.Auctions.Include(x =>x.Item)
                .FirstOrDefaultAsync(x =>x.Id == id);

            if (auction == null) return NotFound();

            if (auction.Seller != User.Identity.Name) return Forbid();

            auction.Item!.Make = auctionDto.make ?? auction.Item.Make;
            auction.Item!.Model = auctionDto.Model ?? auction.Item.Model;
            auction.Item!.Color = auctionDto.Color ?? auction.Item.Color;
            auction.Item!.Details = auctionDto.Details ?? auction.Item.Details;
            auction.Item!.Year = auctionDto.Year ?? auction.Item.Year;

            await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));

            var result = await _auctionDbContext.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest("Problem saving changes");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            var auction = await _auctionDbContext.Auctions.FindAsync(id);

            if (auction == null) return NotFound();

            if (auction.Seller != User.Identity.Name) return Forbid();

            _auctionDbContext.Auctions.Remove(auction);

            await _publishEndpoint.Publish<AuctionDeleted>(new { Id = auction.Id.ToString() });

            var result = await _auctionDbContext.SaveChangesAsync() > 0;


            if (result)
                return Ok("Deleted Successfully");

            return BadRequest("Fail to delete");
        }
    }
}
