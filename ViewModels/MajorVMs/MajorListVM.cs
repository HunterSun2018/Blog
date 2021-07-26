using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Blog.Model;


namespace Blog.ViewModels.MajorVMs
{
    public partial class MajorListVM : BasePagedListVM<Major_View, MajorSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<Major_View>> InitGridHeader()
        {
            return new List<GridColumn<Major_View>>{
                this.MakeGridHeader(x => x.MajorCode),
                this.MakeGridHeader(x => x.MajorName),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeader(x => x.SchoolName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Major_View> GetSearchQuery()
        {
            var query = DC.Set<Major>()
                .Select(x => new Major_View
                {
				    ID = x.ID,
                    MajorCode = x.MajorCode,
                    MajorName = x.MajorName,
                    Remark = x.Remark,
                    SchoolName_view = x.School.SchoolName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Major_View : Major{
        [Display(Name = "学校名称")]
        public String SchoolName_view { get; set; }

    }
}
