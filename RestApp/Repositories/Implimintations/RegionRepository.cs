using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestApp.Data;
using RestApp.Models;
using RestApp.Repositories.Interfaces;

namespace RestApp.Repositories.Implimintations
{
    public class RegionRepository : IRegionRepository<Region>
    {
        private readonly ApiDbContext _context;

        public RegionRepository(ApiDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await Task.Run(() => { return _context.Items.ToListAsync(); });
        }

        public async Task<Region> CreateRegion(Region region)
        {
            if (region is null) throw new ArgumentException(nameof(region));
            var result = await _context.Items.AddAsync(region);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Region> DeleteRegion(int id)
        {
            var existItem = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (existItem != null)
            {
                _context.Items.Remove(existItem);
                await _context.SaveChangesAsync();
                return existItem;
            }

            return null;
        }

        public async Task<Region> GetRegion(int id)
        {
            return await _context.Items
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Region> UpdateRegion(Region region)
        {
            var result = await _context.Items.FirstOrDefaultAsync(e => e.Id == region.Id);

            if (result != null)
            {
                result.Alias = region.Alias;
                result.FullName = region.FullName;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}