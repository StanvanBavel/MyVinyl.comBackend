using MyVinyl.com.Database.Contexts;
using MyVinyl.com.Database.Converters;
using MyVinyl.com.Database.Datamodels;
using MyVinyl.com.Database.Datamodels.Dtos;
using MyVinyl.com.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVinyl.com.Services
{
    public class VinylService : IVinylService
    {
        private readonly VinylContext _context;
        private readonly IDtoConverter<Vinyl, VinylRequest, VinylResponse> _converter;

        public VinylService(VinylContext context, IDtoConverter<Vinyl, VinylRequest, VinylResponse> converter)
        {
            _context = context;
            _converter = converter;
        }


        public async Task<VinylResponse> AddAsync(VinylRequest request)
        {
            Vinyl vinyl = _converter.DtoToModel(request);

            if (await IsDuplicateAsync(vinyl))
            {
                throw new DuplicateException("This Vinyl already exists.");
            }

            await _context.AddAsync(vinyl);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(vinyl);
        }

        public async Task<List<VinylResponse>> GetAllAsync()
        {
            List<Vinyl> vinyls = await _context.Vinyls.Where(e => e.IsActive).ToListAsync();
            List<VinylResponse> responses = new List<VinylResponse>();

            return responses;
        }

        public async Task<VinylResponse> GetByIdAsync(Guid id)
        {
            Vinyl vinyl = await _context.Vinyls.FirstOrDefaultAsync(e => e.Id == id);

            if (vinyl == null)
            {
                throw new NotFoundException($"Vinyl with id {id} not found.");
            }

            return await CreateResponseAsync(vinyl);
        }

        public async Task<VinylResponse> GetByNameAsync(string name)
        {
            Vinyl vinyl = await _context.Vinyls.FirstOrDefaultAsync(e => e.Name == name);

            if (vinyl == null)
            {
                throw new NotFoundException($"Vinyl with name {name} not found.");
            }

            return await CreateResponseAsync(vinyl);
        }

        public async Task<VinylResponse> UpdateAsync(Guid id, VinylRequest request)
        {
            Vinyl vinyl = _converter.DtoToModel(request);
            vinyl.Id = id;

            if (!await _context.Vinyls.AnyAsync(v => v.Id == id))
            {
                throw new NotFoundException($"Vinyl with id {id} not found.");
            }

            if (await IsDuplicateAsync(vinyl, id))
            {
                throw new DuplicateException("This Vinyl already exists.");
            }

            _context.Update(vinyl);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(vinyl);
        }

        public async Task<VinylResponse> DeleteAsync(Guid id)
        {
            Vinyl vinyl = await _context.Vinyls.FirstOrDefaultAsync(e => e.Id == id);

            if (vinyl == null)
            {
                throw new NotFoundException($"Vinyl with id {id} not found.");
            }

            // Set inactive:
            vinyl.IsActive = false;

          

            // Update record:
            _context.Update(vinyl);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(vinyl);
        }

        private async Task<bool> IsDuplicateAsync(Vinyl vinyl, Guid? id=null)
        {
            if (id == null)
            {
                return await _context.Vinyls.AnyAsync(
                    e => e.Name == vinyl.Name
                    && e.Description == vinyl.Description
                    && e.Image == vinyl.Image
                    && e.IsActive);
            }
            else
            {
                return await _context.Vinyls.AnyAsync(
                    e => e.Name == vinyl.Name
                    && e.Description == vinyl.Description
                    && e.Image == vinyl.Image
                    && e.IsActive && e.Id != id);
            }
        }

        private async Task<VinylResponse> CreateResponseAsync(Vinyl Vinyl)
        {
            VinylResponse response = _converter.ModelToDto(Vinyl);
        

            return response;
        }
    }
}
