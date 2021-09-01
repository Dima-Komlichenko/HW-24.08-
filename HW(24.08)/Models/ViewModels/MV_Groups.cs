using HW_24._08_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_24._08_.Models.ViewModels
{
    public class MV_Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public Teacher Teacher { get; set; }

        public MV_Group()
        {

        }
        public MV_Group(Group group)
        {
            FromModel(group);
        }

        public MV_Group FromModel(Group group)
        {
            Id = group.Id;
            Teacher = group.Teacher;
            Students = group.Students;
            return this;
        }
    }
}
