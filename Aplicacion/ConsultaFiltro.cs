﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Uttt.Micro.Libro.Aplicacion;
using Uttt.Micro.Libro.Modelo;
using Uttt.Micro.Libro.Persistencia;


namespace Uttt.Micro.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroMaterialDto>
        {
            public Guid? LibroId { get; set; }
        }
        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDto>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriasMaterial
                    .Where(x => x.LibreriaMaterialId == request.LibroId)
                    .FirstOrDefaultAsync();

                if (libro == null)
                {
                    return null; // ✅ No lanzar excepción
                }

                var LibroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);
                return LibroDto;
            }
        }
    }
}