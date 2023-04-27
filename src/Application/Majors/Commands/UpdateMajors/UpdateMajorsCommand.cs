
using AutoMapper;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Majors.Dtos;

namespace WeCare.Application.Majors.Commands.UpdateMajors;
public class UpdateMajorsCommand : IRequest<MajorDto>
{
    public int MajorId { get; set; }
    public string MajorName { get; set; } = null!;
}

public class UpdateMajorsCommandHandler : IRequestHandler<UpdateMajorsCommand, MajorDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public UpdateMajorsCommandHandler(IMapper mapper, IApplicationDbContext context) { 
        _mapper = mapper;
        _context = context;
    }
    public async Task<MajorDto> Handle(UpdateMajorsCommand request, CancellationToken cancellationToken) 
    {
        var major = (await _context.Majors.FindAsync(request.MajorId))!;
        major.Name = request.MajorName;
        
        _context.Majors.Update(major);

        await _context.SaveChangesAsync(cancellationToken);
       
        return _mapper.Map<MajorDto>(major);





    }
}
