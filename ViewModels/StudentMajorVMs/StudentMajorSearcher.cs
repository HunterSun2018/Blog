using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Blog.Model;


namespace Blog.ViewModels.StudentMajorVMs
{
    public partial class StudentMajorSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AllStudents { get; set; }
        public Guid? StudentId { get; set; }
        public List<ComboSelectListItem> AllMajors { get; set; }
        public Guid? MajorId { get; set; }

        protected override void InitVM()
        {
            AllStudents = DC.Set<Student>().GetSelectListItems(Wtm, y => y.LoginName);
            AllMajors = DC.Set<Major>().GetSelectListItems(Wtm, y => y.MajorName);
        }

    }
}
