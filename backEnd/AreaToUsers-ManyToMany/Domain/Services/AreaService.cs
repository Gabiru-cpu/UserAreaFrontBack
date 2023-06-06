using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AreaApi.Domain.Models;
using AreaApi.Infrastructure.Data.Repositories;
using System.Linq;

namespace AreaApi.Domain.Services
{
    public class AreaService
    {
        private readonly AreaRepository _areaRepository;
        private readonly AuthService _authService;

        public AreaService(AreaRepository areaRepository, AuthService authService)
        {            
            _areaRepository = areaRepository;
            _authService = authService;
        }

        public async Task<List<Area>> ListAreas()
        {
            List<Area> list = await _areaRepository.ListAreas();

            return list;
        }

        public async Task<List<Area>> ListMeusAreas()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Area> list = await _areaRepository.ListAreasByApplicationUserId(currentUser.Id);

            return list;
        }

        public async Task<Area> GetArea(int areaId)
        {
            Area area = await _areaRepository.GetAreaById(areaId);

            if (area == null)
                throw new ArgumentException("Area não existe!");

            return area;
        }

        public async Task<Area> NovoArea(Area area)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Area novoArea = new Area();

            novoArea.ApplicationUserId = currentUser.Id;
            novoArea.Data = DateTime.Now;
            novoArea.Titulo = area.Titulo;
            novoArea.Conteudo = area.Conteudo;

            //adicionar o criador na area usando o currentUser na coluna Usuarios
            novoArea.Usuarios = new List<AreaUsers>
            {
                new AreaUsers
                {
                    UserId = currentUser.Id,
                    AreaId = novoArea.Id,
                    UserName = currentUser.UserName
                }
            };

            novoArea = await _areaRepository.CreateArea(novoArea);

            return novoArea;
        }




        public async Task AtualizarArea(Area area)
        {
            await _areaRepository.UpdateArea(area);
        }






        public async Task AdicionarUsuario(int areaId, string userIdAdd)
        {
            Area area = await _areaRepository.GetAreaById(areaId);
            if (area == null)
            {
                throw new ArgumentException("Área não existe!");
            }

            ApplicationUser currentUser = await _authService.GetCurrentUser();
            if (currentUser == null)
            {
                throw new ArgumentException("Usuário atual não encontrado!");
            }

            if (area.ApplicationUserId != currentUser.Id)
            {
                throw new ArgumentException("Apenas o criador da área pode adicionar usuários!");
            }

            ApplicationUser userToAdd = await _authService.GetUserById(userIdAdd);
            if (userToAdd == null)
            {
                throw new ArgumentException("Usuário a ser adicionado não encontrado!");
            }

            if (area.Usuarios == null)
            {
                area.Usuarios = new List<AreaUsers>();
            }

            // Verificar se já existe um usuário com as mesmas propriedades UserId e AreaId
            if (area.Usuarios.Any(u => u.Id != 0 && u.UserId == userToAdd.Id && u.AreaId == area.Id))
            {
                throw new ArgumentException("Usuário já está na área!");
            }

            area.Usuarios.Add(new AreaUsers { UserId = userToAdd.Id, AreaId = area.Id, UserName = userToAdd.UserName });

            await _areaRepository.UpdateArea(area);
        }






    }
}
