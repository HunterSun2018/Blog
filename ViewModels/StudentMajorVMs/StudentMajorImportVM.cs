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
    public partial class StudentMajorTemplateVM : BaseTemplateVM
    {
        public ExcelPropety Student_Excel = ExcelPropety.CreateProperty<StudentMajor>(x => x.StudentId);
        public ExcelPropety Major_Excel = ExcelPropety.CreateProperty<StudentMajor>(x => x.MajorId);
        [Display(Name = "分数")]
        public ExcelPropety score_Excel = ExcelPropety.CreateProperty<StudentMajor>(x => x.score);

	    protected override void InitVM()
        {
            Student_Excel.DataType = ColumnDataType.ComboBox;
            Student_Excel.ListItems = DC.Set<Student>().GetSelectListItems(Wtm, y => y.LoginName);
            Major_Excel.DataType = ColumnDataType.ComboBox;
            Major_Excel.ListItems = DC.Set<Major>().GetSelectListItems(Wtm, y => y.MajorName);
        }

    }

    public class StudentMajorImportVM : BaseImportVM<StudentMajorTemplateVM, StudentMajor>
    {

    }

}
