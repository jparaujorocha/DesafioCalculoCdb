using AutoMapper;
using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Application.Interfaces;
using DesafioCalculoCdb.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Application.Services
{
    public class ImpostoInvestimentoService : IImpostoInvestimentoService
    {
        private IImpostoInvestimentoRepository _impostoInvestimentoRepository;
        private readonly IMapper _mapper;

        public ImpostoInvestimentoService(IImpostoInvestimentoRepository impostoInvestimentoRepository, IMapper mapper)
        {
            _impostoInvestimentoRepository = impostoInvestimentoRepository;
            _mapper = mapper;
        }
        public async Task<ImpostoInvestimentoDTO> GetById(int id)
        {
            var impostoInvestimentoEntity = await _impostoInvestimentoRepository.GetById(id);
            return _mapper.Map<ImpostoInvestimentoDTO>(impostoInvestimentoEntity);
             
        }

        public IEnumerable<ImpostoInvestimentoDTO> GetByIdInvestimento(int idInvestimento)
        {
            var listImpostoInvestimento = _impostoInvestimentoRepository.GetByIdInvestimento(idInvestimento);
            return _mapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimento);
        }

        public async Task<IEnumerable<ImpostoInvestimentoDTO>> GetImpostoInvestimentosAtivos()
        {
            var listImpostoInvestimento = await _impostoInvestimentoRepository.GetImpostosInvestimentosAtivos();
            return _mapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimento);
        }
    }
}
