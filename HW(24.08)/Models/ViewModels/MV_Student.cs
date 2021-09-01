using HW_24._08_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_24._08_.Models.ViewModels
{
    public class MV_Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
        public string GroupName { get; set; }
        public Teacher Teacher { get; set; }

        public MV_Student()
        { }

        public MV_Student(Student student)
        {
            FromModel(student);
        }

        public MV_Student FromModel(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            Group = student.Group;
            if (student.Group != null)
                GroupName = student.Group.Name; //формирую контейнер, наполняя его необходимыми данными
            if (student.Group != null)
                Teacher = student.Group.Teacher;
            return this;
        }
    }
}
