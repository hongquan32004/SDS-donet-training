using ProtoBuf;
using System.Runtime.Serialization;
namespace DTOS.Responses
{
	[DataContract]
	public class ExportExcel
	{
		[DataMember(Order = 1)] public byte[] fileBytes { get; set; }
		[DataMember(Order = 2)] public string fileName { get; set; }
	}
}
