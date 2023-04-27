using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeCare.Application.Common.Mappings;
using WeCare.Domain.Entities;

namespace WeCare.Application.Exams.Dto;
public class ExamDto : IMapFrom<Exam>
{
    public DateTime Date { get; set; }
    public string HallNu { get; set; } = null!;
    public string loc { get; set; } = null!;
}
