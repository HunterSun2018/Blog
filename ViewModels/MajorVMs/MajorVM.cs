using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Blog.Model;


namespace Blog.ViewModels.MajorVMs
{
    public partial class MajorVM : BaseCRUDVM<Major>
    {
        public List<ComboSelectListItem> AllSchools { get; set; }

        public MajorVM()
        {
            SetInclude(x => x.School);
        }

        protected override void InitVM()
        {
            AllSchools = DC.Set<School>().GetSelectListItems(Wtm, y => y.SchoolName, y => y.ID, true, true);
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
