using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Blog.Model;


namespace Blog.ViewModels.StudentMajorVMs
{
    public partial class StudentMajorVM : BaseCRUDVM<StudentMajor>
    {
        public List<ComboSelectListItem> AllStudents { get; set; }
        public List<ComboSelectListItem> AllMajors { get; set; }

        public StudentMajorVM()
        {
            SetInclude(x => x.Student);
            SetInclude(x => x.Major);
        }

        protected override void InitVM()
        {
            AllStudents = DC.Set<Student>().GetSelectListItems(Wtm, y => y.LoginName);
            AllMajors = DC.Set<Major>().GetSelectListItems(Wtm, y => y.MajorName);
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
