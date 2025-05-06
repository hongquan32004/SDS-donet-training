using ProtoBuf;
namespace StudentManagementgRPCService.Contracts.Responses
{
	[ProtoContract]
	public class ExportExcel
	{
		[ProtoMember(1)] public byte[] fileBytes { get; set; }
		[ProtoMember(2)] public string fileName { get; set; }
	}
}
