using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_24._08_.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
    }
}
