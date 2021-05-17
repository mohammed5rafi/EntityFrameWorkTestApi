using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Webapi.Data;
using Webapi.Models;
using Webapi.Models.DTO.Fight;

namespace Webapi.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FightService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<ServiceResponce<FightResultDTO>> Fight(FightRequestDTO request)
        {
            ServiceResponce<FightResultDTO> responce = new ServiceResponce<FightResultDTO>
            {
                Data = new FightResultDTO()
            };
            try
            {
                List<Charecter> charecters = await _context.Charecters.Include(cs => cs.Weapon)
                .Include(c => c.CharecterSkills).ThenInclude(cs => cs.Skill)
                .Where(c => request.CharecterIds.Contains(c.Id)).ToListAsync();
                bool defeated = false;
                while (!defeated)
                {
                    foreach (Charecter attacker in charecters)
                    {
                        List<Charecter> opponenets = charecters.Where(c => c.Id != attacker.Id).ToList();
                        Charecter opponent = opponenets[new Random().Next(opponenets.Count)];
                        int damage = 0;
                        string attackUsed = string.Empty;
                        bool useWeapon = new Random().Next(2) == 0;
                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeaponAttack(attacker, opponent);

                        }
                        else
                        {
                            int randomSkill = new Random().Next(attacker.CharecterSkills.Count);
                            attackUsed = attacker.CharecterSkills[randomSkill].Skill.Name;
                            damage = DoSkillAttack(attacker, opponent, attacker.CharecterSkills[randomSkill]);

                        }
                        responce.Data.Log.Add($"{attacker.Name} attcks {opponent.Name} using {attackUsed} with {(damage > 0 ? damage : 0)} damage.");
                        if (opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            responce.Data.Log.Add($"{opponent.Name} has been defeated!");
                            responce.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
                            break;
                        }
                    }
                }
                charecters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });
                _context.Charecters.UpdateRange(charecters);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
            }
            return responce;
        }

        public async Task<ServiceResponce<AttackResultDTO>> SkillAttack(SKillAttackDTO request)
        {
            ServiceResponce<AttackResultDTO> responce = new ServiceResponce<AttackResultDTO>();
            try
            {

                Charecter attacker = await _context.Charecters
                .Include(c => c.CharecterSkills).ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                Charecter opponent = await _context.Charecters.FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                CharecterSkill charecterSkill = attacker.CharecterSkills.FirstOrDefault(cs => cs.Skill.Id == request.SkillId);
                if (charecterSkill == null)
                {

                    responce.Success = false;
                    responce.Message = $"{attacker.Name} doesnt know that skill!";
                    return responce;
                }
                int damage = DoSkillAttack(attacker, opponent, charecterSkill);
                if (opponent.HitPoints <= 0)
                {
                    responce.Message = $"{opponent.Name} has been defeated!";
                }
                _context.Charecters.Update(opponent);
                await _context.SaveChangesAsync();
                responce.Data = new AttackResultDTO
                {
                    Attacker = attacker.Name,
                    AttackerHp = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage

                };
            }
            catch (Exception ex)
            {

                responce.Success = false;
                responce.Message = ex.Message;
            }
            return responce;
        }



        public async Task<ServiceResponce<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request)
        {
            ServiceResponce<AttackResultDTO> responce = new ServiceResponce<AttackResultDTO>();
            try
            {

                Charecter attacker = await _context.Charecters
                .Include(c => c.Weapon).FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                Charecter opponent = await _context.Charecters.FirstOrDefaultAsync(c => c.Id == request.OpponentId);
                int damage = DoWeaponAttack(attacker, opponent);
                if (opponent.HitPoints <= 0)
                {
                    responce.Message = $"{opponent.Name} has been defeated!";
                }
                _context.Charecters.Update(opponent);
                await _context.SaveChangesAsync();
                responce.Data = new AttackResultDTO
                {
                    Attacker = attacker.Name,
                    AttackerHp = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage

                };
            }
            catch (Exception ex)
            {

                responce.Success = false;
                responce.Message = ex.Message;
            }
            return responce;
        }
        public async Task<ServiceResponce<List<HighscoreDTO>>> GetHighscore()
        {
            List<Charecter> charecters = await _context.Charecters
            .Where(c => c.Fights > 0)
            .OrderByDescending(c => c.Victories)
            .ThenBy(c => c.Defeats)
            .ToListAsync();
            var response = new ServiceResponce<List<HighscoreDTO>>
            {
                Data = charecters.Select(c => _mapper.Map<HighscoreDTO>(c)).ToList()
            };
            return response;
        }
        #region Private
        private static int DoWeaponAttack(Charecter attacker, Charecter opponent)
        {
            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(opponent.Defence);
            if (damage > 0)
            {
                opponent.HitPoints -= damage;

            }

            return damage;
        }
        private static int DoSkillAttack(Charecter attacker, Charecter opponent, CharecterSkill charecterSkill)
        {
            int damage = Convert.ToInt32(charecterSkill.Skill.Damage) + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defence);
            if (damage > 0)
            {
                opponent.HitPoints -= damage;

            }

            return damage;
        }


        #endregion
    }
}