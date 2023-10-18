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
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            ResponseService.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return ResponseService;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var ResponseService = new ServiceResponse<List<GetCharacterDTO>>();

            try
            {
                var character = characters.First(c => c.Id == id);
                if (character is null)
                    throw new Exception($"Character with Id '{id}' not found");

                characters.Remove(character);

                ResponseService.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            }
            catch (Exception ex)
            {
                ResponseService.Success = false;
                ResponseService.Message = ex.Message;
            }

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

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var ResponseService = new ServiceResponse<GetCharacterDTO>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                if (character is null)
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found");

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                ResponseService.Data = _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception ex)
            {
                ResponseService.Success = false;
                ResponseService.Message = ex.Message;
            }
            return ResponseService;
        }
    }
}