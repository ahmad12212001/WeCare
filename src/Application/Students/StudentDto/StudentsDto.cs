using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeCare.Domain.Entities;

namespace WeCare.Application.Students.StudentDto;
public class StudentsDto
{
    public string StudentId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public Major Major { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
