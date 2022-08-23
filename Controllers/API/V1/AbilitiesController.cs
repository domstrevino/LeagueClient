﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeagueClient.Models;

namespace LeagueClient.Controllers.API.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbilitiesController : ControllerBase
    {
        private readonly LeagueClientContext _context;

        public AbilitiesController(LeagueClientContext context)
        {
            _context = context;
        }

        // GET: api/Abilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ability>>> GetAbilities()
        {
          if (_context.Abilities == null)
          {
              return NotFound();
          }
            return await _context.Abilities.ToListAsync();
        }

        // GET: api/Abilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ability>> GetAbility(int id)
        {
          if (_context.Abilities == null)
          {
              return NotFound();
          }
            var ability = await _context.Abilities.FindAsync(id);

            if (ability == null)
            {
                return NotFound();
            }

            return ability;
        }

        // PUT: api/Abilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbility(int id, Ability ability)
        {
            if (id != ability.AbilityId)
            {
                return BadRequest();
            }

            _context.Entry(ability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbilityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Abilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ability>> PostAbility(Ability ability)
        {
          if (_context.Abilities == null)
          {
              return Problem("Entity set 'LeagueClientContext.Abilities'  is null.");
          }
            _context.Abilities.Add(ability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbility", new { id = ability.AbilityId }, ability);
        }

        // DELETE: api/Abilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbility(int id)
        {
            if (_context.Abilities == null)
            {
                return NotFound();
            }
            var ability = await _context.Abilities.FindAsync(id);
            if (ability == null)
            {
                return NotFound();
            }

            _context.Abilities.Remove(ability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbilityExists(int id)
        {
            return (_context.Abilities?.Any(e => e.AbilityId == id)).GetValueOrDefault();
        }
    }
}
