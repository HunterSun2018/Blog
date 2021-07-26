using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Blog.Model
{
    public enum SchoolTypeEnum
    {
        [Display(Name = "公立学校")]
        PUB,
        [Display(Name = "私立学校")]
        PRI
    }
    public class School : BasePoco
    {
        [Display(Name = "学校编码")]
        [Required(ErrorMessage = "{0}是必填项")]
        [RegularExpression("^[0-9]{3,3}$", ErrorMessage = "{0}必须是3位数字")]
        [StringLength(3)]
        public string SchoolCode { get; set; }

        [Display(Name = "学校名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string SchoolName { get; set; }

        [Display(Name = "学校类型")]
        [Required(ErrorMessage = "{0}是必填项")]
        public SchoolTypeEnum? SchoolType { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
    public class Major : BasePoco
    {
        [Display(Name = "专业编码")]
        [Required(ErrorMessage = "{0}是必填项")]
        [RegularExpression("^[0-9]{3,3}$", ErrorMessage = "{0}必须是3位数字")]
        public string MajorCode { get; set; }

        [Display(Name = "专业名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string MajorName { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        [Display(Name = "所属学校")]
        [Required()]
        public Guid? SchoolId { get; set; }
        public School School { get; set; }

        [Display(Name = "学生")]
        public List<Student> Students { get; set; }
        public List<StudentMajor> StudentMajors { get; set; }
    }

    public class Student : BasePoco
    {
        [Display(Name = "账号")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string LoginName { get; set; }

        [Display(Name = "密码")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(32)]
        public string Password { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Name { get; set; }

        [Display(Name = "手机")]
        [RegularExpression("^[1][3,4,5,7,8][0-9]{9}$", ErrorMessage = "{0}格式错误")]
        public string CellPhone { get; set; }

        [Display(Name = "邮编")]
        [RegularExpression("^[0-9]{6,6}$", ErrorMessage = "{0}必须是6位数字")]
        public string ZipCode { get; set; }

        [Display(Name = "日期")]
        public DateTime? EnRollDate { get; set; }

        [Display(Name = "照片")]
        public Guid? PhotoId { get; set; }
        public FileAttachment Photo { get; set; }

        [Display(Name = "专业")]
        public List<Major> Majors { get; set; }
        public List<StudentMajor> StudentMajors { get; set; }
    }
    // [MiddleTable]
    public class StudentMajor : BasePoco
    {
        [Display(Name = "学生ID")]
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [Display(Name = "专业ID")]
        public Guid MajorId { get; set; }
        public Major Major { get; set; }

        [Display(Name = "分数")]
        public uint score { get; set; }
    }
}