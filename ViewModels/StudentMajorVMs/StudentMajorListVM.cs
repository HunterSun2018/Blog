using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Blog.Model;


namespace Blog.ViewModels.StudentMajorVMs
{
    public partial class StudentMajorListVM : BasePagedListVM<StudentMajor_View, StudentMajorSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("StudentMajor", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("StudentMajor", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMajor", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMajor", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMajor", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMajor", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMajor", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMajor", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<StudentMajor_View>> InitGridHeader()
        {
            return new List<GridColumn<StudentMajor_View>>{
                this.MakeGridHeader(x => x.LoginName_view),
                this.MakeGridHeader(x => x.MajorName_view),
                this.MakeGridHeader(x => x.score),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<StudentMajor_View> GetSearchQuery()
        {
            var query = DC.Set<StudentMajor>()
                .CheckEqual(Searcher.StudentId, x=>x.StudentId)
                .CheckEqual(Searcher.MajorId, x=>x.MajorId)
                .Select(x => new StudentMajor_View
                {
				    ID = x.ID,
                    LoginName_view = x.Student.LoginName,
                    MajorName_view = x.Major.MajorName,
                    score = x.score,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class StudentMajor_View : StudentMajor{
        [Display(Name = "账号")]
        public String LoginName_view { get; set; }
        [Display(Name = "专业名称")]
        public String MajorName_view { get; set; }

    }
}
