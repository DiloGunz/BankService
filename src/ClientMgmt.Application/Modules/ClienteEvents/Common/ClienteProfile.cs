using AutoMapper;
using ClientMgmt.Domain.Entities;

namespace ClientMgmt.Application.Modules.ClienteEvents.Common;

public class ClienteProfile : Profile
{
	public ClienteProfile()
	{
		CreateMap<Cliente, ClienteDto>();
	}
}