global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character {Id = 1, Name = "Sam"}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var ResponseService = new ServiceResponse<List<GetCharacterDTO>>();
            characters.Add(_mapper.Map<Character>(newCharacter));
            ResponseService.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return ResponseService;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var ResponseService = new ServiceResponse<List<GetCharacterDTO>>();
            ResponseService.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return ResponseService;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            var ResponseService = new ServiceResponse<GetCharacterDTO>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            ResponseService.Data = _mapper.Map<GetCharacterDTO>(character);
            return ResponseService;
        }
    }
}