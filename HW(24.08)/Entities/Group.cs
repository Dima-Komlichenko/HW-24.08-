using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_24._08_.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
