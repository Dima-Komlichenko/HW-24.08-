using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HW_24._08_.Data;
using HW_24._08_.Entities;
using HW_24._08_.Models.ViewModels;

namespace HW_24._08_.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            //return await (from g in _context.Groups
            //         join t in _context.Teachers on g.TeacherId equals t.Id
            //         join s in _context.Students on g.Id equals s.GroupId into students
            //         select new MV_Group() { Id = g.Id, Name = g.Name, Teacher = t, Students = (List<Student>)students }).ToListAsync();



            //return await _context.Groups
            //       .Include(g => g.Teacher)
            //       .ToListAsync();



            return await Task.Run(() =>
            {
                var groups = _context.Groups.ToListAsync().Result;
                var teacher = _context.Teachers.ToListAsync().Result;
                var students = _context.Students.ToListAsync().Result;


                groups.ForEach(g => g.Teacher.Group = null);
                students.ForEach(s => s.Group = null);
               
                foreach (var g in groups)
                {
                    g.Students = students.Where(s => s.GroupId == g.Id).ToList();
                }

                return groups;
            });
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        // GET: api/Groups/TeachersFree
        [HttpGet("TeachersFree")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachersFree()
        {
            return await _context.Teachers.Where(t => t.Group == null).ToListAsync();
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            _context.Entry(group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = group.Id }, group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
