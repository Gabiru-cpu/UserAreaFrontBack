using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AreaApi.Domain.Models;
using AreaApi.Infrastructure.Data.Context;

namespace AreaApi.Infrastructure.Data.Repositories
{
    public class AreaRepository
    {
        private readonly MySqlContext _context;

        public AreaRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<List<Area>> ListAreas()
        {
            List<Area> list = await _context.Area
                .OrderBy(p => p.Data)
                .Include(p => p.ApplicationUser)
                .Include(p => p.Usuarios) // Inclua a propriedade Usuarios
                .ToListAsync();

            return list;
        }

        public async Task<List<Area>> ListAreasByApplicationUserId(string applicationUserId)
        {
            List<Area> list = await _context.Area
                .Where(p => p.ApplicationUserId.Equals(applicationUserId))
                .OrderBy(p => p.Data)
                .Include(p => p.ApplicationUser)
                .Include(p => p.Usuarios)
                .ToListAsync();

            return list;
        }

        public async Task<Area> GetAreaById(int areaId)
        {
            Area area = await _context.Area.Include(p => p.ApplicationUser).FirstOrDefaultAsync((p => p.Id == areaId));

            return area;
        }

        public async Task<Area> CreateArea(Area area)
        {
            var ret = await _context.Area.AddAsync(area);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }


        //UPDATE
        public async Task<Area> UpdateArea(Area area)
        {
            _context.Area.Update(area);
            await _context.SaveChangesAsync();
            return area;
        }
        // AreaRepository




        // adicionar usuario na area por ID
        public async Task<Area> AddUserToArea(int areaId, string applicationUserId)
        {
            var area = await _context.Area
                .Include(a => a.Usuarios)
                .FirstOrDefaultAsync(p => p.Id == areaId);

            if (area == null)
            {
                // Área não encontrada, pode tratar apropriadamente
                return null;
            }

            var user = await _context.Users.FindAsync(applicationUserId);
            if (user == null)
            {
                // Usuário não encontrado, pode tratar apropriadamente
                return null;
            }

            area.Usuarios.Add(new AreaUsers { UserId = user.Id, AreaId = areaId, UserName = user.UserName });

            await _context.SaveChangesAsync();

            return area;
        }
    }
}
